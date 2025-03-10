using GameServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace Bobcos_Server
{
    class CommandManager
    {



        public static void ProcessCommand(string Command,int clientId)
        {
            //Split command

            string[] SplittedString = Command.Split(" ");



            if (SplittedString[0] == "/item")
            {
                useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));

                if (acc2.StaffLevel < 4)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {
                    int itemid = int.Parse(SplittedString[1]);
                    int itemcount = int.Parse(SplittedString[2]);

                    Logic.AddItemToInventory(Server.Clients[clientId].user.username.ToUpper(), itemid, itemcount);
                    Logic.GetInventoryAndSend(clientId, Server.Clients[clientId].user.username.ToUpper());
                }
                catch { }
            }
            else if (SplittedString[0] == "/givegem")
            {
                useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc32.StaffLevel < 4)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {
                    int gemCount = int.Parse(SplittedString[1]);
                    Logic.AddGems(Server.Clients[clientId].user.username.ToUpper(), gemCount);
                    ServerSend.SendCash(clientId, gemCount);



                }
                catch { }

            }
            else if (SplittedString[0] == "/gm")
            {
                try
                {
                    char[] array = Command.ToCharArray();
                    string text = "";
                    int count = 0;
                    foreach (char a in array)
                    {
                        count++;
                        if (count > 3)
                        {


                            text = text + a;
                        }
                    }




                    Logic.SendGlobalMessage(text, clientId);

                    DiscordPart.GlobalMessageLog(Server.Clients[clientId].user.username, text, Server.Clients[clientId].user.World);

                }
                catch { }
            }
            else if (SplittedString[0] == "/pm")
            {
                try
                {

                    string username = SplittedString[1];

                    char[] array = Command.ToCharArray();
                    string text = "";
                    int count = 0;
                    Console.WriteLine($"Sending message to {username}");
                    foreach (char a in array)
                    {
                        count++;
                        if (count > 5 + username.Length)
                        {


                            text = text + a;
                        }
                    }
                    foreach (Client playerids in Server.Clients.Values)
                    {
                        if (playerids.user != null && playerids.user.username != null)
                        {
                            
                            if (Server.Clients[playerids.peer.Id].user.username.ToUpper() == username.ToUpper())
                            {


                                ServerSend.SendChat(playerids.peer.Id, $"Private Message from <color=green>{Server.Clients[clientId].user.username}</color> : {text}  ");
                                ServerSend.SendChat(clientId, "Sended private message to player!");

                                return;

                            }
                        }
                    }

                    ServerSend.SendChat(clientId, "cant send private message to player!");




                }
                catch { }
            }
            else if (SplittedString[0] == "/report")
            {
                try
                {

                    string username = SplittedString[1];

                    char[] array = Command.ToCharArray();
                    string text = "";
                    int count = 0;
                    Console.WriteLine($"reporting {username}...");
                    foreach (char a in array)
                    {
                        count++;
                        if (count > 9 + username.Length)
                        {


                            text = text + a;
                        }
                    }

                    Logic.TotalInGameReports.Add($"[{DateTime.Now}] Player {Server.Clients[clientId].user.username} Reported player {username} for reason {text} in world {Server.Clients[clientId].user.World}");

                    ServerSend.SendChat(clientId, $"Reported player {username}! Thank you for reporting.");


                }
                catch { }
            }
            else if (SplittedString[0] == "/receivereports")
            {
                try
                {
                    useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));

                    if (acc32.StaffLevel < 2)
                    {
                        ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                        return;
                    }
                    int pagenum =int.Parse( SplittedString[1]) - 1;

                    ServerSend.SendChat(clientId, $"Receiving Reports page {pagenum}. Showing 25 ..");

                    ServerSend.SendChat(clientId, $"Total Reports {Logic.TotalInGameReports.Count}. If Report is wrong / successful, use /removereport <reportnumber> To remove report.");

                    for(int i = pagenum*25; i <= pagenum+1*25; i++ )
                    {
                        ServerSend.SendChat(clientId, $"-{i}- {Logic.TotalInGameReports[i]}");
                    }


                }
                catch { }
            }
            else if (SplittedString[0] == "/removereport")
            {
                try
                {
                    useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));

                    if (acc32.StaffLevel < 2)
                    {
                        ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                        return;
                    }
                    int id = int.Parse(SplittedString[1]);

                    Logic.TotalInGameReports[id] = "REPORT COMPLETE";
                    ServerSend.SendChat(clientId, "Remove successful");
                }
                catch { }
            }
            else if (SplittedString[0] == "/reportworld")
            {
                try
                {


                    char[] array = Command.ToCharArray();
                    string text = "";
                    int count = 0;
                    Console.WriteLine($"reporting World...");
                    foreach (char a in array)
                    {
                        count++;
                        if (count > 9)
                        {


                            text = text + a;
                        }
                    }

                    Logic.TotalInGameReports.Add($"[{DateTime.Now}] Player {Server.Clients[clientId].user.username} Reported world  for reason {text} in world {Server.Clients[clientId].user.World}");

                    Console.WriteLine($"Reported World! Thank you for reporting.");


                }
                catch { }
            }
            else if (SplittedString[0] == "/trade")
            {
                try
                {
                    string PlayerToWait = SplittedString[1];


                    //Check if is player in world or not


                    if (PlayerToWait.ToUpper() == Server.Clients[clientId].user.username.ToUpper())

                    {
                        return;
                    }
                    if(Server.Clients[clientId].user.Trade != null)
                    {
                        ServerSend.SendChat(clientId, "You are already trading with someone.");
                        return;
                    }

                    foreach (int playerids in Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].Playersinworld)
                    {
                        //Gets every player id
                        if (Server.Clients[playerids].user.username.ToUpper() == PlayerToWait.ToUpper())
                        {
                            //Found player in the world check player trade


                            if (Server.Clients[playerids].user.Trade != null)
                            {
                                //player is having trade
                                if (Server.Clients[playerids].user.Trade.PlayerToWait != Server.Clients[clientId].user.username.ToUpper())
                                {
                                    //Player is trading with someone
                                    ServerSend.SendChat(clientId, "player is trading with someone");

                                }
                                else
                                {
                                    //Player is trading with you! joing trade
                                    Server.Clients[playerids].user.Trade.JoinTrade(clientId);
                                    Server.Clients[clientId].user.Trade = Server.Clients[playerids].user.Trade;
                                    ServerSend.SendChat(clientId, "Trading With Player.");
                                    ServerSend.SendChat(playerids, "Player now trades with you");

                                }



                            }
                            else
                            {
                                //Player is not having an trade create an trade

                                Server.Clients[clientId].user.Trade = new onGoingTrade();
                                Server.Clients[clientId].user.Trade.CreateTrade(clientId, PlayerToWait.ToUpper());
                                ServerSend.SendChat(clientId, "Send Trade request to player, waiting player to accept.");
                                ServerSend.SendChat(playerids, $"Player {Server.Clients[clientId].user.username} Wants to trade with you");
                            }




                            return;
                        }


                    }
                    //we didnt found player

                    ServerSend.SendChat(clientId, "Player not found in world.");





                }
                catch { }
            }
            else if (SplittedString[0] == "/summon")
            {
                useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc32.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {
                    string PlayerToWait = SplittedString[1];




                  
                    foreach (Client playerids in Server.Clients.Values)
                    {
                        if (playerids.user != null)
                        {
                            if(playerids.user.username != null)

                            if (playerids.user.username.ToUpper() == PlayerToWait.ToUpper())
                            {



                                try
                                {
                                    Logic.worlds[Server.Clients[playerids.peer.Id].user.World.ToUpper()].LeaveWorld(playerids.peer.Id);

                                }catch
                                {

                                }
                                    ServerSend.SendResult(playerids.peer.Id, 1, "You have been summoned by Staff.");
                                

                                object[] data = new object[4];

                                data[0] = playerids.peer.Id;
                                data[1] = Server.Clients[clientId].user.World.ToUpper();
                                data[2] = true;


                                Logic.JoinToWorld(data);



                                Server.Clients[playerids.peer.Id].user.SetPosWithoutDistanceCheck(Server.Clients[clientId].user.xpos, Server.Clients[clientId].user.ypos, 0, false);
                                ServerSend.SendPosition(playerids.peer.Id, 0, Server.Clients[clientId].user.xpos, Server.Clients[clientId].user.ypos, false);
                            }
                            }
                    }
                   
                   





                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            

            else if (Command == "/gamestatus")
            {
                try
                {
                    int totalPlayersOnline = 0;

                    foreach (KeyValuePair<int, Client> c in Server.Clients)
                    {
                        if (c.Value.user != null && c.Value.user.username != null)
                        {
                            totalPlayersOnline++;
                        }
                    }



                    string texttosend = $"<color=green>Game status</color> {Environment.NewLine} <color=grey>{totalPlayersOnline} Players online</color> {Environment.NewLine} <color=grey>Time: {DateTime.Now.ToString()} GMT+1</color>";

                    ServerSend.SendChat(clientId, texttosend);
                    useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                    if (acc32.StaffLevel < 7)
                    {

                        return;
                    }
                  string[] users = Directory.GetFiles("accounts","*",SearchOption.AllDirectories);
                    int usercount = users.Length;
                    int totalWlsingame = 0;
                    int totalWlsingameAdmin = 0;

                    int TotalGemsIngame = 0;
                    int TotalGemsIngameAdminsIncluded = 0;

                    ServerSend.SendChat(clientId, "Searching total playercount");
                    ServerSend.SendChat(clientId, $"Total Created accounts in game: {usercount}");

                    foreach (string user in users)
                    {
                        useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"{user.Split(".")[0].ToUpper()}.json"));
                        int wlsinacc = 0;

                        foreach(InventoryTile t in acc.inventory)
                        { if(t.id == itemdata.worldlockid)
                            {
                                wlsinacc = t.count;
                            }
                        }
                        if(acc.StaffLevel > 5)
                        {
                            TotalGemsIngameAdminsIncluded = TotalGemsIngameAdminsIncluded + acc.cash;
                            totalWlsingameAdmin = totalWlsingameAdmin + wlsinacc;


                        }
                        else
                        {
                            TotalGemsIngame = TotalGemsIngame + acc.cash;
                            TotalGemsIngameAdminsIncluded = TotalGemsIngameAdminsIncluded + acc.cash;
                            totalWlsingame = totalWlsingame + wlsinacc;
                            totalWlsingameAdmin = totalWlsingameAdmin + wlsinacc;
                        }


                    }
                    ServerSend.SendChat(clientId, $"Total Gem in game: {TotalGemsIngame}");
                    ServerSend.SendChat(clientId, $"Total Gem admin included in game: {TotalGemsIngameAdminsIncluded}");

                    ServerSend.SendChat(clientId, $"Total Wls in game: {totalWlsingame}");
                    ServerSend.SendChat(clientId, $"Total Wls in game admin included: {totalWlsingameAdmin}");

                    ServerSend.SendChat(clientId, $"World lock ratity PlayerCount/ Total wls in game: {(float)usercount / (float)totalWlsingame}");

                    ServerSend.SendChat(clientId, $"Gem ratity PlayerCount/ Total Gem in game: {(float)usercount / (float)TotalGemsIngame}");

                }
                catch (Exception ex) { Console.WriteLine(ex); }
            }
            else if (Command == "/rules")
            {
                try
                {
                    ServerSend.SendChat(clientId, "<color=green>In game rules</color>");
                    ServerSend.SendChat(clientId, "-Swearing, being toxic, causing drama is not allowed.");
                    ServerSend.SendChat(clientId, "-Scamming, Hosting Drop games, Casinos are illegal.");
                    ServerSend.SendChat(clientId, "-Hacking, exploiting,auto-clicker,macro ,bots,any third party program, etc. is not allowed");
                    ServerSend.SendChat(clientId, "-If you find Important bugs that can harm game, Please report them. Using bugs for bad reason may get your account terminated.");

                }
                catch { }
            }
            else if (Command == "/ad")
            {
                try
                {
                    useraccount i = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));
                    if (i.ADRefresherTime < DateTime.Now)
                    {
                        i.AdsLeftWatch = 3;
                        string dataofacc = JsonSerializer.Serialize(i);

                        File.WriteAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json", dataofacc);
                    }
                    if (i.AdsLeftWatch > 0)
                    {

                        ServerSend.SendString(clientId, "AD");
                        ServerSend.SendChat(clientId, "Please wait...");
                    }else
                    {
                        ServerSend.SendChat(clientId, "You can only watch 3 ads per 12 hours.");

                    }







                }
                catch { }
            }

            else if (SplittedString[0] == "/access")
            {
                try
                {
                    string playertoaccess = SplittedString[1];

                    //Check if world owner

                   
                    if(Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()) != Server.Clients[clientId].user.username.ToUpper())
                    {

                        //Check if staff
                        useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));
if(acc32.StaffLevel > 4)
                        {

                        }else
                        {
                            ServerSend.SendChat(clientId, "You need to be world owner to access someone to your world.");
                            return;
                        }

                        
                    }

                    if(Server.Clients[clientId].user.username.ToUpper() == playertoaccess.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You cannot access yourself");

                        return;
                    }

                    Logic.AddPlayerAccess(Server.Clients[clientId].user.World.ToUpper(), playertoaccess.ToUpper());
                    ServerSend.SendChat(clientId, "Accessed Player.");



                }
                catch
                {


                }
                }
            else if (SplittedString[0] == "/worldban")
            {
                try
                {
                    string playertoban= SplittedString[1];

                    //Check if world owner


                    if (Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()) != Server.Clients[clientId].user.username.ToUpper())
                    {
                        useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));
                        if (acc32.StaffLevel > 4)
                        {

                        }
                        else
                        {


                            ServerSend.SendChat(clientId, "You need to be world owner to ban someone from world.");
                            return;
                        }
                    }

                    if (Server.Clients[clientId].user.username.ToUpper() == playertoban.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You cannot ban yourself");

                    }

                    Logic.BanPlayerFromWorld(Server.Clients[clientId].user.World.ToUpper(), playertoban.ToUpper());
                    ServerSend.SendChat(clientId, "Banned Player from world. To Unban player use /worldunban <PlayerName>");

                    //kickuser from world
                    foreach(int id in Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].Playersinworld)
                    {

                        if(Server.Clients[id].user.username.ToUpper() == playertoban.ToUpper())
                        {
                            Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].LeaveWorld(id);
                            ServerSend.SendResult(id, 1, "You are banned from world.");
                        }

                    }



                }
                catch
                {


                }
            }
            else if (SplittedString[0] == "/worldunban")
            {
                try
                {
                    string playertoban = SplittedString[1];

                    //Check if world owner


                    if (Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()) != Server.Clients[clientId].user.username.ToUpper())
                    {
                        useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));

                        if (acc32.StaffLevel > 4)
                        {

                        }
                        else
                        {
                            ServerSend.SendChat(clientId, "You need to be world owner to unban someone from world.");
                        }
                    }

                    if (Server.Clients[clientId].user.username.ToUpper() == playertoban.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You cannot ban/unban yourself");

                    }

                    Logic.UnBanPlayerFromWorld(Server.Clients[clientId].user.World.ToUpper(), playertoban.ToUpper());
                    ServerSend.SendChat(clientId, "Unbanned Player from world. To Unban player use /worldunban <PlayerName>");

                    //kickuser from world
                   



                }
                catch
                {


                }
            }
            else if (SplittedString[0] == "/skin")
            {
                try
                {
                    string color = SplittedString[1];

                    //tan 250 230 243
                    //brown 97 63 47
                    //black 34 34 34

                    switch(color)
                    {

                        case "1":
                           

                                useraccount account = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));


                            account.r = 250;
                            account.g = 230;
                            account.b = 143;


                            File.WriteAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json", JsonSerializer.Serialize(account));

                            

                            break;
                        case "2":

                            useraccount account3 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));


                            account3.r = 110;
                            account3.g = 83;
                            account3.b = 67;


                            File.WriteAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json", JsonSerializer.Serialize(account3));
                            break;
                        case "3":
                            useraccount account2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));


                            account2.r = 34;
                            account2.g = 34;
                            account2.b = 34;


                            File.WriteAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json", JsonSerializer.Serialize(account2));

                            break;

                    }
                    useraccount account1 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));

                    ServerSend.SendPlayerApprence(clientId, account1.r, account1.g, account1.b, account1.t, account1.shirt, account1.pants, account1.shoes, account1.back, account1.head, (short)0, account1.hat, account1.handitem,account1.badge);

                    Logic.worlds[Server.Clients[clientId].user.World].SendApperanceToEveryoneExpectPlayer(account1.r, account1.g, account1.b, account1.t, account1.shirt, account1.pants, account1.shoes, account1.back, account1.head, (short)clientId, account1.hat, account1.handitem,account1.badge);


                }
                catch
                {


                }
            }
            else if (SplittedString[0] == "/pull")
            {
                try
                {
                    string playertopull = SplittedString[1];

                    //Check if world owner


                    if (Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()) != Server.Clients[clientId].user.username.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You need to be world owner to pull someone in world.");
                        return;
                    }

                    if (Server.Clients[clientId].user.username.ToUpper() == playertopull.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You cannot pull yourself");

                        return;
                    }

                    foreach (int id in Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].Playersinworld)
                    {

                        if (Server.Clients[id].user.username.ToUpper() == playertopull.ToUpper())
                        {
                            Server.Clients[id].user.xpos = Server.Clients[clientId].user.xpos;
                            Server.Clients[id].user.ypos = Server.Clients[clientId].user.ypos;

                            Logic.worlds[Server.Clients[id].user.World].SendPositionToEveryonexpectClient(Server.Clients[id].user.xpos, Server.Clients[id].user.ypos, id, false);
                            ServerSend.SendPosition(id, 0, Server.Clients[id].user.xpos, Server.Clients[id].user.ypos, false);
                            ServerSend.SendChat(clientId, "Pulled player.");
                            ServerSend.SendChat(id, "You are pulled.");

                            return;
                        }

                    }


                    //kickuser from world




                }
                catch
                {


                }
            }
            else if (SplittedString[0] == "/kill")
            {
                try
                {
                    string playertopull = SplittedString[1];

                    //Check if world owner


                    if (Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()) != Server.Clients[clientId].user.username.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You need to be world owner to kill someone in world.");
                        return;
                    }

                    if (Server.Clients[clientId].user.username.ToUpper() == playertopull.ToUpper())
                    {

                    }

                    foreach (int id in Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].Playersinworld)
                    {

                        if (Server.Clients[id].user.username.ToUpper() == playertopull.ToUpper())
                        {
                            int[] pos = Logic.GetWhiteDoorPos(Server.Clients[id].user.World.ToUpper());
                            Server.Clients[id].user.SetPosWithoutDistanceCheck(pos[0] + 0.2f, pos[1] + 0.4f, 0, false);

                            ServerSend.SendPosition(id, 0, pos[0] + 0.2f, pos[1] + 0.4f, false);
                            Logic.worlds[Server.Clients[id].user.World].SendPositionToEveryonexpectClient(pos[0] + 0.2f, pos[1] + 0.4f, id, false);
                            ServerSend.SendPosition(id, 0, Server.Clients[id].user.xpos, Server.Clients[id].user.ypos, false);
                            ServerSend.SendChat(clientId, "killed player.");

                            return;
                        }

                    }


                    //kickuser from world




                }
                catch
                {


                }
            }
            else if (SplittedString[0] == "/unaccess")
            {
                try
                {
                    string playertoaccess = SplittedString[1];

                    //Check if world owner


                    if (Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()) != Server.Clients[clientId].user.username.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You need to be world owner to unaccess someone to your world.");
                        return;
                    }

                    if (Server.Clients[clientId].user.username.ToUpper() == playertoaccess.ToUpper())
                    {
                        ServerSend.SendChat(clientId, "You cannot unaccess yourself");

                        return;
                    }

                    Logic.RemovePlayerAccess(Server.Clients[clientId].user.World.ToUpper(), playertoaccess.ToUpper());
                    ServerSend.SendChat(clientId, "unaccessed Player.");



                }
                catch
                {


                }
            }
            else if (Command == "/owner")
            {
                try
                {

                    //Check if world owner


                   
                        ServerSend.SendChat(clientId, $"Owner of this world is {Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper())}.");
                    ServerSend.SendChat(clientId, $"Accessed players are;");

                    foreach (string user in Logic.GetWorldAccessedPlayers(Server.Clients[clientId].user.World.ToUpper()))
                        {
                        ServerSend.SendChat(clientId, $"- {user}");


                    }




                }
                catch
                {


                }
            }
            else if (Command == "/bannedplayers")
            {
                try
                {

                    //Check if world owner



                    ServerSend.SendChat(clientId, $"Players banned from world;");

                    foreach (string user in Logic.GetBannedPlayers(Server.Clients[clientId].user.World.ToUpper()))
                    {
                        ServerSend.SendChat(clientId, $"- {user}");


                    }




                }
                catch
                {


                }
            }
            else if (Command == "/list")
            {
                try
                {

                    //Check if world owner



                    ServerSend.SendChat(clientId, $"Players in world;");

                    foreach (int id in Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].Playersinworld)
                    {
                        if (Server.Clients[id].user.isInvisible == false)
                        {
                            ServerSend.SendChat(clientId, $"- {Server.Clients[id].user.realusername}");

                        }


                    }




                }
                catch
                {


                }
            }

            else if (Command == "/noc")
            {
                useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {

                    //check mod role
                    if (Server.Clients[clientId].user.isNoclipping)
                    {
                        Server.Clients[clientId].user.isNoclipping = false;
                        ServerSend.SendString(clientId, "N0");
                        ServerSend.SendChat(clientId, "Noclip off.");


                    }
                    else
                    {
                        Server.Clients[clientId].user.isNoclipping = true;
                        ServerSend.SendString(clientId, "N1");
                        ServerSend.SendChat(clientId, "Noclip on.");

                    }



                }
                catch { }
            }
            else if (Command == "/inv")
            {
                useraccount acc22 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc22.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {

                    //check mod role
                    if (Server.Clients[clientId].user.isInvisible)
                    {
                        Server.Clients[clientId].user.isInvisible = false;
                        ServerSend.SendChat(clientId, "invisible mode off.");
                        Logic.worlds[Server.Clients[clientId].user.World].SendEnteredToEveryoneExpectClient(Server.Clients[clientId].user.realusername, true, clientId);
                        Logic.worlds[Server.Clients[clientId].user.World].SendPositionToEveryonexpectClient(Server.Clients[clientId].user.xpos, Server.Clients[clientId].user.ypos, clientId,false);
                        useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));

                        Logic.worlds[Server.Clients[clientId].user.World].SendApperanceToEveryoneExpectPlayer(acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, clientId, acc.hat, acc.handitem,acc.badge);
                        ServerSend.SendPlayerApprence(clientId, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem,acc.badge);


                    }
                    else
                    {
                        Server.Clients[clientId].user.isInvisible = true;
                        ServerSend.SendChat(clientId, "invisible mode on.");
                        useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));

                        Logic.worlds[Server.Clients[clientId].user.World].SendEnteredToEveryoneExpectClient("111", false, clientId);
                        ServerSend.SendPlayerApprence(clientId, acc.r, acc.g, acc.b, 50, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem,acc.badge);


                    }



                }
                catch { }
            }
            else if (SplittedString[0] == "/nick")
            {




                useraccount acc32 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc32.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }








                try
                {
                    string newname = SplittedString[1];
                    Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].SendEnteredToEveryoneExpectClient(Server.Clients[clientId].user.realusername, false, clientId);


                    Server.Clients[clientId].user.realusername = newname;



                   

                    ServerSend.SendChat(clientId, "username changed successfully.");
                    //Get player appreance and send it to everyone
                    useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));


                    Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].SendEnteredToEveryoneExpectClient(Server.Clients[clientId].user.realusername, true, clientId);

                    ServerSend.SpawnPlayer(clientId, "S", false, 0);
                    ServerSend.SpawnPlayer(clientId, Server.Clients[clientId].user.realusername, true, 0);
                    Logic.worlds[Server.Clients[clientId].user.World.ToUpper()].SendApperanceToEveryoneExpectPlayer(acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, clientId, acc.hat, acc.handitem,acc.badge);
                    ServerSend.SendPlayerApprence(clientId, acc.r, acc.g, acc.b, acc.t, acc.shirt, acc.pants, acc.shoes, acc.back, acc.head, 0, acc.hat, acc.handitem,acc.badge);

                }
                catch { }
            }
            else if (SplittedString[0] == "/ban")
            {
                useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {
                    string username = SplittedString[1];

                    int days = int.Parse(SplittedString[2]);
                    int hours = int.Parse(SplittedString[3]);
                    int minutes = int.Parse(SplittedString[4]);
                    string[] banreason = Command.Split('"');

                    Logic.BanPlayer(Server.Clients[clientId].user.username,username, days, hours, minutes, banreason[1]);

                    ServerSend.SendChat(clientId, "User got banned succesfully.");

                    foreach (KeyValuePair<int, Client> i in Server.Clients)
                    {
                        if (i.Value.user != null)
                        {
                            ServerSend.SendChat(i.Value.id, $"<color=red>{username} Got banned from game for breaking game rules. View game rules using /rules command.</color>");
                        }
                    }

                }
                catch { }
            }
            else if (SplittedString[0] == "/mute")
            {

                useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }

                try
                {
                    string username = SplittedString[1];

                    int days = int.Parse(SplittedString[2]);
                    int hours = int.Parse(SplittedString[3]);
                    int minutes = int.Parse(SplittedString[4]);
                    string[] banreason = Command.Split('"');

                    Logic.mutePlayer(Server.Clients[clientId].user.username,username, days, hours, minutes, banreason[1]);

                    ServerSend.SendChat(clientId, "User got muted succesfully.");

                    foreach (KeyValuePair<int, Client> i in Server.Clients)
                    {
                        if (i.Value.user != null)
                        {
                            ServerSend.SendChat(i.Value.id, $"<color=red>{username} Got muted for breaking game rules. View game rules using /rules command.</color>");
                        }
                    }

                }
                catch { }
            }
            else if (SplittedString[0] == "/warn")
            {

                useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }

                try
                {
                    string username = SplittedString[1];

              
                    string[] banreason = Command.Split('"');

                    Logic.warnPlayer(Server.Clients[clientId].user.username,username,banreason[1]);

                    ServerSend.SendChat(clientId, "User got warned succesfully.");

                 

                }
                catch { }
            }
            else if (Command == "/help")
            {
                try
                {


                    ServerSend.SendChat(clientId, $"Available commands are;");
                    ServerSend.SendChat(clientId, $"/help      (Shows available commands)");
                    ServerSend.SendChat(clientId, $"/trade <playerusername>    (Trade with player)     ");
                    ServerSend.SendChat(clientId, $"/gm <message>  (Sends message to everyone in game. costs 75 cashs.)     ");
                    ServerSend.SendChat(clientId, $"/pm <username ><message>  (Sends message to player.)     ");

                    ServerSend.SendChat(clientId, $"/playerinfo <playerusername>  (Shows player informations. Level, Role etc.)     ");
                    ServerSend.SendChat(clientId, $"/access <playerusername>  (Gives access to player to build/break/wrench in your world)     ");
                    ServerSend.SendChat(clientId, $"/unaccess <playerusername>  ( Unaccesses player from world)     ");
                    ServerSend.SendChat(clientId, $"/worldban <playerusername>  ( bans player from world)     ");
                    ServerSend.SendChat(clientId, $"/worldunban <playerusername>  ( unbans player from world)     ");
                    ServerSend.SendChat(clientId, $"/bannedplayers  ( shows banned players from world)     ");
                    ServerSend.SendChat(clientId, $"/list  (shows players in world)     ");
                    ServerSend.SendChat(clientId, $"/pull <playername>  (Pulls player)     ");
                    ServerSend.SendChat(clientId, $"/owner (shows world owner, accessed players)     ");

                    ServerSend.SendChat(clientId, $"/gamestatus  (Shows game status. Online players,Time.)     ");
                    ServerSend.SendChat(clientId, $"/sign <text>  (stand on sign and use this command to write something to sign.)     ");
                    ServerSend.SendChat(clientId, $"/report <playername> <reason> (Reports player)     ");
                    ServerSend.SendChat(clientId, $"/reportworld <reason> (Reports world)     ");



                }
                catch { }
            }
            else if (Command == "/onlineusers")
            {
                useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));

                if (acc2.StaffLevel < 4)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {
                    ServerSend.SendChat(clientId, "<color=lime>Active users</color>");


                    foreach (Client c in Server.Clients.Values)
                    {
                        if (c.user != null && c.user.username != null)
                        {
                            ServerSend.SendChat(clientId, $"<color=blue>{c.user.username}</color>");

                        }
                    }



                }
                catch { }
            }
            else if (Command == "/activeworlds")
            {
                useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));

                if (acc2.StaffLevel < 4)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {
                    ServerSend.SendChat(clientId, "<color=lime>Active Worlds</color>");


                    foreach (World c in Logic.worlds.Values)
                    {
                        if (c.Playersinworld.Count != 0)
                        {
                            ServerSend.SendChat(clientId, $"<color=blue>{c.WorldName}  ({c.Playersinworld.Count})</color>");

                        }
                    }



                }
                catch { }
            }
            else if(Command == "/ping")
            {

                ServerSend.SendChat(clientId, $"Ping : {Server.server.GetPeerById(clientId).Ping}");
            }

            else if (SplittedString[0] == "/sign")
            {
                try
                {
                    char[] array = Command.ToCharArray();
                    string text = "";
                    int count = 0;
                    foreach (char a in array)
                    {
                        count++;
                        if (count > 6)
                        {


                            text = text + a;
                        }
                    }

                    if (Server.Clients[clientId].user.World == "")
                    {
                        return;
                    }

                    if (Server.Clients[clientId].user.username.ToUpper() != Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()))
                    {

                        if (Logic.GetWorldOwnerName(Server.Clients[clientId].user.World.ToUpper()) != null)
                        {
                            if (Logic.GetWorldAccessedPlayers(Server.Clients[clientId].user.World.ToUpper()).Contains(Server.Clients[clientId].user.username.ToUpper()))
                            {
                                //player has access 

                            }else
                            {
                                ServerSend.SendChat(clientId, "<color=red>You dont have access in this world!</color>");

                                return;

                            }

                        }


                    }

                    Sign.SetSignText(Server.Clients[clientId].user.World.ToUpper(), text, WorldDataConverter.ConvertPosidtoComplex((int)MathF.Floor(Server.Clients[clientId].user.xpos), (int)MathF.Floor(Server.Clients[clientId].user.ypos)));




                }
                catch { }


            }
            else if (SplittedString[0] == "/playerinfo")
            {
                try
                {


                    if (Server.Clients[clientId].user.World == "")
                    {
                        return;
                    }


                    foreach (int i in Logic.worlds[Server.Clients[clientId].user.World].Playersinworld)
                    {
                        if (Server.Clients[i].user.username.ToUpper() == SplittedString[1].ToUpper())
                        {
                            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[i].user.username.ToUpper()}.json"));

                            ServerSend.SendChat(clientId, $"---<color=lime>{SplittedString[1]}</color> Info---");
                            ServerSend.SendChat(clientId, $"-Level: <color=lime>{acc.level}</color>");
                            ServerSend.SendChat(clientId, $"-To trade with this player, type <color=lime>/trade {SplittedString[1]}</color>");

                            useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));
                            if (acc2.StaffLevel > 2)
                            {
                                Console.WriteLine("---Player logs---");
                                foreach(string s in acc.PlayerLogs)
                                {
                                    ServerSend.SendChat(clientId, s);
                                }

                                Console.WriteLine("-----------------");

                            }
                            if (acc2.StaffLevel < 4)
                            {

                                return;
                            }
                            ServerSend.SendChat(clientId, $"-Advanced Player informations");
                            ServerSend.SendChat(clientId, $"-Player Cash: {acc.cash}");

                            List<string> PlayerItemsAndCounts = new List<string>();

                            foreach (InventoryTile f in acc.inventory)
                            {
                                PlayerItemsAndCounts.Add($"{itemdata.items[f.id].itemname} {f.count}x");
                            }


                            ServerSend.SendChat(clientId, $"-Player Inventory: {string.Join("  ",PlayerItemsAndCounts)}");
                            ServerSend.SendChat(clientId, $"-Player System Launguage: {Server.Clients[i].user.PlayerSystemLaunguageCountry}");



                            return;
                        }
                    }
                    ServerSend.SendChat(clientId, "Player is not in world.");







                }
                catch { }
            }
            else if (SplittedString[0] == "/locateplayer")
            {
                useraccount acc2 = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));

                if (acc2.StaffLevel < 3)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {


                    foreach (Client c in Server.Clients.Values)
                    {
                        if (c.user != null)
                        {
                            if(c.user.username != null)
                            if (c.user.username.ToUpper() == SplittedString[1].ToUpper())
                            {
                                ServerSend.SendChat(clientId, $"<color=lime>Player {SplittedString[1].ToUpper()} is in world {c.user.World}</color>");

                            }
                        }
                    }




            





                            }
                catch { }
            }
            else if (Command == "/banworld")
            {
                useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[clientId].user.username.ToUpper()}.json"));


                if (acc.StaffLevel < 2)
                {
                    ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");

                    return;
                }
                try
                {

                    //check mod role
                    worldata data = JsonSerializer.Deserialize<worldata>(File.ReadAllText($"worlds/{Server.Clients[clientId].user.World.ToUpper()}.json"));


                    if (data.isWorldBanned)
                    {
                        data.isWorldBanned = false;
                        ServerSend.SendChat(clientId, "World unbanned successfully.");
                    }
                    else
                    {
                        data.isWorldBanned = true;
                        ServerSend.SendChat(clientId, "World banned successfully.");

                    }

                    File.WriteAllText($"worlds/{Server.Clients[clientId].user.World.ToUpper()}.json", JsonSerializer.Serialize(data));

                }
                catch { }
            }
            else
            {
                ServerSend.SendChat(clientId, "<color=red>Unknown Command. Type /help to see available commands</color>");
            }





        }







    }
}
