using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Numerics;
using Bobcos_Server;
using LiteNetLib;

namespace GameServer
{
    class Client
    {
        public static int dataBufferSize = 512;

        public int id;
        public User user;
        public TCP tcp;
        public NetPeer peer;

        public Client(int _clientId)
        {
            id = _clientId;
                     user        = new User() { id = id };

            tcp = new TCP(id);
        }

        public class TCP
        {

            private readonly int id;
            private int clientid;
            private Packet receivedData;

            public TCP(int _id)
            {
                id = _id;
                receivedData = new Packet();

            }









            public void ReceivedBytes(byte[] bytes)
            {
                receivedData = new Packet();

                receivedData.Reset(HandleData(bytes));

            }



            private bool HandleData(byte[] _data)
            {
                int _packetLength = 0;

                receivedData.SetBytes(_data);

                if (receivedData.UnreadLength() >= 4)
                {
                    _packetLength = receivedData.ReadInt();
                    if (_packetLength <= 0)
                    {
                        return true;
                    }
                }

                while (_packetLength > 0 && _packetLength <= receivedData.UnreadLength())
                {
                    byte[] _packetBytes = receivedData.ReadBytes(_packetLength);
                   
                        using (Packet _packet = new Packet(_packetBytes))
                        {
                            int _packetId = _packet.ReadInt();
                            Server.packetHandlers[_packetId](id, _packet);
                        }
                    

                    _packetLength = 0;
                    if(receivedData == null)
                    {
                        Console.WriteLine("A problem occoured Received data is null");
                        return true;
                    }
                    if (receivedData.UnreadLength() >= 4)
                    {
                        _packetLength = receivedData.ReadInt();
                        if (_packetLength <= 0)
                        {
                            return true;
                        }
                    }
                }

                if (_packetLength <= 1)
                {
                    return true;
                }

                return false;
            }

            public void Disconnect()
            {
                receivedData = null;
            }
        }

        
        


        public void Disconnect()
        {
            try
            {

                Logic.worlds[user.World].LeaveWorld(peer.Id);

            }
            catch
            {


            }
            user = null;
            Server.server.DisconnectPeer(peer);
            peer = null;
            tcp.Disconnect();
        }
    }
}