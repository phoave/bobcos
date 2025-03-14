using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using Bobcos_Server;
using LiteNetLib;

namespace GameServer
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, Client> Clients = new Dictionary<int, Client>();
        public delegate void PacketHandler(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandler> packetHandlers;



        public static EventBasedNetListener Netlistener;
        public static NetManager server;


        public static void Start(int _maxPlayers, int _port)
        {
            MaxPlayers = _maxPlayers;
            Port = _port;

            Console.WriteLine("Starting server...");
            InitializeServerData();

            Netlistener = new EventBasedNetListener();
            server = new NetManager(Netlistener);
            server.Start(Port);

            Netlistener.ConnectionRequestEvent += Netlistener_ConnectionRequestEvent;
            Netlistener.PeerConnectedEvent += Netlistener_PeerConnectedEvent;
            Netlistener.NetworkReceiveEvent += Netlistener_NetworkReceiveEvent;
            Netlistener.PeerDisconnectedEvent += Netlistener_PeerDisconnectedEvent;

            Console.WriteLine($"Server started on port {Port}.");

            EventBasedNetListener clientls = new EventBasedNetListener();
            NetManager client = new NetManager(clientls);
            client.Start();
            client.Connect("127.0.0.1", Port, "KEYSERVER22");
        }

        private static void Netlistener_PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
        {
            Console.WriteLine($"Peer disconnected! {peer.Id}");


            if (peer.Id == 0)
            {
                EventBasedNetListener clientls = new EventBasedNetListener();
                NetManager client = new NetManager(clientls);
                client.Start();
                client.Connect("127.0.0.1", Port, "KEYSERVER22");
            }
            try
            {

                Logic.worlds[Clients[peer.Id].user.World].LeaveWorld(peer.Id);

            }
            catch
            {


            }
            Clients[peer.Id].user = null;
            peer.Disconnect();

        }

        private static void Netlistener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
        {
            Clients[peer.Id].tcp.ReceivedBytes(reader.GetRemainingBytes());

        }

        private static void Netlistener_PeerConnectedEvent(NetPeer peer)
        {
            Console.WriteLine($"Peer connected! {peer.Id}");
            Clients[peer.Id].peer = peer;
        }

        private static void Netlistener_ConnectionRequestEvent(ConnectionRequest request)
        {
            Console.WriteLine($"Incoming connection from {request.RemoteEndPoint.Address}");
            request.Accept();
        }



        public static void SendUDPData(NetPeer peer, Packet _packet, DeliveryMethod dMethod)
        {
            try
            {
                if (peer == null)
                {
                    return;
                }
                if (_packet == null)
                {
                    return;
                }

                peer.Send(_packet.ToArray(), dMethod);

            }
            catch
            {

            }
        }

        private static void InitializeServerData()
        {
            for (int i = 0; i <= MaxPlayers; i++)
            {
                Clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandler>()
            {
              {(int)ClientPackets.welcomeReceived,ServerHandle.WelcomeReceived  },
                {(int)ClientPackets.Vertificate,ServerHandle.Vertifacition },
                                {(int)ClientPackets.AccountLoginRegisterandWorldEnter,ServerHandle.HandleAccountandworld },
                                {(int)ClientPackets.Chat,ServerHandle.ChatFromClient },

                {(int)ClientPackets.PositionFromClient,ServerHandle.PositionFromClient },
                                {(int)ClientPackets.InventoryTileSelect,ServerHandle.TileSelected },
           {(int)ClientPackets.BlockEdit,ServerHandle.BlockEditFromClient },
       {(int)ClientPackets.StringMessage,ServerHandle.StringFromClient },
       {(int)ClientPackets.TrashRequest,ServerHandle.TrashRequest },
              {(int)ClientPackets.WearItem,ServerHandle.ItemWear },
              {(int)ClientPackets.BuyItem,ServerHandle.BuyItem },
                            {(int)ClientPackets.CraftRequest,ServerHandle.CraftReq },
                                                        {(int)ClientPackets.Trade,ServerHandle.TradeUpdate },
                                                        {(int)ClientPackets.Vertifaction,ServerHandle.Vertifacition },
                                                        {(int)ClientPackets.DropItem,ServerHandle.ItemDropRequest },
                                                        {(int)ClientPackets.DamageData,ServerHandle.DamageDataReceived },
                {(int)ClientPackets.PasswordChange,ServerHandle.PassChangeRequest },                {(int)ClientPackets.FisherBobAmountData,ServerHandle.FisherBobAmountDataReceived }


            };
            Console.WriteLine("Initialized packets.");
        }
    }
}