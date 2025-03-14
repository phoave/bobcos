using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;
using System.Threading;
using GameServer;

namespace Bobcos_Server
{
    class worldata
    {
        public string OwnerUserame { get; set; }

        public List<string> accessedPlayers { get; set; }
        public List<string> BannedPlayers { get; set; }

        public bool isWorldBanned { get; set; }

        public short[] fg { get; set; }
        public short[] bg { get; set; }

    }


    class useraccount
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }

        public int cash { get; set; }

        public List<InventoryTile> inventory { get; set; }

        public byte r { get; set; }
        public byte g { get; set; }
        public byte b { get; set; }
        public byte t { get; set; }

        public short shirt { get; set; }
        public short pants { get; set; }
        public short shoes { get; set; }
        public short head { get; set; }
        public short back { get; set; }
        public short handitem { get; set; }


        public short hat { get; set; }

        public CraftData Craftdata1 { get; set; }

        public DateTime BanExpireTime { get; set; }

        public DateTime MuteExpireTime { get; set; }
        public List<string> PlayerLogs { get; set; }

        public string BanReason { get; set; }

        public string MuteReason { get; set; }

        public int level { get; set; }

        public int Xp { get; set; }

        public byte StaffLevel { get; set; }

        public int AdsLeftWatch { get; set; }

        public DateTime ADRefresherTime { get; set; }

        public byte badge { get; set; }

        public string[] HardwareIDlog { get; set; }
        public string[] IPLog { get; set; }

    }
    class InventoryTile
    {

        public int id { get; set; }
        public int count { get; set; }
    }

    class Logic
    {
        public static List<string> TotalInGameReports = new List<string>();

        public static void AddXp(string username, int HowMuchXp, int clientid)
        {

            useraccount account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + username.ToUpper() + ".json"));

            account.Xp = account.Xp + HowMuchXp;

            int oldlevel = account.level;

            account.level = (int)MathF.Floor((float)account.Xp / 10000f);

            if (account.level >= 100)
            {
                account.level = 100;
            }
            if (account.level != oldlevel)
            {
                //send leveled up message
                ServerSend.SendWarning(clientid, $"You have leveled up! You are now {account.level} Level!");
            }

            File.WriteAllText("accounts/" + username.ToUpper() + ".json", JsonSerializer.Serialize(account));

        }



        public static void BanPlayer(string whobanned, string _PlayerName, int HowManyDays, int HowManHours, int HowManyMinutes, string banreason)
        {
            string PlayerName = _PlayerName.ToUpper();
            useraccount account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + PlayerName.ToUpper() + ".json"));

            account.BanExpireTime = DateTime.Now.AddDays(HowManyDays).AddHours(HowManHours).AddMinutes(HowManyMinutes);
            account.BanReason = banreason;
            string dataofacc = JsonSerializer.Serialize(account);


            foreach (KeyValuePair<int, Client> a in Server.Clients)
            {


                if (a.Value.user != null && a.Value.user.username != null)
                {


                    if (a.Value.user.username.ToUpper() == PlayerName.ToUpper())
                    {
                        ServerSend.SendWarning(a.Value.id, $"<color=red>Warning From System: You got banned for {HowManyDays}  days.</color>");
                        a.Value.Disconnect();
                    }
                }
            }
            DiscordPart.SendSystemMessage($"[{DateTime.Now}] Player got banned for {account.BanExpireTime - DateTime.Now} for reason {banreason} by {whobanned}");
            File.WriteAllText("accounts/" + PlayerName.ToUpper() + ".json", dataofacc);
        }

        public static void mutePlayer(string whoMuted, string PlayerName, int HowManyDays, int HowManHours, int HowManyMinutes, string MuteReason)
        {
            useraccount account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + PlayerName.ToUpper() + ".json"));

            account.MuteExpireTime = DateTime.Now.AddDays(HowManyDays).AddHours(HowManHours).AddMinutes(HowManyMinutes);
            account.MuteReason = MuteReason;
            string dataofacc = JsonSerializer.Serialize(account);
            foreach (KeyValuePair<int, Client> a in Server.Clients)
            {


                if (a.Value.user != null && a.Value.user.username != null)
                {


                    if (a.Value.user.username.ToUpper() == PlayerName.ToUpper())
                    {
                        ServerSend.SendWarning(a.Value.id, $"<color=red>Warning From System: You got muted for breaking game rules.</color>");
                    }
                }
            }
            DiscordPart.SendSystemMessage($"[{DateTime.Now}] Player got muted for {account.MuteExpireTime - DateTime.Now} for reason {MuteReason} by {whoMuted}");

            File.WriteAllText("accounts/" + PlayerName.ToUpper() + ".json", dataofacc);
        }
        public static void warnPlayer(string whowarned, string PlayerName, string Reason)
        {
            useraccount account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + PlayerName.ToUpper() + ".json"));

            string dataofacc = JsonSerializer.Serialize(account);
            foreach (KeyValuePair<int, Client> a in Server.Clients)
            {


                if (a.Value.user != null && a.Value.user.username != null)
                {


                    if (a.Value.user.username.ToUpper() == PlayerName.ToUpper())
                    {
                        ServerSend.SendWarning(a.Value.id, $"<color=red>Warning From System: You got warned: {Reason}.</color>");
                    }
                }
            }
            DiscordPart.SendSystemMessage($"[{DateTime.Now}] Player got warned for reason {Reason} by {whowarned}");
            File.WriteAllText("accounts/" + PlayerName.ToUpper() + ".json", dataofacc);
        }




        #region AuthSystem
        /// <summary>
        ///  Trys to create account. If It returns 0 unexpected error happened, if it returns 1 account created if it returns 2 it returns account already exists, if its 3 it says about length of password or username.
        /// </summary>
        static public int TryToCreateAccount(string username, string password, string email)
        {
            //First check if account exists

            if (File.Exists("accounts/" + username.ToUpper() + ".json"))
            {

                return 2;
            }
            else
            {
                if (password.Length < 3 || username.Length < 3)
                {
                    return 3;

                }
                else
                {
                    useraccount acc = new useraccount() { username = username, password = password, email = email };
                    acc.inventory = new List<InventoryTile>();
                    acc.inventory.Add(new InventoryTile() { id = 2, count = 1 });
                    acc.inventory.Add(new InventoryTile() { id = 29, count = 1 });



                    acc.t = 255;
                    acc.r = 255;
                    acc.g = 240;
                    acc.b = 167;
                    acc.AdsLeftWatch = 3;
                    acc.ADRefresherTime = DateTime.Now;
                    acc.Craftdata1 = new CraftData() { recipeid = 0, resultItem = 0 };
                    string dataofacc = JsonSerializer.Serialize(acc);

                    File.WriteAllText("accounts/" + username.ToUpper() + ".json", dataofacc);
                    return 1;
                }

            }






        }


        static public int TryToLogin(string username, string password)
        {
            //First check if account exists
            if (File.Exists("accounts/" + username.ToUpper() + ".json"))
            {
                useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + username.ToUpper() + ".json"));
                if (acc.password == password)
                {

                    //Check account online state
                    foreach (KeyValuePair<int, Client> a in Server.Clients)
                    {


                        if (a.Value.user != null && a.Value.user.username != null)
                        {


                            if (a.Value.user.username.ToUpper() == username.ToUpper())
                            {
                                a.Value.Disconnect();
                                return 3;
                            }
                        }
                    }


                    return 1;
                }
                else
                {

                    return 0;
                }

            }
            else
            {
                return 2;

            }






        }


        #endregion


        static public int[] GetWhiteDoorPos(string worldname)
        {
            //get world data
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));

            //Search on fg tiles
            int count = 0;
            foreach (short i in n.fg)
            {
                if (i == itemdata.whitedoorid)
                {
                    // get position of that tile

                    return WorldDataConverter.ConvertComplexidtopos(count);
                }
                count++;
            }
            return new int[] { 0, 0 };

        }


        static public void EditWorldFg(string worldname, int blockid, int blocktoset)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));
            n.fg[blockid] = (short)blocktoset;
            File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

        }
        static public void EditWorldBg(string worldname, int blockid, int blocktoset)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));
            n.bg[blockid] = (short)blocktoset;
            File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

        }

        static public void GetInventoryAndSend(int _toclient, string username)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));
            ServerSend.SendInventory(_toclient, i.inventory);

        }

        static public List<InventoryTile> GetInventory(string username)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));
            return i.inventory;
        }


        static public bool CheckItemInInventory(string username, int itemid, int itemcount)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));





            if (itemid == 0)
            {
                return true;

            }

            foreach (InventoryTile c in i.inventory)
            {
                if (c.id == itemid)
                {
                    if (c.count >= itemcount)
                    {
                        return true;
                    }


                }


            }
            return false;

        }
        static public bool AddItemToInventory(string username, int itemid, int itemcount)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));



            foreach (InventoryTile c in i.inventory)
            {
                if (c.id == itemid)
                {


                    if (c.count > 799)
                    {
                        //dont take item
                        return false;
                    }

                    if (c.count + itemcount > 799)
                    {
                        //Dont take item
                        return false;
                    }


                    c.count = c.count + itemcount;


                    if (c.count <= 0)
                    {
                        c.count = 0;
                        c.id = 0;

                    }
                    DiscordPart.SendSystemMessage($" Player {username.ToUpper()} Taked {itemid} {itemcount}x called by method {(new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name}");
                    File.WriteAllText($"accounts/{username.ToUpper()}.json", JsonSerializer.Serialize(i));
                    ClearEmptyInventoryTiles(username);
                    return true;
                }
                if (c.id == 0)
                {
                    c.id = itemid;
                    c.count = c.count + itemcount;
                    if (c.count <= 0)
                    {
                        c.count = 0;
                        c.id = 0;

                    }
                    File.WriteAllText($"accounts/{username.ToUpper()}.json", JsonSerializer.Serialize(i));
                    ClearEmptyInventoryTiles(username);
                    DiscordPart.SendSystemMessage($"Player {username.ToUpper()} Taked {itemid} {itemcount}x called by method {(new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name}");

                    return true;
                }


            }

            i.inventory.Add(new InventoryTile() { id = itemid, count = itemcount });
            File.WriteAllText($"accounts/{username.ToUpper()}.json", JsonSerializer.Serialize(i));
            ClearEmptyInventoryTiles(username);
            DiscordPart.SendSystemMessage($"Player {username.ToUpper()} Taked {itemid} {itemcount}x called by method {(new System.Diagnostics.StackTrace()).GetFrame(1).GetMethod().Name}");

            return true;
        }
        static public bool CanTakeItem(string username, int itemid, int itemcount)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));
            foreach (InventoryTile c in i.inventory)
            {
                if (c.id == itemid)
                {


                    if (c.count > 799)
                    {
                        //dont take item
                        return false;
                    }

                    if (c.count + itemcount > 799)
                    {
                        //Dont take item
                        return false;
                    }



                    return true;
                }
                if (c.id == 0)
                {

                    return true;
                }

            }


            return true;
        }

        static public void ClearEmptyInventoryTiles(string username)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));
            List<InventoryTile> NewTile = new List<InventoryTile>();

            foreach (InventoryTile c in i.inventory)
            {
                if (c.count > 0)
                {
                    NewTile.Add(c);

                }
            }
            i.inventory = NewTile;

            File.WriteAllText($"accounts/{username.ToUpper()}.json", JsonSerializer.Serialize(i));

        }
        static public void TryToReadWorldDataAndSendToClient(int _toclient, string worldname)

        {

            ServerSend.SendWorldDataFg(_toclient, ReadWorldFg(worldname.ToUpper()), "fg");

            ServerSend.SendWorldDataFg(_toclient, ReadWorldBg(worldname.ToUpper()), "bg");


        }

        static public void AddGems(string username, int gemamount)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));
            i.cash = i.cash + gemamount;
            File.WriteAllText($"accounts/{username.ToUpper()}.json", JsonSerializer.Serialize(i));
        }
        static public void SetGems(string username, int amount)
        {
            useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json"));
            i.cash = amount;
            File.WriteAllText($"accounts/{username.ToUpper()}.json", JsonSerializer.Serialize(i));
        }

        static public int GetGems(string username)
        {
            return JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{username.ToUpper()}.json")).cash;

        }

        static public int CheckWorldFile(string worldname)
        {
            if (File.Exists($"worlds/{worldname.ToUpper()}.json"))
            {


                return 1;
            }
            else
            {

                return 0;
            }
        }

        static public void CreateWorld(string worldname)
        {
            Random SandWorldOrNormalWorld = new Random();

            if (SandWorldOrNormalWorld.Next(0, 3) == 2)
            {
                worldata newworldata2 = new worldata();
                newworldata2.fg = new short[4703];
                newworldata2.bg = new short[4703];


                for (int i = 0; i < 2353; i++)
                {
                    newworldata2.fg[WorldDataConverter.convertToComplexBlock(i)] = (short)66;
                }




                newworldata2.fg[WorldDataConverter.convertToComplexBlock(2353)] = (short)5;
                for (int i = 2354; i < 4703; i++)
                {
                    newworldata2.fg[WorldDataConverter.convertToComplexBlock(i)] = (short)0;
                }

                for (int i = 0; i < 2353; i++)
                {
                    newworldata2.bg[WorldDataConverter.convertToComplexBlock(i)] = (short)4;
                }
                for (int i = 2353; i < 4703; i++)
                {
                    newworldata2.bg[WorldDataConverter.convertToComplexBlock(i)] = (short)0;
                }
                Random StonePicker2 = new Random();

                for (int i = 0; i < 55; i++)
                {
                    newworldata2.fg[WorldDataConverter.convertToComplexBlock(StonePicker2.Next(0, 2353))] = (short)37;

                }
                for (int i = 0; i < 20; i++)
                {
                    newworldata2.fg[WorldDataConverter.convertToComplexBlock(StonePicker2.Next(0, 2353))] = (short)73;

                }

                newworldata2.fg[2257] = (short)11;

                for (int i = 0; i < 192; i++)
                {
                    newworldata2.fg[WorldDataConverter.convertToComplexBlock(i)] = (short)11;
                }

                SignDataWorld data1232 = new SignDataWorld();

                data1232.Data = new string[4703];




                File.WriteAllText("worldsign/" + worldname.ToUpper() + ".json", JsonSerializer.Serialize(data1232));
                string jsonver2 = JsonSerializer.Serialize(newworldata2);
                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", jsonver2);

                WorldCraftData Worldcraftdat2 = new WorldCraftData();
                Worldcraftdat2.Data = null;
                jsonver2 = JsonSerializer.Serialize(Worldcraftdat2);
                File.WriteAllText($"worldcraftdata/{worldname.ToUpper()}.json", jsonver2);





            }
            else
            {






                worldata newworldata = new worldata();
                newworldata.fg = new short[4703];
                newworldata.bg = new short[4703];


                for (int i = 0; i < 2353; i++)
                {
                    newworldata.fg[WorldDataConverter.convertToComplexBlock(i)] = (short)3;
                }





                for (int i = 2354; i < 4703; i++)
                {
                    newworldata.fg[WorldDataConverter.convertToComplexBlock(i)] = (short)0;
                }

                for (int i = 0; i < 2353; i++)
                {
                    newworldata.bg[WorldDataConverter.convertToComplexBlock(i)] = (short)4;
                }
                for (int i = 2353; i < 4703; i++)
                {
                    newworldata.bg[WorldDataConverter.convertToComplexBlock(i)] = (short)0;
                }
                Random StonePicker = new Random();

                for (int i = 0; i < 55; i++)
                {
                    newworldata.fg[WorldDataConverter.convertToComplexBlock(StonePicker.Next(0, 2353))] = (short)37;

                }



                for (int i = 2248; i <= 2400; i++)
                {
                    newworldata.fg[i] = (short)72;
                    newworldata.bg[i] = (short)0;

                }
                for (int i = 2354; i <= 2399; i++)
                {
                    newworldata.fg[i] = (short)72;
                    newworldata.bg[i] = (short)0;

                }

                for (int i = 0; i < 16; i++)
                {
                    int val = StonePicker.Next(2248, 2401);
                    newworldata.fg[val] = (short)71;
                    newworldata.bg[val] = (short)0;

                }
                for (int i = 0; i < 16; i++)
                {
                    int val = StonePicker.Next(2248, 2401);
                    newworldata.fg[val] = (short)70;
                    newworldata.bg[val] = (short)0;

                }
                for (int i = 0; i < 16; i++)
                {
                    int val = StonePicker.Next(2248, 2401);
                    newworldata.fg[val] = (short)69;
                    newworldata.bg[val] = (short)0;

                }
                for (int i = 0; i < 16; i++)
                {
                    int val = StonePicker.Next(2248, 2401);
                    newworldata.fg[val] = (short)73;
                    newworldata.bg[val] = (short)0;

                }
                for (int i = 0; i < 192; i++)
                {
                    newworldata.fg[WorldDataConverter.convertToComplexBlock(i)] = (short)11;
                }

                newworldata.fg[2257] = (short)11;
                newworldata.fg[WorldDataConverter.convertToComplexBlock(2353)] = (short)5;

                SignDataWorld data123 = new SignDataWorld();

                data123.Data = new string[4703];




                File.WriteAllText("worldsign/" + worldname.ToUpper() + ".json", JsonSerializer.Serialize(data123));
                string jsonver = JsonSerializer.Serialize(newworldata);
                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", jsonver);

                WorldCraftData Worldcraftdat = new WorldCraftData();
                Worldcraftdat.Data = null;
                jsonver = JsonSerializer.Serialize(Worldcraftdat);
                File.WriteAllText($"worldcraftdata/{worldname.ToUpper()}.json", jsonver);
            }
        }

        static public short[] ReadWorldFg(string worldname)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));
            return n.fg;

        }
        static public short[] ReadWorldBg(string worldname)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));
            return n.bg;

        }


        static public Dictionary<string, World> worlds = new Dictionary<string, World>();
        static public void JoinToWorld(object data)
        {

            object[] dat = (object[])data;


            int idofplayer = (int)dat[0];
            string worldname = (string)dat[1];
            bool issummonedbymod = (bool)dat[2];
            if (Server.Clients[idofplayer].user.World != null)
            {

                return;
            }



            if (CheckWorldFile(worldname.ToUpper()) == 0)
            {
                CreateWorld(worldname.ToUpper());

            }


            //Get world info


            worldata worlddata = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));




            //Get account

            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[idofplayer].user.username.ToUpper()}.json"));

            bool canPass = false;

            if (acc.StaffLevel > 2)
            {
                canPass = true;
            }











            //Check for banned-world status if its banned check player status if its mod let him in, if its not don't.

            if (worlddata.isWorldBanned)
            {
                if (!canPass)
                {
                    ServerSend.SendWarning(idofplayer, "This world is banned for breaking game rules");
                    return;
                }

            }



            //Check for world ban 

            if (GetBannedPlayers(worldname.ToUpper()).Contains(Server.Clients[idofplayer].user.username.ToUpper()))
            {

                if (!canPass)
                {
                    ServerSend.SendWarning(idofplayer, "You got banned from world");
                    return;
                }


            }


            ServerSend.SendResult(idofplayer, 2, "Entered world successfully.");


            Logic.GetInventoryAndSend((int)idofplayer, Server.Clients[(int)idofplayer].user.username.ToLower());




            ServerSend.SendCash((int)idofplayer, Logic.GetGems(Server.Clients[(int)idofplayer].user.username.ToUpper()));


            ServerSend.SendChat(idofplayer, "<color=green>In game rules</color>");
            ServerSend.SendChat(idofplayer, "-Swearing, being toxic, causing drama is not allowed.");
            ServerSend.SendChat(idofplayer, "-Scamming, Hosting Drop games, Casinos are illegal.");
            ServerSend.SendChat(idofplayer, "-Hacking, exploiting,auto-clicker,macro ,bots,any third party program, etc. is not allowed");
            ServerSend.SendChat(idofplayer, "-If you find Important bugs that can harm game, Please report them. Using bugs for bad reason may get your account terminated.");



            if (!worlds.ContainsKey(worldname.ToUpper()))
            {
                ServerSend.SpawnPlayer((int)idofplayer, Server.Clients[(int)idofplayer].user.realusername, true, 0);

                worlds[worldname.ToUpper()] = new World() { WorldName = worldname.ToUpper() };



                worlds[worldname.ToUpper()].Playersinworld.Add(idofplayer);


                Console.WriteLine("Waiting...");





                if (!Server.Clients[idofplayer].user.isInvisible)
                {


                    worlds[worldname.ToUpper()].SendEnteredToEveryoneExpectClient(Server.Clients[(int)idofplayer].user.realusername, true, (int)idofplayer);


                    ServerSend.SendChat((int)idofplayer, "Entered to <color=green>" + worldname + "</color>. Theres " + worlds[worldname.ToUpper()].Playersinworld.Count + " People here.");


                    worlds[worldname.ToUpper()].SendPositionToEveryonexpectClient(Server.Clients[(int)idofplayer].user.xpos, Server.Clients[(int)idofplayer].user.ypos, (int)idofplayer, false);
                }











                worlds[worldname.ToUpper()].SendEveryonesEnteredToPlayer((int)idofplayer);


                worlds[worldname.ToUpper()].SendPositionsOfOtherPlayers((int)idofplayer);







                useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[(int)idofplayer].user.username.ToUpper()}.json"));


















                //get white door and spawn on it

                worlds[worldname.ToUpper()].SendMessageToEveryoneInWorld("(<color=blue>" + Server.Clients[(int)idofplayer].user.realusername + "</color>) Entered to world!");











                Server.Clients[(int)idofplayer].user.xpos = Logic.GetWhiteDoorPos(worldname.ToUpper())[0];

                Server.Clients[(int)idofplayer].user.ypos = Logic.GetWhiteDoorPos(worldname.ToUpper())[1] + 1f;


                ServerSend.SendPositionViaTCP((int)idofplayer, 0, Server.Clients[(int)idofplayer].user.xpos, Server.Clients[(int)idofplayer].user.ypos, false);
                ServerSend.SendPosition((int)idofplayer, 0, Server.Clients[(int)idofplayer].user.xpos, Server.Clients[(int)idofplayer].user.ypos, false);













                //Get player appreance and send it to everyone
                acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[idofplayer].user.username.ToUpper() + ".json"));





                worlds[worldname.ToUpper()].SendApperanceToEveryoneExpectPlayer(acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, idofplayer, acc.hat, acc.handitem, acc.badge);

                if (!Server.Clients[idofplayer].user.isInvisible)
                {
                    ServerSend.SendPlayerApprence(idofplayer, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem, acc.badge);
                }
                //Receive other players apperance

                foreach (int p in worlds[worldname.ToUpper()].Playersinworld)
                {

                    if (p != idofplayer)
                    {
                        try
                        {
                            acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[p].user.username.ToUpper() + ".json"));

                            ServerSend.SendPlayerApprence(idofplayer, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, (short)p, acc.hat, acc.handitem, acc.badge);

                        }
                        catch
                        {

                        }

                    }
                }






                Server.Clients[idofplayer].user.World = worldname.ToUpper();







                ServerSend.SendChat(idofplayer, "You successfully joined this world.");


                Logic.TryToReadWorldDataAndSendToClient(idofplayer, worldname.ToUpper());

                worlds[worldname.ToUpper()].SendDrop();



            }
            else
            {
                ServerSend.SpawnPlayer((int)idofplayer, Server.Clients[(int)idofplayer].user.realusername, true, 0);



                worlds[worldname.ToUpper()].Playersinworld.Add(idofplayer);



                Console.WriteLine("Waiting...");











                if (!Server.Clients[idofplayer].user.isInvisible)
                {
                    worlds[worldname.ToUpper()].SendEnteredToEveryoneExpectClient(Server.Clients[(int)idofplayer].user.realusername, true, (int)idofplayer);

                    worlds[worldname.ToUpper()].SendMessageToEveryoneInWorld("(<color=blue>" + Server.Clients[(int)idofplayer].user.realusername + "</color>) Entered to world!");


                    worlds[worldname.ToUpper()].SendPositionToEveryonexpectClient(Server.Clients[(int)idofplayer].user.xpos, Server.Clients[(int)idofplayer].user.ypos, (int)idofplayer, false);
                }





                worlds[worldname.ToUpper()].SendEveryonesEnteredToPlayer((int)idofplayer);


                worlds[worldname.ToUpper()].SendPositionsOfOtherPlayers((int)idofplayer);






                useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[(int)idofplayer].user.username.ToUpper()}.json"));






                ServerSend.SendChat((int)idofplayer, "Entered to <color=green>" + worldname + "</color>. Theres " + worlds[worldname.ToUpper()].Playersinworld.Count + " People here.");












                //get white door and spawn on it










                Server.Clients[(int)idofplayer].user.xpos = Logic.GetWhiteDoorPos(worldname.ToUpper())[0];

                Server.Clients[(int)idofplayer].user.ypos = Logic.GetWhiteDoorPos(worldname.ToUpper())[1] + 1f;


                ServerSend.SendPositionViaTCP((int)idofplayer, 0, Server.Clients[(int)idofplayer].user.xpos, Server.Clients[(int)idofplayer].user.ypos, false);
                ServerSend.SendPosition((int)idofplayer, 0, Server.Clients[(int)idofplayer].user.xpos, Server.Clients[(int)idofplayer].user.ypos, false);













                //Get player appreance and send it to everyone
                acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[idofplayer].user.username.ToUpper() + ".json"));





                worlds[worldname.ToUpper()].SendApperanceToEveryoneExpectPlayer(acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, idofplayer, acc.hat, acc.handitem, acc.badge);

                if (!Server.Clients[idofplayer].user.isInvisible)
                {
                    ServerSend.SendPlayerApprence(idofplayer, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem, acc.badge);
                }
                //Receive other players apperance

                foreach (int p in worlds[worldname.ToUpper()].Playersinworld)
                {

                    if (p != idofplayer)
                    {
                        try
                        {
                            acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[p].user.username.ToUpper() + ".json"));

                            ServerSend.SendPlayerApprence(idofplayer, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, (short)p, acc.hat, acc.handitem, acc.badge);

                        }
                        catch
                        {

                        }

                    }
                }





                Server.Clients[idofplayer].user.World = worldname.ToUpper();







                ServerSend.SendChat(idofplayer, "You successfully joined this world.");

                Logic.TryToReadWorldDataAndSendToClient(idofplayer, worldname.ToUpper());


                worlds[worldname.ToUpper()].SendDrop();


            }




        }





        static public void WearItem(int plrid)
        {
            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[plrid].user.username.ToUpper()}.json"));

            short item = Server.Clients[plrid].user.CurrentSelecteditem;




            if (itemdata.items[item].itemtype == "SHIRT")
            {
                if (acc.shirt == item)
                {
                    //You are wearing item, unwear it set it to 0
                    acc.shirt = -1;
                }
                else
                {
                    //wear item
                    acc.shirt = item;
                }
            }

            if (itemdata.items[item].itemtype == "PANT")
            {
                if (acc.pants == item)
                {
                    //You are wearing item, unwear it set it to 0
                    acc.pants = -1;
                }
                else
                {
                    //wear item
                    acc.pants = item;
                }
            }

            if (itemdata.items[item].itemtype == "SHOE")
            {
                if (acc.shoes == item)
                {
                    //You are wearing item, unwear it set it to 0
                    acc.shoes = -1;
                }
                else
                {
                    //wear item
                    acc.shoes = item;
                }
            }

            if (itemdata.items[item].itemtype == "HAIR")
            {
                if (acc.head == item)
                {
                    //You are wearing item, unwear it set it to 0
                    acc.head = -1;
                }
                else
                {
                    //wear item
                    acc.head = item;
                }
            }

            if (itemdata.items[item].itemtype == "HAT")
            {
                if (acc.hat == item)
                {
                    //You are wearing item, unwear it set it to 0
                    acc.hat = -1;
                }
                else
                {
                    //wear item
                    acc.hat = item;
                }
            }

            if (itemdata.items[item].itemtype == "BACK")
            {
                if (acc.back == item)
                {
                    //You are wearing item, unwear it set it to 0
                    acc.back = -1;
                }
                else
                {
                    //wear item
                    acc.back = item;
                }
            }
            if (itemdata.items[item].itemtype == "HAND")
            {
                if (acc.handitem == item)
                {
                    //You are wearing item, unwear it set it to 0
                    acc.handitem = -1;
                }
                else
                {
                    //wear item
                    acc.handitem = item;
                }
            }

            worlds[Server.Clients[plrid].user.World.ToUpper()].SendApperanceToEveryoneExpectPlayer(acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, plrid, acc.hat, acc.handitem, acc.badge);

            ServerSend.SendPlayerApprence(plrid, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem, acc.badge);
            File.WriteAllText($"accounts/{Server.Clients[plrid].user.username.ToUpper().ToUpper()}.json", JsonSerializer.Serialize(acc));

        }


        static public void CheckIfStillOld(string worldname, int oldbreakid, int blockorder)
        {
            //Wait for 5 seconds.
            Thread.Sleep(5000);
            try
            {


                if (worlds[worldname.ToUpper()].HealthOfBlocks[blockorder] == oldbreakid)
                {
                    //Cancel breaking.
                    worlds[worldname.ToUpper()].HealthOfBlocks[blockorder] = 0;
                }
            }
            catch
            {

            }


        }

        static public void SetWorldOwner(string worldname, string username)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));
            n.OwnerUserame = username;
            File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

        }

        static public string GetWorldOwnerName(string worldname)
        {




            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));
            return n.OwnerUserame;

        }

        public static List<string> GetWorldAccessedPlayers(string worldname)
        {




            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));


            if (n.accessedPlayers != null)
            {
                return n.accessedPlayers;

            }
            else
            {
                n.accessedPlayers = new List<string>();
                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

                return n.accessedPlayers;

            }


        }

        public static void AddPlayerAccess(string worldname, string username)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));

            if (n.accessedPlayers != null)
            {
                if (n.accessedPlayers.Contains(username.ToUpper()))
                {
                    // player is already accessed
                    return;
                }
                else
                {

                    n.accessedPlayers.Add(username.ToUpper());
                }
                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

            }
            else
            {
                n.accessedPlayers = new List<string>();

                n.accessedPlayers.Add(username.ToUpper());

                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));


            }
        }
        public static void RemovePlayerAccess(string worldname, string username)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));

            if (n.accessedPlayers != null)
            {
                if (n.accessedPlayers.Contains(username.ToUpper()))
                {
                    n.accessedPlayers.Remove(username.ToUpper());
                    File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

                }
                else
                {

                    //player is not accessed
                }

            }
            else
            {
                //no one is accessed


            }
        }


        public static List<string> GetBannedPlayers(string worldname)
        {




            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));


            if (n.BannedPlayers != null)
            {
                return n.BannedPlayers;

            }
            else
            {
                n.BannedPlayers = new List<string>();
                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

                return n.BannedPlayers;

            }


        }

        public static void BanPlayerFromWorld(string worldname, string username)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));

            if (n.BannedPlayers != null)
            {
                if (n.BannedPlayers.Contains(username.ToUpper()))
                {
                    // player is already accessed
                    return;
                }
                else
                {

                    n.BannedPlayers.Add(username.ToUpper());
                }
                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

            }
            else
            {
                n.BannedPlayers = new List<string>();

                n.BannedPlayers.Add(username.ToUpper());

                File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));


            }
        }
        public static void UnBanPlayerFromWorld(string worldname, string username)
        {
            worldata n = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{worldname.ToUpper()}.json"));

            if (n.BannedPlayers != null)
            {
                if (n.BannedPlayers.Contains(username.ToUpper()))
                {
                    n.BannedPlayers.Remove(username.ToUpper());
                    File.WriteAllText($"worlds/{worldname.ToUpper()}.json", JsonSerializer.Serialize(n));

                }
                else
                {

                    //player is not accessed
                }

            }
            else
            {
                //no one is accessed


            }
        }


        static public void SendGlobalMessage(string Message, int clientid)
        {
            if (GetGems(Server.Clients[clientid].user.username.ToUpper()) > 74)
            {
                Logic.AddGems(Server.Clients[clientid].user.username.ToUpper(), -75);
                ServerSend.SendCash(clientid, Logic.GetGems(Server.Clients[clientid].user.username.ToUpper()));
                foreach (KeyValuePair<int, Client> client in Server.Clients)
                {

                    ServerSend.SendChat(client.Value.id, $"<color=grey>Global Message from</color><color=green> ({Server.Clients[clientid].user.username.ToUpper()})</color><color=grey> In</color> <color=cyan>[{Server.Clients[clientid].user.World.ToUpper()}]:</color> {Message}  ");

                }
            }
        }

    }

    class World
    {
        public string WorldName;
        public List<int> Playersinworld = new List<int>();

        public int[] HealthOfBlocks = new int[4704];



        public void OnJoinedWorld(object playerid)
        {
            //execute this when an player enters world.

            Console.WriteLine("Waiting...");















            SendEnteredToEveryoneExpectClient(Server.Clients[(int)playerid].user.realusername, true, (int)playerid);



            SendPositionToEveryonexpectClient(Server.Clients[(int)playerid].user.xpos, Server.Clients[(int)playerid].user.ypos, (int)playerid, false);



            SendEveryonesEnteredToPlayer((int)playerid);


            SendPositionsOfOtherPlayers((int)playerid);


            Logic.GetInventoryAndSend((int)playerid, Server.Clients[(int)playerid].user.username.ToLower());



            ServerSend.SendCash((int)playerid, Logic.GetGems(Server.Clients[(int)playerid].user.username.ToUpper()));



            useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[(int)playerid].user.username.ToUpper()}.json"));






            ServerSend.SendChat((int)playerid, "Entered to <color=green>" + WorldName + "</color>. Theres " + Playersinworld.Count + " People here.");












            //get white door and spawn on it

            SendMessageToEveryoneInWorld("(<color=blue>" + Server.Clients[(int)playerid].user.realusername + "</color>) Entered to world!");






            ServerSend.SpawnPlayer((int)playerid, Server.Clients[(int)playerid].user.realusername, true, 0);



            Server.Clients[(int)playerid].user.xpos = Logic.GetWhiteDoorPos(WorldName.ToUpper())[0];

            Server.Clients[(int)playerid].user.ypos = Logic.GetWhiteDoorPos(WorldName.ToUpper())[1] + 1f;


            ServerSend.SendPositionViaTCP((int)playerid, 0, Server.Clients[(int)playerid].user.xpos, Server.Clients[(int)playerid].user.ypos, false);
            ServerSend.SendPosition((int)playerid, 0, Server.Clients[(int)playerid].user.xpos, Server.Clients[(int)playerid].user.ypos, false);













            //Get player appreance and send it to everyone
            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[(int)playerid].user.username.ToUpper() + ".json"));





            SendApperanceToEveryoneExpectPlayer(acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, (int)playerid, acc.hat, acc.handitem, acc.badge);


            ServerSend.SendPlayerApprence((int)playerid, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem, acc.badge);

            //Receive other players apperance

            foreach (int p in Playersinworld)
            {

                if (p != (int)playerid)
                {
                    try
                    {
                        acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[p].user.username.ToUpper() + ".json"));

                        ServerSend.SendPlayerApprence((int)playerid, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, (short)p, acc.hat, acc.handitem, acc.badge);

                    }
                    catch
                    {

                    }

                }
            }



        }


        public void PosRefresher(object toClient)
        {
            Console.WriteLine("Pos refresher started.");
            while (Playersinworld.Count > 0)
            {
                if (Server.Clients[(int)toClient].user == null)
                {
                    return;
                }
                if (Server.Clients[(int)toClient].user.World != "")
                {
                    Thread.Sleep(680);
                    SendPositionsOfOtherPlayers((int)toClient);

                }


            }

        }


        public void LeaveWorld(int playerid)
        {
            Playersinworld.Remove(playerid);
            SendEnteredToEveryoneExpectClient("", false, playerid);


            if (Server.Clients[playerid].user.Trade != null)
            {
                Server.Clients[playerid].user.Trade.CancelTrade(playerid);
            }

            try
            {
                SendMessageToEveryoneInWorld("(<color=blue>" + Server.Clients[playerid].user.realusername + "</color>) Left the world!");
                Server.Clients[playerid].user.World = null;
                ServerSend.SendResult(playerid, 1, "Left world");
            }
            catch
            {

            }

        }

        public void SendMessageToEveryoneInWorld(string message)
        {
            try
            {


                foreach (int i in Playersinworld)
                {
                    ServerSend.SendChat(i, message);
                }
            }
            catch
            {


            }
        }

        /// <summary>
        /// Sends player spawn or despawn to everyone
        /// </summary>
        public void SendEnteredToEveryoneExpectClient(string username, bool spawnordespawn, int playerid)
        {

            if (Server.Clients[playerid].user == null)
            {
                LeaveWorld(playerid);
                Server.Clients[playerid].Disconnect();
                return;
            }
            useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[playerid].user.username.ToUpper()}.json"));



            foreach (int i in Playersinworld)
            {
                if (i != playerid)
                {


                    ServerSend.SpawnPlayer(i, username, spawnordespawn, playerid);
                }
            }
        }

        public void SendEveryonesEnteredToPlayer(int toclient)
        {

            foreach (int i in Playersinworld)
            {
                if (i != toclient)
                {
                    if (!Server.Clients[i].user.isInvisible)
                    {

                        ServerSend.SpawnPlayer(toclient, Server.Clients[i].user.realusername, true, i);
                    }

                }
            }
        }



        public void SendPositionToEveryonexpectClient(float x, float y, int id, bool isjumping)
        {
            if (Server.Clients[id].user.isInvisible)
            {
                return;
            }

            foreach (int i in Playersinworld)
            {


                if (i != id)
                {
                    ServerSend.SendPosition(i, id, x, y, isjumping);

                }
            }

        }

        public void SendPositionsOfOtherPlayers(int targetid)
        {

            foreach (int i in Playersinworld)
            {
                if (i != targetid)
                {
                    try
                    {
                        if (!Server.Clients[i].user.isInvisible)
                        {
                            ServerSend.SendPosition(targetid, i, Server.Clients[i].user.xpos, Server.Clients[i].user.ypos, false);
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        public void SendBlockToEveryoneinworld(short sira, short itemid, string isbgorfg)
        {
            foreach (int i in Playersinworld)
            {
                ServerSend.SendBlockData(i, sira, itemid, isbgorfg);
            }
        }
        public void SendDrop()
        {
            if (File.Exists($"dropItemWorldData/{WorldName.ToUpper()}.json"))
            {


                foreach (int i in Playersinworld)
                {
                    DropWorldData datareaded = JsonSerializer.Deserialize<DropWorldData>(File.ReadAllText($"dropItemWorldData/{WorldName.ToUpper()}.json"));

                    ServerSend.SendDropDatas(i, datareaded.DropData);
                }
            }
        }

        public void SendBlockBreakToEveryoneInWorld(int order, byte animNum)
        {

            foreach (int i in Playersinworld)
            {
                ServerSend.SendBlockBrekingAnim(i, (short)order, animNum);

            }
        }

        public void SendApperanceToEveryoneExpectPlayer(byte r, byte g, byte b, byte t, short shirt, short pant, short shoes, short backid, short headid, int playerid, short hatid, short handitem, byte badge)
        {
            foreach (int i in Playersinworld)
            {
                if (i != playerid)
                {



                    ServerSend.SendPlayerApprence(i, r, g, b, t, shirt, pant, shoes, backid, headid, (short)playerid, hatid, handitem, badge);
                }
            }
        }
        public void SendChatBubbleToEveryoneExpectPlayer(string text, int PlayerId)
        {

            foreach (int i in Playersinworld)
            {
                if (i != PlayerId)
                {

                    ServerSend.SendPlayerChatBubble(i, text, PlayerId);

                }
            }
        }

        public void SendChattingBubbleToEveryone(int PlayerId, byte anim)
        {

            foreach (int i in Playersinworld)
            {
                if (i != PlayerId)
                {

                    ServerSend.SendBubble(i, PlayerId, anim);

                }
                else
                {
                    ServerSend.SendBubble(PlayerId, 0, anim);


                }
            }
        }


        public void SendPunch(int playerid)
        {
            ServerSend.SendAnim(playerid, 0, 0);
            try
            {

                foreach (int i in Playersinworld)
                {
                    if (i != playerid)
                    {

                        ServerSend.SendAnim(i, 0, playerid);
                        

                    }
                }
            }
            catch
            {
                SendPunch(playerid);
            }

        }
        public void SendWalk(int playerid)
        {
            ServerSend.SendAnim(playerid, 1, 0);
            try
            {

                foreach (int i in Playersinworld)
                {
                    if (i != playerid)
                    {

                        ServerSend.SendAnim(i, 1, playerid);

                    }
                }
            }
            catch
            {
                SendWalk(playerid);
                Console.WriteLine("Walk work");
            }

        }
        public void SendTalk(int playerid)
        {
            ServerSend.SendAnim(playerid, 2, 0);
            try
            {

                foreach (int i in Playersinworld)
                {
                    if (i != playerid)
                    {

                        ServerSend.SendAnim(i, 2, playerid);

                  

                    }
                }
            }
            catch
            {
                SendTalk(playerid);

            }

        }

    }




}