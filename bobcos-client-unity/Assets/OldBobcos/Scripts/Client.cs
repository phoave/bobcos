using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using System;
using LiteNetLib;
using UnityEngine.SceneManagement;

public class Client : MonoBehaviour
{
    public static Client instance;
    public static int dataBufferSize = 1024;

    public string ip = "127.0.0.1";
    public int port = 2020;
    public int myId = 0;
    public TCP tcp;

    private bool isConnected = false;
    private delegate void PacketHandler(Packet _packet);
    private static Dictionary<int, PacketHandler> packetHandlers;
    public GameObject DisconnectedScreen;
    public EventBasedNetListener listener;
    public NetManager Client1;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Start()
    {
        tcp = new TCP();
        ConnectToServer();
    }

    public void Update()
    {
        Client1.PollEvents();
    }
    private void OnApplicationQuit()
    {
        Disconnect();
    }

    public void ConnectToServer()
    {
        InitializeClientData();

        isConnected = true;

        listener = new EventBasedNetListener();
        Client1 = new NetManager(listener);

        Client1.Start();
        Client1.Connect(ip, port, "KEY");
       
      

        listener.PeerConnectedEvent += Listener_PeerConnectedEvent;
        listener.NetworkReceiveEvent += Listener_NetworkReceiveEvent;
        listener.PeerDisconnectedEvent += Listener_PeerDisconnectedEvent;
    }

    private void Listener_PeerDisconnectedEvent(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Disconnect();
    }

    private void Listener_NetworkReceiveEvent(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
      Client.instance.tcp.receivedData.Reset(Client.instance.tcp.HandleData(reader.GetRemainingBytes()));


    }

    private void Listener_PeerConnectedEvent(NetPeer peer)
    {
        Debug.Log("Connected to server!");
        tcp.Connect();

    }

    public class TCP
    { 

        public Packet receivedData;

        public void Connect()
        {
            receivedData = new Packet();
            ClientSend.SendVertifaction();
        }



        public void SendData(Packet _packet,DeliveryMethod Dmethod)
        {
            Client.instance.Client1.FirstPeer.Send(_packet.ToArray(), Dmethod);
        }

       

        public bool HandleData(byte[] _data)
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
                        packetHandlers[_packetId](_packet);
                    }
               

                _packetLength = 0;
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

        private void Disconnect()
        {
            instance.Disconnect();

            receivedData = null;
        }
    }


    private void InitializeClientData()
    {
        packetHandlers = new Dictionary<int, PacketHandler>()
        {
         {(int)ServerPackets.welcome,ClientHandle.Welcome },
         {(int)ServerPackets.LoadingSignal,ClientHandle.LoadingSingal },
         {(int)ServerPackets.resultofenter,ClientHandle.resultofenter },
         {(int)ServerPackets.Menu,ClientHandle.MenuFromServer},
                  {(int)ServerPackets.Chat,ClientHandle.Chat},
            {(int)ServerPackets.playerspawn,ClientHandle.SpawnPlayer },
                        {(int)ServerPackets.PositionFromServer,ClientHandle.PositionFromServer },
                        {(int)ServerPackets.worlddata,ClientHandle.WorldDataFGReceived },
              {(int)ServerPackets.inventory,ClientHandle.Inventory },
              {(int)ServerPackets.block,ClientHandle.OnBlockData },
              {(int)ServerPackets.breakinganim,ClientHandle.BreakBlockAnim },
          {(int)ServerPackets.SendCash,ClientHandle.Cash },
                    {(int)ServerPackets.ItemInfo,ClientHandle.ItemInfoReceived },
                                        {(int)ServerPackets.PlayerApperance,ClientHandle.PlayerApperance },
                                        {(int)ServerPackets.PlayerChatBubble,ClientHandle.PlayerChatBubble },
                                        {(int)ServerPackets.ShopData,ClientHandle.ShopDataReceived },
                                        {(int)ServerPackets.Anim,ClientHandle.AnimReceived },
                                        {(int)ServerPackets.Trade,ClientHandle.TradeUpdate },
                                        {(int)ServerPackets.DropDatas,ClientHandle.DropDatas },
                                        {(int)ServerPackets.ClientInfo,ClientHandle.ClientInfo },
                    {(int)ServerPackets.Bubble,ClientHandle.BubbleReceived },
                    {(int)ServerPackets.WarningMessage,ClientHandle.WarningFromServer }


        };
        Debug.Log("Initialized packets.");
    }

    public void Disconnect()
    {
        if (isConnected)
        {
            isConnected = false;

            Debug.Log("Disconnected from server.");
            StartCoroutine(waitforSecs());

        
        }


    }
    public void DisconnectFast()
    {
        if (isConnected)
        {
            isConnected = false;

            DisconnectedScreen.SetActive(true);
            //try reconnect
            SceneManager.LoadScene(0);


        }


    }
    IEnumerator waitforSecs()
    {
        yield return new WaitForSeconds(10);
        
                DisconnectedScreen.SetActive(true);
        //try reconnect
        SceneManager.LoadScene(0);
    
    }
}