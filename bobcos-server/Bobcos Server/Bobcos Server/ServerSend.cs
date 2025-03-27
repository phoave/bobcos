using GameServer;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Bobcos_Server
{
    class ServerSend
    {
        static private void SendTcpData(int _ToClient, Packet _packet)
        {



            _packet.WriteLength();



            Server.SendUDPData(Server.server.GetPeerById(_ToClient), _packet, LiteNetLib.DeliveryMethod.ReliableOrdered);

        }

        static private void SendUdpData(int _ToClient, Packet _packet)
        {



            _packet.WriteLength();


            Server.SendUDPData(Server.server.GetPeerById(_ToClient), _packet, LiteNetLib.DeliveryMethod.Unreliable);




        }


        static public void SendString(int _toclient, string message)
        {
            Packet packet = new Packet((int)ServerPackets.welcome);
            packet.Write(message);

            SendTcpData(_toclient, packet);

            packet.Dispose();

        }



        static public void SendLoading(int _toclient, byte operation, string info)
        {

            Packet packet = new Packet((int)ServerPackets.LoadingSignal);


            packet.Write((byte)operation);
            packet.Write(info);
            SendTcpData(_toclient, packet);
            packet.Dispose();

        }


        static public void SendResult(int _toclient, int enterin, string info)
        {

            Packet p = new Packet((int)ServerPackets.resultofenter);


            p.Write(enterin);
            p.Write(info);
            SendTcpData(_toclient, p);
            p.Dispose();


        }

        static public void SendMenu(int _toclient, List<string> content)
        {

            Packet t = new Packet((int)ServerPackets.Menu);


            foreach (string i in content)
            {

                t.Write(i);
            }
            SendTcpData(_toclient, t);
            t.Dispose();

        }
        static public void SendChat(int _toclient, string text)
        {

            Packet t = new Packet((int)ServerPackets.Chat);

            t.Write(text);

            SendTcpData(_toclient, t);
            t.Dispose();

        }

        static public void SpawnPlayer(int _toclient, string playerusername, bool spawnordespawn, int playerid)
        {
            Packet p = new Packet((int)ServerPackets.playerspawn);

            p.Write(playerusername);
            p.Write(spawnordespawn);
            p.Write(playerid);
            SendTcpData(_toclient, p);
            p.Dispose();


        }


        static public void SendPosition(int _toclient, int idofplayer, float x, float y, bool isjumping)
        {
            Packet p = new Packet((int)ServerPackets.PositionFromServer);




            p.Write(idofplayer);
            p.Write(x);
            p.Write(y);
            p.Write(isjumping);

            SendUdpData(_toclient, p);
            p.Dispose();

        }
        static public void SendPositionViaTCP(int _toclient, int idofplayer, float x, float y, bool isjumping)
        {
            Packet p = new Packet((int)ServerPackets.PositionFromServer);




            p.Write(idofplayer);
            p.Write(x);
            p.Write(y);
            p.Write(isjumping);

            SendTcpData(_toclient, p);
            p.Dispose();
        }

        static public void SendWorldDataFg(int _toclient, short[] data, string fgorbg)
        {
            Packet p = new Packet((int)ServerPackets.worlddata);


            p.Write(fgorbg); // which tile?
            foreach (short i in data)
            {
                p.Write(i);

            }
            p.Write(30431);

            SendTcpData(_toclient, p);
            p.Dispose();
        }

        static public void SendInventory(int _toclient, List<InventoryTile> inv)
        {
            Packet p = new Packet((int)ServerPackets.inventory);
            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_toclient].user.username.ToUpper()}.json"));

            if (Server.Clients[_toclient].user.World != null)
            {



                Logic.worlds[Server.Clients[_toclient].user.World.ToUpper()].SendApperanceToEveryoneExpectPlayer(acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, _toclient, acc.hat, acc.handitem, acc.badge);


                ServerSend.SendPlayerApprence(_toclient, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem, acc.badge);
            }
            foreach (InventoryTile i in inv)
            {

                p.Write((short)i.id);
                p.Write((short)i.count);

            }
            p.Write((short)13928);
            p.Write((short)13928);

            SendTcpData(_toclient, p);
            p.Dispose();

        }

        static public void SendBlockData(int _toclient, short itemsirasi, short itemid, string isbgorfg)
        {
            Packet p = new Packet((int)ServerPackets.block);
            p.Write(isbgorfg);

            p.Write((short)itemsirasi);
            p.Write((short)itemid);

            SendTcpData(_toclient, p);
            p.Dispose();

        }

        static public void SendBlockBrekingAnim(int _toclient, short blockpos, byte breaktilenumber)
        {
            Packet p = new Packet((int)ServerPackets.breakinganim);
            p.Write(blockpos);

            p.Write(breaktilenumber);

            SendTcpData(_toclient, p);
            p.Dispose();

        }
        static public void SendCash(int _toclient, int cash)
        {
            Packet p = new Packet((int)ServerPackets.SendCash);


            p.Write(cash);
            SendTcpData(_toclient, p);
            p.Dispose();


        }
        static public void SendWarning(int _toclient, string text)
        {
            Packet p = new Packet((int)ServerPackets.WarningMessage);


            p.Write(text);
            SendTcpData(_toclient, p);
            p.Dispose();


        }
        static public void SendItemInfo(int _toclient, string itemname, string iteminfo, bool istrashable, bool iswearable, bool isdroppable)
        {
            Packet p = new Packet((int)ServerPackets.ItemInfo);

            p.Write(itemname);
            p.Write(iteminfo);
            p.Write(istrashable);
            p.Write(iswearable);
            p.Write(isdroppable);

            SendTcpData(_toclient, p);
            p.Dispose();


        }

        static public void SendPlayerApprence(int _toclient, byte r, byte g, byte b, byte t, short shirt, short pants, short shoes, short backId, short HeadId, short PlayerId, short hatid, short handitem, byte badgeid)
        {
            Packet p = new Packet((int)ServerPackets.PlayerApperance);


            //Check before sending



            //Check items 

            //Get Inventory


            useraccount account;
            if (PlayerId == 0)
            {
                account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_toclient].user.username.ToUpper()}.json"));

            }
            else
            {
                account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[PlayerId].user.username.ToUpper()}.json"));

            }



            //Check shirt

            bool passedshirtver = false, passespantver = false, passedshoever = false, passedbackIdver = false, passesheadId = false, PasseshatId = false, passedhanditemver = false;
            foreach (InventoryTile i in account.inventory)
            {
                if (i.id == shirt)
                {
                    passedshirtver = true;
                }
                if (i.id == pants)
                {
                    passespantver = true;
                }
                if (i.id == shoes)
                {
                    passedshoever = true;
                }
                if (i.id == backId)
                {
                    passedbackIdver = true;
                }
                if (i.id == HeadId)
                {
                    passesheadId = true;
                }
                if (i.id == hatid)
                {
                    PasseshatId = true;
                }
                if (i.id == handitem)
                {
                    passedhanditemver = true;
                }
            }

            if (passedshirtver == false)
            {
                account.shirt = -1;
                shirt = -1;

            }
            if (passespantver == false)
            {
                account.pants = -1;
                pants = -1;

            }
            if (passedshoever == false)
            {
                account.shoes = -1;
                shoes = -1;

            }
            if (PasseshatId == false)
            {
                account.hat = -1;
                hatid = -1;
            }
            if (passesheadId == false)
            {
                account.head = -1;
                HeadId = -1;

            }
            if (passedbackIdver == false)
            {
                account.back = -1;
                backId = -1;

            }
            if (passedhanditemver == false)
            {
                account.handitem = -1;
                handitem = -1;

            }

            if (passedhanditemver == false || passedshirtver == false || passedshoever == false || passespantver == false || passesheadId == false || passedbackIdver == false || passedhanditemver == false || PasseshatId == false)
            {
                if (PlayerId == 0)
                {
                    File.WriteAllText($"accounts/{Server.Clients[(int)_toclient].user.username.ToUpper()}.json", JsonSerializer.Serialize(account));
                }
                else
                {
                    File.WriteAllText($"accounts/{Server.Clients[(int)PlayerId].user.username.ToUpper()}.json", JsonSerializer.Serialize(account));

                }
            }


            p.Write(r);
            p.Write(g);
            p.Write(b);
            p.Write(t);

            p.Write(shirt);
            p.Write(pants);
            p.Write(shoes);
            p.Write(backId);
            p.Write(HeadId);
            p.Write(hatid);

            p.Write(PlayerId);
            p.Write(handitem);
            p.Write(badgeid);



            SendTcpData(_toclient, p);
            p.Dispose();


        }
        static public void SendPlayerChatBubble(int _toclient, string text, int PlayerId)
        {
            Packet p = new Packet((int)ServerPackets.PlayerChatBubble);

            p.Write(text);
            p.Write(PlayerId);


            SendTcpData(_toclient, p);
            p.Dispose();


        }
        static public void SendShopData(int _toclient, ShopItem[] Data)
        {
            Packet p = new Packet((int)ServerPackets.ShopData);

            foreach (ShopItem i in Data)
            {
                p.Write(i.ItemName);
                p.Write(i.Price);
                p.Write(i.ShopId);
                p.Write(i.ItemID);
                p.Write(i.description);

            }

            p.Write("<END>");


            SendTcpData(_toclient, p);
            p.Dispose();


        }
        static public void SendAnim(int _toclient, byte animId, int PlayerId)
        {
            Packet p = new Packet((int)ServerPackets.Anim);

            p.Write(animId);
            p.Write(PlayerId);


            SendTcpData(_toclient, p);
            p.Dispose();


        }

        static public void SendBubble(int _toclient, int playerid, byte animid)
        {
            Packet p = new Packet((int)ServerPackets.Bubble);

            p.Write(playerid);
            p.Write(animid);


            SendTcpData(_toclient, p);
            p.Dispose();


        }

        static public void EnableTradeUI(int _toclient)
        {
            Packet p = new Packet((int)ServerPackets.Trade);

            p.Write((byte)1);


            SendTcpData(_toclient, p);
            p.Dispose();


        }

        static public void DisableTradeUI(int _toclient)
        {
            Packet p = new Packet((int)ServerPackets.Trade);

            p.Write((byte)2);


            SendTcpData(_toclient, p);
            p.Dispose();


        }

        static public void UpdateTradeUI(int _toclient, short item1, short item2, short item3, short item4, short item1count, short item2count, short item3count, short item4count)
        {
            Packet p = new Packet((int)ServerPackets.Trade);

            p.Write((byte)3);

            p.Write(item1);
            p.Write(item1count);

            p.Write(item2);
            p.Write(item2count);

            p.Write(item3);
            p.Write(item3count);

            p.Write(item4);
            p.Write(item4count);


            SendTcpData(_toclient, p);
            p.Dispose();


        }

        static public void SendDropDatas(int _toclient, List<DroppedItem> data)
        {
            Packet p = new Packet((int)ServerPackets.DropDatas);



            foreach (DroppedItem i in data)
            {
                p.Write((short)i.itemid);
                p.Write((short)i.itemcount);
                p.Write(i.Xpos);
                p.Write(i.Ypos);

            }
            p.Write((short)30431);

            SendTcpData(_toclient, p);
            p.Dispose();
        }
        static public void SendClientInfo(int _toclient)
        {
            Packet p = new Packet((int)ServerPackets.ClientInfo);




            p.Write(_toclient);

            SendTcpData(_toclient, p);
            p.Dispose();
        }

        static public void SendDisplayItemDatas(int _toclient, List<DisplayBlock> data)
        {
            Packet p = new Packet((int)ServerPackets.displayBlockItems);

            foreach (DisplayBlock i in data)
            {
                if (i != null)
                {
                    p.Write((int)i.itemid);
                    p.Write((int)i.xPos);
                    p.Write((int)i.yPos);
                }
            }
            p.Write((short)30431);

            SendTcpData(_toclient, p);
            p.Dispose();
        }

    }
}