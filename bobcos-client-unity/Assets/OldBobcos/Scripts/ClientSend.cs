using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Device;

public   class ClientSend
    {


        public static void SendTcpData(Packet _packet)
        {
            _packet.WriteLength();
            Client.instance.tcp.SendData(_packet,LiteNetLib.DeliveryMethod.ReliableOrdered);
        }
    public static void SendUdpData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet, LiteNetLib.DeliveryMethod.ReliableOrdered);
    }
    public static void Refresh(string text)
        {

            Packet packet = new Packet((int)ClientPackets.welcomeReceived);
            packet.Write(text);
            SendTcpData(packet);
            packet.Dispose();

        }
    public static void SendPassChange(string oldpass1,string newpass1)
    {

        Packet packet = new Packet((int)ClientPackets.PasswordChange);
        packet.Write((byte)0);

        packet.Write(oldpass1) ;
        packet.Write(newpass1);

        SendTcpData(packet);
        packet.Dispose();

    }
    public static void SendPassRecovery(string mail, string username)
    {

        Packet packet = new Packet((int)ClientPackets.PasswordChange);
        packet.Write((byte)1);
        packet.Write(mail);
        packet.Write(username);

        SendTcpData(packet);
        packet.Dispose();

    }


    public static void Vertification(string hwid,string version,string country)
    {
        Packet packet = new Packet((int)ClientPackets.Vertificate);
        packet.Write(hwid);
        packet.Write(version);
        packet.Write(country);


        SendTcpData(packet);
        packet.Dispose();


    }


    public static void SendLoginOrWorld(string username,string password)
    {
        Packet p = new Packet((int)ClientPackets.AccountLoginRegisterandWorldEnter);

        p.Write((byte)1); // login
        p.Write(username);
        p.Write(password);
        SendTcpData(p);
        p.Dispose();

    }
    public static void SendLoginOrWorld(string username, string password,string email)
    {
        Packet p = new Packet((int)ClientPackets.AccountLoginRegisterandWorldEnter);

        p.Write((byte)2); // register
        p.Write(username);
        p.Write(password);
        p.Write(email);

        SendTcpData(p);
        p.Dispose();
    }
    public static void SendLoginOrWorld(string worldname)
    {
        Packet p = new Packet((int)ClientPackets.AccountLoginRegisterandWorldEnter);

        p.Write((byte)3); // WorldEnter
        p.Write(worldname);

        SendTcpData(p);
        p.Dispose();
    
    }

    public static void SendChat(string chat)
    {
        Packet p = new Packet((int)ClientPackets.Chat);

        p.Write(chat);

        SendTcpData(p);
        p.Dispose();

    }
    public static void SendPosition(float x, float y,bool isjump)
    {
        Packet p = new Packet((int)ClientPackets.PositionFromClient);

        // convert to short data

        


        p.Write(x);
        p.Write(y);
        p.Write(isjump);

        SendUdpData(p);
        p.Dispose();

    }

    public static void SendInventory(short selected)
    {
        Packet p = new Packet((int)ClientPackets.InventoryTileSelect);
        p.Write((short)selected);

        SendTcpData(p);
        p.Dispose();

    }

    public static void SendBlock(short blocksira)
    {
        Packet p = new Packet((int)ClientPackets.BlockEdit);
        p.Write((short)blocksira);

        SendTcpData(p);
        p.Dispose();

    }

    public static void SendString(string stringmessage)
    {
        Packet p = new Packet((int)ClientPackets.StringMessage);
        p.Write(stringmessage);

        SendTcpData(p);
        p.Dispose();

    }

    public static void SendTrashRequest(short count)
    {
        Packet p = new Packet((int)ClientPackets.TrashRequest);
        p.Write(count);

        SendTcpData(p);
        p.Dispose();

    }
    public static void SendAmount(byte fishid,short count)
    {
        Packet p = new Packet((int)ClientPackets.FisherBobAmountData);
        p.Write(fishid);
        p.Write(count);

        SendTcpData(p);
        p.Dispose();

    }
    public static void SendWearItem()
    {
        Packet p = new Packet((int)ClientPackets.WearItem);

        SendTcpData(p);
        p.Dispose();

    }

    public static void BuyItem(int ItemShopId)
    {
        Packet p = new Packet((int)ClientPackets.BuyItem);
        p.Write(ItemShopId);
        SendTcpData(p);
        p.Dispose();

    }
    public static void Craft(short item1,short item2)
    {
        Packet p = new Packet((int)ClientPackets.CraftRequest);
        p.Write(item1);
        p.Write(item2);

        SendTcpData(p);
        p.Dispose();

    }

    public static void TradeInput(short item1, short item2,short item3,short item4,short item1count,short item2count,short item3count,short item4count)
    {
        Packet p = new Packet((int)ClientPackets.Trade);
        p.Write((byte)1);

        p.Write(item1);
        p.Write(item1count);

        p.Write(item2);
        p.Write(item2count);

        p.Write(item3);
        p.Write(item3count);

        p.Write(item4);
        p.Write(item4count);

        SendTcpData(p);
        p.Dispose();

    }
    public static void SendDamageData(short BlockId)
    {
        Packet p = new Packet((int)ClientPackets.DamageData);
        p.Write(BlockId);
        SendTcpData(p);

        p.Dispose();

    }
    public static void ConfirmTrade()
    {
        Packet p = new Packet((int)ClientPackets.Trade);
        p.Write((byte)2);

        SendTcpData(p);
        p.Dispose();

    }
    public static void CancelTrade()
    {
        Packet p = new Packet((int)ClientPackets.Trade);
        p.Write((byte)3);

        SendTcpData(p);
        p.Dispose();

    }

    public static void SendVertifaction()
    {
        Packet p = new Packet((int)ClientPackets.Vertifaction);
        p.Write(GameManager.instance.Launguage);
        

        p.Write(GameManager.instance.Version);


        p.Write(SystemInfo.deviceUniqueIdentifier);

        SendTcpData(p);
        p.Dispose();

    }

    public static void DropItemRequest(short howmuch)
    {
        Packet p = new Packet((int)ClientPackets.DropItem);

        p.Write(howmuch);

        SendTcpData(p);
        p.Dispose();

    }
}

