using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Numerics;
using System.Text.Json;
using System.IO;
using System.Linq;
using GameServer;
using LiteNetLib;

namespace Bobcos_Server
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromclient,Packet _packet)
        {
            string i = _packet.ReadString();

           
           if (i == "S")
            {
               if(Server.Clients[_fromclient].user.ServerHelloSendedToClient)
                {
                    Server.Clients[_fromclient].user.ServerHelloReceivedFromClient = true;
                }else
                {
                }
            

           
            }

        }

        public static void Vertifacition(int _Fromcliemt, Packet _packet)
        {
            Console.WriteLine("Vertification success.");
            string Country = _packet.ReadString();
            string Version = _packet.ReadString();
            string HWID = _packet.ReadString();
            if(Version != Constants.version)
            {
                //Disconnect

                ServerSend.SendResult(_Fromcliemt, 0, "<color=red>Your version is outdated, Please install new version of bobcos.</color>");
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
            if(_Fromcliemt == 0)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                EventBasedNetListener clientls = new EventBasedNetListener();
                NetManager client = new NetManager(clientls);
                client.Start();
                client.Connect("127.0.0.1", Server.Port, "KEYSERVER22");
            }
            
            Server.Clients[_Fromcliemt].user = new User() { id = _Fromcliemt };
            Server.Clients[_Fromcliemt].user.hwid = HWID;
            Server.Clients[_Fromcliemt].user.ip = Server.server.GetPeerById(_Fromcliemt).EndPoint.Address.ToString();
           Server.Clients[_Fromcliemt].user.PassedVertifaction = true;
            Server.Clients[_Fromcliemt].user.PlayerSystemLaunguageCountry = Country;
            //Send information to client to use UDP connection

            ServerSend.SendClientInfo(_Fromcliemt);
            

        }

        public static void HandleAccountandworld(int _Fromcliemt, Packet _packet)
        {

            if (Server.Clients[_Fromcliemt].user == null)
            {
                Server.server.DisconnectPeer(Server.Clients[_Fromcliemt].peer);
            }

            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }

            //check

            



            //




            string accountname;
                string password;
                string email;
                string worldname;

            byte selection = _packet.ReadByte();

            switch (selection)
            {

                case 1:
                    //login
                    accountname = _packet.ReadString();
                     password = _packet.ReadString();



                    char[] AllowedCharacters = new char[] {'İ','W', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'Y', 'Z', 'Q', 'X', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };



                    foreach (char cha in accountname.ToUpper())
                    {
                        if (!AllowedCharacters.Contains(cha))
                        {
                            // contains bad string
                            ServerSend.SendWarning(_Fromcliemt, "<color=red>Only use A-Z a-z 0-9 characters</color>");

                            return;
                        }

                    }



                    int b =   Logic.TryToLogin(accountname, password);

                    if(b == 0)
                    {
                        ServerSend.SendWarning(_Fromcliemt,  "<color=red>Wrong password!</color>");
                    }else if(b == 1)
                    {
                        //check account ban
                        useraccount account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + accountname.ToUpper() + ".json"));

                        if(DateTime.Now < account.BanExpireTime)
                        {
                            ServerSend.SendWarning(_Fromcliemt, $"<color=red>Account is banned. Your ban will expire at {account.BanExpireTime}. Ban Reason: {account.BanReason}</color>");

                            ServerSend.SendLoading(_Fromcliemt, 0, "Account.");
                            return;
                        }

                        //maitance

                        // if(account.StaffLevel < 2)
                        // {
                        // ServerSend.SendWarning(_Fromcliemt, "The server is on maintenance right now");
                        // return;
                        // }


                        ServerSend.SendResult(_Fromcliemt, 1, "<color=green>Logged in successfully!</color>");
                        //Send news

                        ServerSend.SendLoading(_Fromcliemt, 0, "Account.");

                        Server.Clients[_Fromcliemt].user.username = accountname;
                        useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + accountname.ToUpper() + ".json"));
                        Server.Clients[_Fromcliemt].user.realusername = acc.username;

                        ServerSend.SendChat(_Fromcliemt, "Welcome to Bobcos. Visit World <color=green>Bobcos</color> To get rewards.");

                        ServerSend.SendCash(_Fromcliemt, Logic.GetGems(Server.Clients[_Fromcliemt].user.username.ToUpper()));
                    }
                    else if(b == 2)
                    {
                        ServerSend.SendWarning(_Fromcliemt,  "<color=yellow>This account doesnt exist, create it now!</color>");

                    }
                    else if (b == 3)
                    {
                        ServerSend.SendWarning(_Fromcliemt, "<color=red>This account is already online! kicked player.</color>");

                    }
                    ServerSend.SendLoading(_Fromcliemt, 0, "Account.");

                    break;
                case 2:
                    //Register
                     accountname = _packet.ReadString();

                    if(accountname.Length > 32)
                    {
                        ServerSend.SendWarning(_Fromcliemt,  "<color=red>username cant be longer than 32 chartacters!</color>");

                        return;
                    }
                    char[] AllowedCharacters2 = new char[] { 'İ', 'W', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'Y', 'Z', 'Q', 'X', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };



                    foreach (char cha in accountname.ToUpper())
                    {
                        if (!AllowedCharacters2.Contains(cha))
                        {
                            // contains bad string
                            ServerSend.SendWarning(_Fromcliemt,  "<color=red>Only use A-Z 0-9 characters</color>");
                            ServerSend.SendLoading(_Fromcliemt, 0, "Account.");

                            return;
                        }

                    }

                    password = _packet.ReadString();
                    email = _packet.ReadString();
                    int c = Logic.TryToCreateAccount(accountname,password,email);
                    if (c == 1)
                    {
                        ServerSend.SendWarning(_Fromcliemt,  "<color=green>Account created!</color>");
                    }
                    else if (c == 2)
                    {

                        ServerSend.SendWarning(_Fromcliemt,  "<color=red>This account already exists.!</color>");

                    }
                    else if (c == 3)
                    {
                        ServerSend.SendWarning(_Fromcliemt,  "<color=red>Password or username cant be shorter than 3 chartacters!</color>");

                    }
                    ServerSend.SendLoading(_Fromcliemt, 0, "Account.");

                    break;

                case 3:
                    //Enter world
                    worldname = _packet.ReadString();
                    char[] AllowedCharacters3 = new char[] { 'w', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'r', 's', 't', 'u', 'v', 'y', 'z', 'q', 'x', 'W', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'R', 'S', 'T', 'U', 'V', 'Y', 'Z', 'Q', 'X', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

                    string newworldname = "";


                    foreach (char cha in worldname)
                    {
                        if (!AllowedCharacters3.Contains(cha) )
                        {
                            // contains bad string
                            ServerSend.SendWarning(_Fromcliemt, "<color=red>Only use A-Z 0-9 characters</color>");

                            return;
                        }else
                        {
                            if(cha == 'i')
                            {
                                newworldname = newworldname + "I";
                            }else
                            {
                                newworldname = newworldname + cha;

                            }
                        }

                    }
                    if(newworldname == "")
                    {
                        return;
                    }

                    ServerSend.SendString(_Fromcliemt, "AD2");

                    object[] data = new object[4];

                    data[0] = _Fromcliemt;
                    data[1] = newworldname.ToUpper();
                    data[2] = false;


                    Logic.JoinToWorld(data);

                    ServerSend.SendLoading(_Fromcliemt, 0, "World Loaded");
                    break;

            }

           
        }

        public static void FisherBobAmountDataReceived(int _fromClient, Packet _packet)
        {
            short FishId = _packet.ReadByte();
            short Amount = _packet.ReadShort();

            //check player inventory




            if(Amount <= 0)
            {
                return;
            }
            if(itemdata.items[FishId].itemtype != "FISH")
            {
                return;
            }

           if( Logic.CheckItemInInventory(Server.Clients[_fromClient].user.username, FishId, Amount))
            {
                Logic.AddItemToInventory(Server.Clients[_fromClient].user.username, FishId, -Amount);
                Logic.GetInventoryAndSend(_fromClient, Server.Clients[_fromClient].user.username);
                Logic.AddGems(Server.Clients[_fromClient].user.username, itemdata.items[FishId].fishGemAmount * Amount);
                ServerSend.SendChat(_fromClient, $"<color=green>You successfully sold your fish(s) {itemdata.items[FishId].itemname} {Amount} x for {Amount * itemdata.items[FishId].fishGemAmount} Gems.");
                ServerSend.SendCash(_fromClient, Logic.GetGems(Server.Clients[_fromClient].user.username));
            }
            else
                {
                ServerSend.SendWarning(_fromClient, "<color=red>Error: You dont have enough item</color>");

            }





        }

        public static void PassChangeRequest(int _fromClient, Packet _packet)
        {
            if(Server.Clients[_fromClient].user.username != null)
            {

                //player is logged





                byte readbyte = _packet.ReadByte();


                if (readbyte == 0)
                {




                    string oldpass = _packet.ReadString();
                    string newpass = _packet.ReadString();


                    //get player informations
                    useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_fromClient].user.username.ToUpper()}.json"));

                    if (i.password == oldpass)
                    {
                        //change password
                        i.password = newpass;
                        File.WriteAllText($"accounts/{Server.Clients[_fromClient].user.username.ToUpper()}.json", JsonSerializer.Serialize(i));


                    }
                    else
                    {
                        //send password cant change message
                    }
                }
               
            }
            else
            {
                byte readbyte = _packet.ReadByte();

             
                string mail = _packet.ReadString();
                string username = _packet.ReadString();


                //get player informations

                //MailPart.GetAccountPasswordAndSendToMail(username, mail);

            }
        }

        public static void ItemDropRequest(int _Fromcliemt, Packet _packet)
        {



            short itemcount = _packet.ReadShort();

            string worldname = Server.Clients[_Fromcliemt].user.World.ToUpper();



            if (Server.Clients[_Fromcliemt].user.Trade != null)
            {

                ServerSend.SendChat(_Fromcliemt, "You cant drop items while trading.");
                return;
            }

            if(itemcount < 1)
            {
                return;
            }


            if(worldname == null)
            {
                return;
            }
            if(!itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].istradable)
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, itemcount))
            {
                return;
            }
           

            foreach (InventoryTile i in Logic.GetInventory(Server.Clients[_Fromcliemt].user.username.ToUpper()))
            {
                if(i.count >= itemcount && Server.Clients[_Fromcliemt].user.CurrentSelecteditem == i.id)
                {
                    if (Server.Clients[_Fromcliemt].user.isFacingLeft)
                    {
                     if(   DroppingSystem.DropItem(worldname, Server.Clients[_Fromcliemt].user.CurrentSelecteditem, (int)itemcount, Server.Clients[_Fromcliemt].user.xpos - 0.8f, Server.Clients[_Fromcliemt].user.ypos - 0.3f, _Fromcliemt) )
                        {

                            Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -itemcount);
                            DiscordPart.SendItemDrop(Server.Clients[_Fromcliemt].user.username, Server.Clients[_Fromcliemt].user.World, itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemname, itemcount);

                        }


                    }
                    else
                    {

                      if(  DroppingSystem.DropItem(worldname, Server.Clients[_Fromcliemt].user.CurrentSelecteditem, (int)itemcount, Server.Clients[_Fromcliemt].user.xpos + 0.8f, Server.Clients[_Fromcliemt].user.ypos - 0.3f, _Fromcliemt) == true)
                        {

                            Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -itemcount);
                            DiscordPart.SendItemDrop(Server.Clients[_Fromcliemt].user.username, Server.Clients[_Fromcliemt].user.World, itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemname, itemcount);

                        }

                    }




                    Logic.GetInventoryAndSend(_Fromcliemt, Server.Clients[_Fromcliemt].user.username.ToUpper());
                }

            }




            
        }

        public static void TradeUpdate(int _Fromcliemt, Packet _packet)
        {
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
            byte operation = _packet.ReadByte();




            if(operation == 1)
            {

                //Input Update
                short item1 = _packet.ReadShort();
                short item1Count = _packet.ReadShort();

                short item2 = _packet.ReadShort();
                short item2Count = _packet.ReadShort();

                short item3 = _packet.ReadShort();
                short item3Count = _packet.ReadShort();

                short item4 = _packet.ReadShort();
                short item4Count = _packet.ReadShort();


                if(Server.Clients[_Fromcliemt].user.Trade == null)
                {
                    //trade is null????
                    Console.WriteLine("Trade is null");
                }

                if (Server.Clients[_Fromcliemt].user.Trade != null)
                {
                    Server.Clients[_Fromcliemt].user.Trade.AddItems(_Fromcliemt, item1, item1Count, item2, item2Count, item3, item3Count, item4, item4Count);
                }

            }
            if (operation == 2)
            {
                //Try Accept

                if (Server.Clients[_Fromcliemt].user.Trade != null)
                {
                    Server.Clients[_Fromcliemt].user.Trade.TryCompleteTrade(_Fromcliemt);
                }
            }
            if(operation == 3)
            {
                //cancel trade
                try
                {
                    if (Server.Clients[_Fromcliemt].user.Trade.player2id != 0)
                    {
                        ServerSend.DisableTradeUI(Server.Clients[_Fromcliemt].user.Trade.player2id);


                        if (Server.Clients[_Fromcliemt].user.Trade.player2id != 0)
                        {
                            Server.Clients[Server.Clients[_Fromcliemt].user.Trade.player2id].user.Trade = null;


                        }
                    }
                }
                catch
                {

                }
                try
                {


                    if(Server.Clients[_Fromcliemt].user.Trade != null)
                    {
                        if (Server.Clients[_Fromcliemt].user.Trade.player1id != 0)
                        {
                            ServerSend.DisableTradeUI(Server.Clients[_Fromcliemt].user.Trade.player1id);

                            Server.Clients[Server.Clients[_Fromcliemt].user.Trade.player1id].user.Trade = null;

                        }
                    }
                    
                }
                catch
                {

                }



            }
        }

        public static void CraftReq(int _Fromcliemt, Packet _packet)
        {
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
            short item1 = _packet.ReadShort();
            short item2 = _packet.ReadShort();


            if(Server.Clients[_Fromcliemt].user.worldCraftDataOrder == -1)
            {
                if (CraftingSystem.CheckIfCrafting(_Fromcliemt))
                {
                    CraftingSystem.TryToGetCraftedItem(_Fromcliemt);

                }
                else
                {
                    CraftingSystem.BeginCrafting(_Fromcliemt, item1, item2);
                }
            }else
            {

                if (CraftingSystem.CheckIfCrafting(_Fromcliemt, Server.Clients[_Fromcliemt].user.worldCraftDataOrder))
                {
                    CraftingSystem.TryToGetCraftedItem(_Fromcliemt, Server.Clients[_Fromcliemt].user.worldCraftDataOrder);

                }
                else
                {
                    CraftingSystem.BeginCrafting(_Fromcliemt, item1, item2, Server.Clients[_Fromcliemt].user.worldCraftDataOrder);
                }

            }

         

        }

        public static void BuyItem(int _Fromcliemt, Packet _packet)
        {
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
            int itemShopId = _packet.ReadInt();
            Shop.BuyItem(itemShopId, _Fromcliemt);
        }

        public static void ItemWear(int _Fromcliemt, Packet _packet)
        {
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
            Logic.WearItem(_Fromcliemt);
        }
        public static void DamageDataReceived(int _Fromcliemt, Packet _packet)
        {

            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
        short blockid =  _packet.ReadShort();

            if (Server.Clients[_Fromcliemt].user != null)
            {
                Server.Clients[_Fromcliemt].user.Damage(blockid);
            }



        }
        public static void StringFromClient(int _Fromcliemt, Packet _packet)
        {
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
            string message = _packet.ReadString();


            if (message == "CH1")
            {
                //enable chatting baloon
                if(Server.Clients[_Fromcliemt].user.World != null)
                {
                    Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendChattingBubbleToEveryone(_Fromcliemt, 1);
                }
            }
            if (message == "CH0")
            {
                //disable chatting baloon
                if (Server.Clients[_Fromcliemt].user.World != null)
                {
                    Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendChattingBubbleToEveryone(_Fromcliemt, 0);
                }
            }



            if (message == "RES")
            {
             
                
                if(Server.Clients[_Fromcliemt].user.World != null)
                {
                    int[] pos = Logic.GetWhiteDoorPos(Server.Clients[_Fromcliemt].user.World.ToUpper());
                    Server.Clients[_Fromcliemt].user.SetPosWithoutDistanceCheck(pos[0] , pos[1] + 0.4f,0,false);

                    ServerSend.SendPosition(_Fromcliemt, 0, pos[0] , pos[1] + 0.4f, false);
                    Logic.worlds[Server.Clients[_Fromcliemt].user.World].SendPositionToEveryonexpectClient(pos[0] , pos[1] + 0.4f, _Fromcliemt,false);




                }



            }


            if (message == "rewardsw")
            {



                object[] data = new object[4];

                data[0] = _Fromcliemt;
                data[1] = "bobcos".ToUpper();
                data[2] = false;


                Logic.JoinToWorld(data);


            }

            if (message == "randomw")
            {

                //count all worlds
                List<string> worlds = new List<string>();

                foreach (World w in Logic.worlds.Values)
                {
                    if (w.Playersinworld.Count != 0)
                    {
                        worlds.Add(w.WorldName.ToLower());
                    }
                }


                Random rand = new Random();








                try
                {

                    ServerSend.SendString(_Fromcliemt, "AD2");

                    object[] data = new object[4];

                    data[0] = _Fromcliemt;
                    data[1] = worlds[rand.Next(0, worlds.Count)].ToUpper();
                    data[2] = false;


                    Logic.JoinToWorld(data);


                }
                catch
                {

                }











            }

                if (message == "UI_MENU3_LEAVEWORLD")
            {
                //leave world and goto menu
                if (Server.Clients[_Fromcliemt].user.World != "")
                {
                    Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].LeaveWorld(_Fromcliemt);
                    ServerSend.SendResult(_Fromcliemt, 1, "You have leaved world.");

                }
            }
            if (message == "UI_SHOP")
            {
                if (Server.Clients[_Fromcliemt].user.World != "")
                {
                    Shop.SendShopData(_Fromcliemt);

                }
            }
            if (message == "UI_SHOW_CRAFT_REQ")
            {
                Server.Clients[_Fromcliemt].user.worldCraftDataOrder = -1;

                if (!CraftingSystem.CheckIfCrafting(_Fromcliemt))
                {
                    ServerSend.SendString(_Fromcliemt, "UI_SHOW_CRAFT");
                }else
                {

                    CraftingSystem.TryToGetCraftedItem(_Fromcliemt);

                }

            }
            if(message == "AD_212")
            {
                useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_Fromcliemt].user.username.ToUpper()}.json"));

                if(i.AdsLeftWatch > 0)
                {
                    Random rand = new Random();
                    int randomgemcount = rand.Next(0, 1000);
                    Logic.AddGems(Server.Clients[_Fromcliemt].user.username.ToUpper(), randomgemcount);

                    ServerSend.SendCash(_Fromcliemt, Logic.GetGems(Server.Clients[_Fromcliemt].user.username.ToUpper()));
                    ServerSend.SendChat(_Fromcliemt, $"<color=green>Thank you for watching ad! You are rewarded with {randomgemcount} gems.</color>");
                    useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_Fromcliemt].user.username.ToUpper()}.json"));


                   
                        acc.AdsLeftWatch--;
                    acc.ADRefresherTime = DateTime.Now.AddHours(12);
                        string dataofacc2 = JsonSerializer.Serialize(acc);

                        File.WriteAllText("accounts/" + Server.Clients[_Fromcliemt].user.username.ToUpper() + ".json", dataofacc2);
                   

                }
             
            }
        }

        public static void BlockEditFromClient(int _Fromcliemt, Packet _packet)
        {
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }


            bool isaccessedplayer = false;
            Thread b = new Thread(new ParameterizedThreadStart(BlockOperationCoolDown));
            b.Start(_Fromcliemt);


            if (!Server.Clients[_Fromcliemt].user.CanBreakBlock)
            {
                return;
            }


            short sira = _packet.ReadShort();
            //Check world name
            if(Server.Clients[_Fromcliemt].user.World == null)
            {
                return;
            }
            string worldname = Server.Clients[_Fromcliemt].user.World;

            if(worldname == null)
            {
                return;
            }


            Vector2 playerpos = new Vector2(Server.Clients[_Fromcliemt].user.xpos, Server.Clients[_Fromcliemt].user.ypos);
            Vector2 Blockpos = new Vector2(WorldDataConverter.ConvertComplexidtopos(sira)[0], WorldDataConverter.ConvertComplexidtopos(sira)[1]);

          if(!  Logic.CheckItemInInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, 0))
            {
                return;
            }

          if(Vector2.Distance(playerpos, Blockpos) > 3.6f)
            {
                return;
            }


            useraccount i12 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_Fromcliemt].user.username.ToUpper()}.json"));
            if (i12.handitem == 83 && Server.Clients[_Fromcliemt].user.iscatchedfish) //rod detected
            {
                FishingSystem.ReceiveReward(_Fromcliemt);
                return;
            }

            if (i12.handitem == 83 && Logic.ReadWorldFg(worldname)[sira] == 81) //rod detected
            {
                FishingSystem.StartFishing(_Fromcliemt, Server.Clients[_Fromcliemt].user.CurrentSelecteditem);
            }

            if (itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "GOLDENLOOT")
            {
                Logic.AddGems(Server.Clients[_Fromcliemt].user.username.ToUpper(), 5000);

                useraccount i1 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_Fromcliemt].user.username.ToUpper()}.json"));

                ServerSend.SendCash(_Fromcliemt, i1.cash);

                ServerSend.SendChat(_Fromcliemt, "You got 5000 Gems from Golden loot!");

                
                Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -1);
                Logic.GetInventoryAndSend(_Fromcliemt, Server.Clients[_Fromcliemt].user.username.ToUpper());
                return;
            }



            Logic.worlds[worldname.ToUpper()].SendPunch(_Fromcliemt);

            if(worldname != "")




            {

                if(itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "WRENCH")
{
                    if (Logic.ReadWorldFg(worldname)[sira] == 0)
                    {
                        int blockid = Logic.ReadWorldBg(worldname)[sira];

                        if (itemdata.items[Logic.ReadWorldFg(worldname)[sira]].itemtype == "DISPLAYBLOCK")
                        {
                            return;
                        }
                    }
                    else
                    {
                        int blockid = Logic.ReadWorldFg(worldname)[sira];

                        if (itemdata.items[Logic.ReadWorldFg(worldname)[sira]].itemtype == "DISPLAYBLOCK")
                        {
                            return;
                        }

                        if (blockid == 47)
                        {


                            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_Fromcliemt].user.username.ToUpper()}.json"));


                            if(i.ADRefresherTime < DateTime.Now)
                            {
                                i.AdsLeftWatch = 3;
                                string dataofacc = JsonSerializer.Serialize(i);

                                File.WriteAllText("accounts/" + Server.Clients[_Fromcliemt].user.username.ToUpper() + ".json", dataofacc);
                            }

                            if (i.AdsLeftWatch > 0)
                            {

                                ServerSend.SendString(_Fromcliemt, "AD");
                                ServerSend.SendChat(_Fromcliemt, "Please wait...");
                            }
                            else
                            {
                                ServerSend.SendChat(_Fromcliemt, "You can only watch 3 ads per 12 hours.");

                            }




                        }
                        if (blockid == 107)
                        {




                            ServerSend.SendString(_Fromcliemt, "FBU");




                        }
                    }




                   

                }

                //Check permissions
                try
                {
                    if (Server.Clients[_Fromcliemt].user.username.ToUpper() != Logic.GetWorldOwnerName(worldname.ToUpper()))
                    {

                        if (Logic.GetWorldOwnerName(worldname.ToUpper()) != null)
                        {
                            //Theres owner
                            //Check if has access


                            if (Logic.GetWorldAccessedPlayers(worldname.ToUpper()).Contains(Server.Clients[_Fromcliemt].user.username.ToUpper()))
                            {
                                //player has access 

                                isaccessedplayer = true;
                            }
                            else
                            {
                                //player doesnt have access   ServerSend.SendChat(_Fromcliemt, "<color=red>You dont have access in this world!</color>");






                                return;
                            }








                        }


                    }

                }catch
                {
                    return;
                }




                // TODO: Check item type here

                if (itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "FIST")
                {
                    //Todo: add if theres not air in selected block so it does not break empty

                    //if fg is 0 dont break if its not break. if fg is 0 check bg if bg is 0 dont do anything.
                    int pickaxehelpercount = 0;

                    useraccount accountOfUser = JsonSerializer.Deserialize<useraccount>(File.ReadAllText(("accounts/" + Server.Clients[_Fromcliemt].user.username.ToUpper() + ".json")));


                    if (accountOfUser.handitem == 31) // pickaxe

                    {
                        pickaxehelpercount = 1;
                    }
                    if (accountOfUser.handitem == 74) // ketaru

                    {
                        pickaxehelpercount = 7;
                    }
                    if (Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira] == 0)
                    {
                        // dont break fg try check bg
                        if (Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira] == 0)
                        {
                            // Dont do anything.
                        }else
                        {
                          

                            // break bg
                            //Check health of block if its equal to destroy health set it to 0 and destory
                          
                                //add one more and if its equal destroy
                                Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira]++;
                                int old = Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira];
                            if(pickaxehelpercount == 1 && itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].blockhealth <= 1)
                            {
                                pickaxehelpercount = 0;
                            }
                            if (pickaxehelpercount == 7 ||  Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira] == itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].blockhealth -pickaxehelpercount)
                                {
                                //destory it
                                //Give gem to player
                                //give xp to player
                                if(pickaxehelpercount == 7)
                                {
                                    pickaxehelpercount = 0;
                                }

                                Logic.AddXp(Server.Clients[_Fromcliemt].user.username.ToUpper(), itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].xpCount, _Fromcliemt);


                                try
                                {


                                    string username = Server.Clients[_Fromcliemt].user.username.ToUpper();

                                    if (itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].RandomPick != null)
                                    {
                                        int itemToGive = itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].RandomPick.itemToGive;

                                        int itemCount = itemdata.items[itemToGive].RandomPick.Pick();
                                        if (itemCount != 0)
                                        {


                                            int xpos = WorldDataConverter.ConvertComplexidtopos(sira)[0];
                                            int ypos = WorldDataConverter.ConvertComplexidtopos(sira)[1];

                                            DroppingSystem.DropItem(worldname, itemToGive, itemCount, xpos, ypos + 0.65f, 0);
                                        }
                                    }

                                }
                                catch
                                {

                                }

                                Random rand = new Random();
                                int selectedgems = rand.Next(itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].mingem, itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].maxgem);
                                Logic.AddGems(Server.Clients[_Fromcliemt].user.username.ToUpper(), selectedgems);
                                ServerSend.SendCash(_Fromcliemt, Logic.GetGems(Server.Clients[_Fromcliemt].user.username.ToUpper()));

                                //

                                Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira] = 0;
                                    Logic.EditWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper(), sira, 0);
                                    Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockToEveryoneinworld(sira, (short)0, "bg");
                                }




                            Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockBreakToEveryoneInWorld(sira, (byte)Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira]);
                        



                            Thread t = new Thread(() => Logic.CheckIfStillOld(Server.Clients[_Fromcliemt].user.World.ToUpper(), old, sira));
                            t.Start();




                        }
                    }
                    else
                    {
                        // break fg
                       
                            Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira]++;
                            int old = Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira];



                        if(Logic.ReadWorldFg(worldname.ToUpper())[sira] == itemdata.worldlockid)
                        {
                            if(isaccessedplayer)
                            {
                                ServerSend.SendChat(_Fromcliemt, "Only owner can break locks.");
                                //Accessed players cant break
                                return;
                            }
                        }

                        if (itemdata.items[Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].itemtype == "DISPLAYBLOCK")
                        {
                            List<DisplayBlock> list = JsonSerializer.Deserialize<List<DisplayBlock>>(File.ReadAllText("displayBlockWorldData/" + worldname.ToUpper() + ".json"));

                            // break
                            if (list[sira] != null)
                            {
                                if (list[sira].itemid > 0)
                                {
                                    if (isaccessedplayer)
                                    {
                                        return;
                                    }
                                    if (Logic.CanTakeItem(accountOfUser.username, list[sira].itemid, 1))
                                    {
                                        Logic.AddItemToInventory(accountOfUser.username, list[sira].itemid, 1);
                                        Logic.GetInventoryAndSend(_Fromcliemt, accountOfUser.username);

                                        list[sira].itemid = 0;
                                        list[sira].xPos = 0;
                                        list[sira].yPos = 0;

                                        try
                                        {
                                            string updateText = JsonSerializer.Serialize(list);
                                            File.WriteAllText("displayBlockWorldData/" + worldname.ToUpper() + ".json", updateText);
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex.Message + ex.StackTrace);
                                        }

                                        ServerSend.SendChat(_Fromcliemt, "<color=#22FF00FF>Returned your displayed item to your inventory.</color>");

                                        Logic.worlds[worldname].SendDisplayBlockData();
                                    }
                                    else
                                    {
                                        ServerSend.SendChat(_Fromcliemt, "<color=#FF0000FF>You don't have space to return your item, block can't be broken</color>");
                                        return;
                                    }
                                }
                            }
                        }


                        //ITEM 30 CRAFTING MACHINE IS WORKING 
                        if (Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira] == 30)
                        {
                            //item 30 detected check inside

                            if (CraftingSystem.CheckIfCrafting(_Fromcliemt, sira))
                            {
                                ServerSend.SendChat(_Fromcliemt, "Crafting machine is still crafting, If you  break it, you will lose items inside it.");
                            }
                        }

                        if (pickaxehelpercount == 1 && itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].blockhealth <= 1)
                        {
                            pickaxehelpercount = 0;
                        }
                        //
                        
                        if (pickaxehelpercount == 7 || Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira] == itemdata.items[Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].blockhealth - pickaxehelpercount)
                            {
                            if (pickaxehelpercount == 7)
                            {
                                pickaxehelpercount = 0;
                            }

                            //destory it
                            Logic.AddXp(Server.Clients[_Fromcliemt].user.username.ToUpper(), itemdata.items[Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].xpCount, _Fromcliemt);

                            //ITEM 30 CRAFTING MACHINE IS WORKING 
                            if (Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira] == 30)
                            {
                                //item 30 detected check inside

                             if(CraftingSystem.CheckIfCrafting(_Fromcliemt,sira))
                                {
                                    WorldCraftData acc = JsonSerializer.Deserialize<WorldCraftData>(File.ReadAllText("worldcraftdata/" + Server.Clients[_Fromcliemt].user.World.ToUpper() + ".json"));
                                    acc.Data[sira].resultItem = 0;
                                    acc.Data[sira].recipeid = 0;
                                    acc.Data[sira].craftDate = DateTime.Now;
                                    string dataofacc = JsonSerializer.Serialize(acc);

                                    File.WriteAllText("worldcraftdata/" + Server.Clients[_Fromcliemt].user.World.ToUpper() + ".json", dataofacc);

                                }
                            }


                            //



                            //Get some random block
                            try {


                                string username = Server.Clients[_Fromcliemt].user.username.ToUpper();
                                if (itemdata.items[Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].RandomPick != null)
                                {
                                    int itemToGive = itemdata.items[Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].RandomPick.itemToGive;

                                    int itemCount = itemdata.items[itemToGive].RandomPick.Pick();


                                    if (itemCount != 0)
                                    {



                                        int xpos = WorldDataConverter.ConvertComplexidtopos(sira)[0];
                                        int ypos = WorldDataConverter.ConvertComplexidtopos(sira)[1];

                                        DroppingSystem.DropItem(worldname, itemToGive, itemCount, xpos, ypos + 0.65f, 0);
                                    }
                                }

                            }
                            catch
                            {

                            }



                            Random rand = new Random();
                            int selectedgems =  rand.Next(itemdata.items[Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].mingem, itemdata.items[Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira]].maxgem);
                         
                            Logic.AddGems(Server.Clients[_Fromcliemt].user.username.ToUpper(), selectedgems);
                            ServerSend.SendCash(_Fromcliemt, Logic.GetGems(Server.Clients[_Fromcliemt].user.username.ToUpper()));

                            Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira] = 0;

                            if(Logic.ReadWorldFg(worldname.ToUpper())[sira] == itemdata.worldlockid)
                            {
                                Logic.SetWorldOwner(worldname.ToUpper(), null);
                                ServerSend.SendChat(_Fromcliemt, "You are not owning this world anymore.");

                                Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), 28, 1);
                                Logic.GetInventoryAndSend(_Fromcliemt, Server.Clients[_Fromcliemt].user.username.ToUpper());
                            }


                                Logic.EditWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper(), sira, 0);
                                Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockToEveryoneinworld(sira, (short)0, "fg");

                            }

                        Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockBreakToEveryoneInWorld(sira, (byte)Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].HealthOfBlocks[sira]);
                        //Check if its still old 

                        Thread t = new Thread(() => Logic.CheckIfStillOld(Server.Clients[_Fromcliemt].user.World.ToUpper(), old, sira));
                        t.Start();

                    }





                }
                if (itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "NUKE")
                {
                  
                    string worldKey = Server.Clients[_Fromcliemt].user.World.ToUpper();

                    for (int blockIndex = 0; blockIndex < Logic.worlds[worldKey].HealthOfBlocks.Length; blockIndex++)
                    {
                        int blockId = Logic.ReadWorldFg(worldKey)[blockIndex];

                       
                        if (blockId == 5 || blockId == 11 || blockId == 28 || blockId == 110 || blockId == 0)
                            continue;

                        Logic.worlds[worldKey].HealthOfBlocks[blockIndex] = 0;

                        Logic.EditWorldFg(worldKey, blockIndex, 0);
       
                        Logic.EditWorldBg(worldKey, blockIndex, 0);

                        // Send block destruction to all clients instantly
                        Logic.worlds[worldKey].SendBlockToEveryoneinworld((short)blockIndex, (short)0, "fg");
                        Logic.worlds[worldKey].SendBlockToEveryoneinworld((short)blockIndex, (short)0, "bg");
                    }

                   
                    Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -1);

        
                    ServerSend.SendChat(_Fromcliemt, "The world has been destroyed with the NUKK!");
                }


                else if (itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "BLOCK")
                {

                    //Todo: if selected block is not 0, cancel block putting.
                   if(Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira] != 0)
                    { return; }


                    // player is in world
                    //Check inventory item count, if theres count -1 it
                    List<InventoryTile> inventory = Logic.GetInventory(Server.Clients[_Fromcliemt].user.username);

                    foreach (InventoryTile i in inventory)
                    {
                        if (Server.Clients[_Fromcliemt].user.CurrentSelecteditem == i.id)
                        {
                            // item found check count
                            if (i.count > 0)
                            {
                                //Change world data and remove 1 block from inventory




                                Logic.EditWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper(), sira, Server.Clients[_Fromcliemt].user.CurrentSelecteditem);
                                Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -1);

                                //Send block information to players in world 
                                Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockToEveryoneinworld(sira, (short)i.id,"fg");

                                //Send inventory data to player
                                Logic.GetInventoryAndSend(_Fromcliemt, Server.Clients[_Fromcliemt].user.username.ToUpper());
                            }
                        }
                    }
                }
                else if (itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "DISPLAYBLOCK")
                {
                   

                   if (itemdata.items[Logic.ReadWorldFg(worldname)[sira]].itemtype == "DISPLAYBLOCK")
                    {
                        DisplayBlockSystem.PlaceDisplayBlockItem(_Fromcliemt, sira, Server.Clients[_Fromcliemt].user.CurrentSelecteditem);
                        return;
                    }

                    if (Logic.ReadWorldFg(worldname)[sira] == 0)
                    {
                        // player is in world
                        //Check inventory item count, if theres count -1 it
                        List<InventoryTile> inventory = Logic.GetInventory(Server.Clients[_Fromcliemt].user.username);

                        foreach (InventoryTile i in inventory)
                        {
                            if (Server.Clients[_Fromcliemt].user.CurrentSelecteditem == i.id)
                            {
                                // item found check count
                                if (i.count > 0)
                                {
                                    //Change world data and remove 1 block from inventory
                                    Logic.EditWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper(), sira, Server.Clients[_Fromcliemt].user.CurrentSelecteditem);
                                    Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -1);
                                    //Send block information to players in world 
                                    Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockToEveryoneinworld(sira, (short)i.id, "fg");
                                    //Send inventory data to player
                                    Logic.GetInventoryAndSend(_Fromcliemt, Server.Clients[_Fromcliemt].user.username.ToUpper());

                                }
                            }
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                else if (itemdata.items[Logic.ReadWorldFg(worldname)[sira]].itemtype == "DISPLAYBLOCK")
                {
                    DisplayBlockSystem.PlaceDisplayBlockItem(_Fromcliemt, sira, Server.Clients[_Fromcliemt].user.CurrentSelecteditem);
                    return;
                }
                else if (itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "BGBLOCK")
                {

                    //Todo: if selected block is not 0, cancel block putting.
                    if (Logic.ReadWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira] != 0)
                    { return; }


                    // player is in world
                    //Check inventory item count, if theres count -1 it
                    List<InventoryTile> inventory = Logic.GetInventory(Server.Clients[_Fromcliemt].user.username);

                    foreach (InventoryTile i in inventory)
                    {
                        if (Server.Clients[_Fromcliemt].user.CurrentSelecteditem == i.id)
                        {
                            // item found check count
                            if (i.count > 0)
                            {
                                //Change world data and remove 1 block from inventory
                                Logic.EditWorldBg(Server.Clients[_Fromcliemt].user.World.ToUpper(), sira, Server.Clients[_Fromcliemt].user.CurrentSelecteditem);
                                Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -1);

                                //Send block information to players in world 
                                Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockToEveryoneinworld(sira, (short)i.id, "bg");

                                //Send inventory data to player
                                Logic.GetInventoryAndSend(_Fromcliemt, Server.Clients[_Fromcliemt].user.username.ToUpper());
                            }
                        }
                    }
                }
                else if(itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "LOCK")
                {
                    if (Logic.ReadWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper())[sira] != 0)
                    { return; }
                    if (Logic.GetWorldOwnerName(worldname) != null)
                    {
                        ServerSend.SendChat(_Fromcliemt, "You already locked this world!");
                        return;
                    }
                    else
                    {

                        Logic.SetWorldOwner(worldname, Server.Clients[_Fromcliemt].user.username.ToUpper());

                        ServerSend.SendChat(_Fromcliemt, "You have locked this world!");
                    }


                    // player is in world
                    //Check inventory item count, if theres count -1 it
                    List<InventoryTile> inventory = Logic.GetInventory(Server.Clients[_Fromcliemt].user.username);

                    foreach (InventoryTile i in inventory)
                    {
                        if (Server.Clients[_Fromcliemt].user.CurrentSelecteditem == i.id)
                        {
                            // item found check count
                            if (i.count > 0)
                            {
                                //Change world data and remove 1 block from inventory




                                Logic.EditWorldFg(Server.Clients[_Fromcliemt].user.World.ToUpper(), sira, Server.Clients[_Fromcliemt].user.CurrentSelecteditem);
                                Logic.AddItemToInventory(Server.Clients[_Fromcliemt].user.username.ToUpper(), Server.Clients[_Fromcliemt].user.CurrentSelecteditem, -1);

                                //Send block information to players in world 
                                Logic.worlds[Server.Clients[_Fromcliemt].user.World.ToUpper()].SendBlockToEveryoneinworld(sira, (short)i.id, "fg");

                                //Send inventory data to player
                                Logic.GetInventoryAndSend(_Fromcliemt, Server.Clients[_Fromcliemt].user.username.ToUpper());
                            }
                        }
                    }


                  
                

                }
                else if (itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemtype == "WRENCH")
                {
                    if (Logic.ReadWorldFg(worldname)[sira] == 0)
                    {
                        int blockid = Logic.ReadWorldBg(worldname)[sira];

                        if (itemdata.items[Logic.ReadWorldFg(worldname)[sira]].itemtype == "DISPLAYBLOCK")
                        {
                            return;
                        }
                    }
                    else
                    {
                        int blockid = Logic.ReadWorldFg(worldname)[sira];

                        if (itemdata.items[Logic.ReadWorldFg(worldname)[sira]].itemtype == "DISPLAYBLOCK")
                        {
                            return;
                        }

                        if (blockid == 61)
                        {
                            Logic.EditWorldFg(worldname.ToUpper(), sira, 62);
                            Logic.worlds[worldname].SendBlockToEveryoneinworld(sira, 62, "fg");

                            //open door
                        }
                        else if (blockid == 62)
                        {
                            Logic.EditWorldFg(worldname.ToUpper(), sira, 61);
                            //Close door
                            Logic.worlds[worldname].SendBlockToEveryoneinworld(sira, 61, "fg");
                        }
                        if (blockid == 30)
                        {
                            //Get machine info
                            Server.Clients[_Fromcliemt].user.worldCraftDataOrder = sira;

                            if(CraftingSystem.CheckIfCrafting(_Fromcliemt,sira))
                            {
                                CraftingSystem.TryToGetCraftedItem(_Fromcliemt, sira);
                            }
                            else
                            {
                                ServerSend.SendString(_Fromcliemt, "UI_SHOW_CRAFT");

                            }
                        }
                        if (blockid == 47)
                        {



                            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[_Fromcliemt].user.username.ToUpper()}.json"));


                            if (i.ADRefresherTime < DateTime.Now)
                            {
                                i.AdsLeftWatch = 3;
                                string dataofacc = JsonSerializer.Serialize(i);

                                File.WriteAllText("accounts/" + Server.Clients[_Fromcliemt].user.username.ToUpper() + ".json", dataofacc);
                            }
                            if (i.AdsLeftWatch > 0)
                            {

                                ServerSend.SendString(_Fromcliemt, "AD");
                                ServerSend.SendChat(_Fromcliemt, "Please wait...");
                            }
                            else
                            {
                                ServerSend.SendChat(_Fromcliemt, "You can only watch 3 ads per 12 hours.");

                            }




                        }

                    }

                    

                }
               





            }


        }

        public static void BlockOperationCoolDown(object clientid)
        {
           
        }


        public static void TileSelected(int _Fromcliemt, Packet _packet)
        {
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }
            short selecteditemsirasi = _packet.ReadShort();
            List<InventoryTile> inventory = Logic.GetInventory(Server.Clients[_Fromcliemt].user.username);

            //Check double click
            if (Server.Clients[_Fromcliemt].user.CurrentSelecteditem == (short)inventory[selecteditemsirasi].id)
            {

                //Show item info 
                if(itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem] == null)
                {
                    return;
                }
                try
                {


                    ServerSend.SendItemInfo(_Fromcliemt, itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].itemname, itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].description, itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].istrashable, itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].iswearable, itemdata.items[Server.Clients[_Fromcliemt].user.CurrentSelecteditem].istradable);
                }
                catch
                {


                }

            }

            Server.Clients[_Fromcliemt].user.CurrentSelecteditem = (short)inventory[selecteditemsirasi].id;


           
        }
       

        public static void PositionFromClient(int _Fromcliemt, Packet _packet)
        {



            
            if (!Server.Clients[_Fromcliemt].user.PassedVertifaction)
            {
                Server.Clients[_Fromcliemt].Disconnect();
                return;
            }

            float x = _packet.ReadFloat();
            float y = _packet.ReadFloat();
            bool isjumping = _packet.ReadBool();
            Server.Clients[_Fromcliemt].user.SetPos(x, y, 0,isjumping);


        }

        public static void ChatFromClient(int _fromclient,Packet _packet)
        {
            if (!Server.Clients[_fromclient].user.PassedVertifaction)
            {
                Server.Clients[_fromclient].Disconnect();
                return;
            }
            string message = _packet.ReadString();

            useraccount account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[_fromclient].user.username.ToUpper() + ".json"));
            DiscordPart.PlayerChat(Server.Clients[_fromclient].user.username, message, Server.Clients[_fromclient].user.World);
            if (account.MuteExpireTime > DateTime.Now)
            {
                ServerSend.SendChat(_fromclient, $"<color=red>You are muted, You cannot send messages right now, Your mute will end in {account.MuteExpireTime - DateTime.Now} Mute reason: {account.MuteReason}</color>");
                return;
            }


            if (Server.Clients[_fromclient].user.ChatCooldown)
            {
                ServerSend.SendChat(_fromclient, "<color=red>Spam detected! Please dont spam</color>");
                return;
            }
            else
            {



                try
                {
                    if (message[0] == '/')
                    {
                        CommandManager.ProcessCommand(message, _fromclient);
                        return;
                    }
                }
                catch { }


                if (Server.Clients[_fromclient].user.World != "")
                {

                    Logic.worlds[Server.Clients[_fromclient].user.World.ToUpper()].SendMessageToEveryoneInWorld($"[<color=blue>{Server.Clients[_fromclient].user.realusername}</color>]: {message}");
                    Logic.worlds[Server.Clients[_fromclient].user.World.ToUpper()].SendChatBubbleToEveryoneExpectPlayer(message, _fromclient);
                    ServerSend.SendPlayerChatBubble(_fromclient, message, 0);

                    Logss.UserLog($"Player {Server.Clients[_fromclient].user.username} in world {Server.Clients[_fromclient].user.World} Sended message: {message}", Server.Clients[_fromclient].user.username);
                  Logss.WorldLog($"Player {Server.Clients[_fromclient].user.username} Sended message: {message}", Server.Clients[_fromclient].user.World);




                }
                Server.Clients[_fromclient].user.ChatCooldown = true;
                Server.Clients[_fromclient].user.ChatCooldown = false;
            }
                 
        }

        public static void TrashRequest(int _fromclient, Packet _packet)
        {
             if (!Server.Clients[_fromclient].user.PassedVertifaction)
            {
                Server.Clients[_fromclient].Disconnect();
                return;
            }
            short count = _packet.ReadShort();

            if (Server.Clients[_fromclient].user.Trade != null)
            {

                ServerSend.SendChat(_fromclient, "You cant trash items while trading.");
                return;
            }
            if (count.ToString().Contains("-") || count.ToString().Contains("/") || count.ToString().Contains("+") || count.ToString().Contains("*"))
            {
                ServerSend.SendChat(_fromclient, "Nice try.");
                return;

            }
            if(!itemdata.items[Server.Clients[_fromclient].user.CurrentSelecteditem].istrashable)
            { return; }

            Logic.AddItemToInventory(Server.Clients[_fromclient].user.username.ToUpper(), Server.Clients[_fromclient].user.CurrentSelecteditem, (int)-count);
            Logic.GetInventoryAndSend(_fromclient, Server.Clients[_fromclient].user.username.ToUpper());
        }


    }
}
