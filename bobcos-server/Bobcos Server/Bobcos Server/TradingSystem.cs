using GameServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bobcos_Server
{
    class TradingSystem
    {
    }

    public class onGoingTrade
    {

        public int player1id, player2id;
        public string PlayerToWait;
        public short item1, item1Count, item2, item2count, item3, item3count, item4, item4count;
        public short item1_2, item1Count_2, item2_2, item2count_2, item3_2, item3count_2, item4_2, item4count_2;
        public bool player1accepted, player2accepted;
        

        public void CreateTrade(int plrid,string _playertowait) //setting player1
        {
            ServerSend.EnableTradeUI(plrid);
            player1id = plrid;
            PlayerToWait = _playertowait;
            if (Server.Clients[plrid].user.World != null)
            {
                Logic.worlds[Server.Clients[plrid].user.World.ToUpper()].SendChattingBubbleToEveryone(plrid, 2);
            }
        }
        public void JoinTrade(int plrid) //Setting player2
        {

            if(player2id != 0)
            {
                return;
            }
            if (Server.Clients[plrid].user.World != null)
            {
                Logic.worlds[Server.Clients[plrid].user.World.ToUpper()].SendChattingBubbleToEveryone(plrid, 2);
            }
            ServerSend.EnableTradeUI(plrid);

            ServerSend.UpdateTradeUI(plrid, item1, item2, item3, item4, item1Count, item2count, item3count, item4count);

            if (Server.Clients[plrid].user.username.ToUpper() == PlayerToWait.ToUpper())
            {
                player2id = plrid;
                //Send player 2 joined message
                ServerSend.SendChat(player1id, "Player is now trading with you.");
            }
        }

        public void AddItems(int plrid,short _item1,short _item1count,short _item2,short _item2count,short _item3,short _item3count,short _item4,short _item4count)
        {
            //Check item counts
            Console.WriteLine("Updated");





            if(_item1count < -1)
            {
                Console.WriteLine("Uff");

                return;
            }
            if (_item2count < -1)
            {
                Console.WriteLine("Uff");

                return;
            }
            if (_item3count < -1)
            {
                Console.WriteLine("Uff");

                return;
            }

            if (_item4count < -1)
            {
                Console.WriteLine("Uff");

                return;
            }

            if (_item1 == -1)
            {
                _item1 = 0;
            }
            if (_item2 == -1)
            {
                _item2 = 0;
            }
            if (_item3 == -1)
            {
                _item3 = 0;
            }
            if (_item4 == -1)
            {
                _item4 = 0;
            }
            if(_item1 != 0 && _item2 != 0)
            {
                if (_item1 == _item2)
                {
                    Console.WriteLine("Updated");

                    return;
                }
            }
            if (_item1 != 0 && _item3 != 0)
            {
                if (_item1 == _item3)
                {
                    return;
                }
            }
            if (_item1 != 0 && _item4 != 0)
            {
                if (_item1 == _item4)
                {
                    return;
                }
            }

            if (_item2 != 0 && _item3 != 0)
            {
                if (_item2 == _item3)
                {
                    return;
                }
            }
            if (_item2 != 0 && _item3 != 0)
            {
                if (_item2 == _item3)
                {
                    return;
                }
            }
            if (_item2 != 0 && _item4 != 0)
            {
                if (_item2 == _item4)
                {
                    return;
                }
            }
            if (_item3 != 0 && _item4 != 0)
            {
                if (_item3 == _item4)
                {
                    return;
                }
            }




            if (plrid == player1id)
            {
                bool isfound = false;
                if (_item1 != 0)
                {


                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item1)
                        {
                            if (_item1count > i.count)
                            {
                                return;
                            }
                            if (_item1count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }

                    if (isfound == false)
                    {
                        Console.WriteLine("Returned 1");
                        return;
                    }
                }
                 isfound = false;
                if (_item2 != 0)
                {
                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item2)
                        {
                            if (_item2count > i.count)
                            {

                                return;
                            }
                            else if (_item2count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }
                    if (isfound == false)
                    {
                        Console.WriteLine("Returned 2");

                        return;
                    }
                }
                isfound = false;
                if (_item3 != 0)
                {
                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item3)
                        {
                            if (_item3count > i.count)
                            {

                                return;
                            }
                            else if (_item3count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }
                    if (isfound == false)
                    {
                        return;
                    }
                }
                isfound = false;
                if (_item4 != 0)
                {
                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item4)
                        {
                            if (_item4count > i.count)
                            {

                                return;
                            }
                            else if (_item4count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }

                    if (isfound == false)
                    {
                        return;
                    }
                }

                if (_item1count > 0)
                {
                    item1 = _item1;
                    item1Count = _item1count;

                }
                else
                {
                    item1 = 0;

                }


                if (_item2count > 0)
                {
                    item2 = _item2;
                    item2count = _item2count;

                }
                else
                {
                    item2 = 0;

                }



                if (_item3count > 0)
                {
                    item3= _item3;
                    item3count = _item3count;

                }
                else
                {
                    item3 = 0;

                }




                if (_item4count > 0)
                {
                    item4 = _item4;
                    item4count = _item4count;

                }
                else
                {
                    item4 = 0;

                }
                if(item1Count < 0)
                {
                    item1Count = 0;
                }
                if (item2count < 0)
                {
                    item2count = 0;
                }
                if (item3count < 0)
                {
                    item3count = 0;
                }
                if (item4count < 0)
                {
                    item4count = 0;
                }

                player1accepted = false;
                player2accepted = false;

                //Send changes to player 2
                if (player2id != 0)
                {
                    ServerSend.UpdateTradeUI(player2id, item1, item2, item3, item4, item1Count, item2count, item3count, item4count);

                }else
                {
                    Console.WriteLine("Changes not send to player 2");
                }


            }
            else
            if (plrid == player2id)
            {
                bool isfound = false;
                if (_item1 != 0)
                {
                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item1)
                        {
                            if (_item1count > i.count)
                            {


                                return;
                            }
                            else if (item1Count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }
                    if (isfound == false)
                    {
                        return;
                    }
                }
                isfound = false; if (_item2 != 0)
                {
                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item2)
                        {
                            if (_item2count > i.count)
                            {

                                return;
                            }
                            else if (item1Count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }
                    if (isfound == false)
                    {
                        return;
                    }
                }
                isfound = false;
                if (_item3 != 0)
                {
                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item3)
                        {
                            if (_item3count > i.count)
                            {

                                return;
                            }
                            else if (item1Count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }
                    if (isfound == false)
                    {
                        return;
                    }
                }
                isfound = false;
                if (_item4 != 0)
                {
                    foreach (InventoryTile i in Logic.GetInventory(Server.Clients[plrid].user.username.ToUpper()))
                    {
                        if (i.id == _item4)
                        {
                            if (_item4count > i.count)
                            {

                                return;
                            }
                            else if (item1Count <= i.count)
                            {
                                isfound = true;
                            }
                        }
                    }
                    if (isfound == false)
                    {
                        return;
                    }
                }


                if(_item1count > 0)
                {
                    item1_2 = _item1;
                    item1Count_2 = _item1count;

                }
                else
                {
                    item1_2 = 0;

                }


                if (_item2count > 0)
                {
                    item2_2 = _item2;
                    item2count_2 = _item2count;

                }
                else
                {
                    item2_2 = 0;

                }

                if (_item3count > 0)
                {
                    item3_2 = _item3;
                    item3count_2 = _item3count;

                }
                else
                {
                    item3_2 = 0;

                }


                if (_item4count > 0)
                {
                    item4_2 = _item4;
                    item4count_2 = _item4count;

                }
                else
                {
                    item4_2 = 0;

                }


                if(item1Count_2 < 0)
                {
                    item1Count_2 = 0;
                }
                if (item2count_2 < 0)
                {
                    item2count_2 = 0;
                }
                if (item3count_2 < 0)
                {
                    item3count_2 = 0;
                }
                if (item4count_2 < 0)
                {
                    item4count_2 = 0;
                }


                player1accepted = false;
                player2accepted = false;

                //Send changes to player 1
                ServerSend.UpdateTradeUI(player1id, item1_2, item2_2, item3_2, item4_2, item1Count_2, item2count_2, item3count_2, item4count_2);

            }
            else
            {
            }


        }

        public void TryCompleteTrade(int plrid)
        {

            if(player2id == 0)
            {
                //Send player 2 not joined message

                return;



            }


          if(!  Logic.CheckItemInInventory(Server.Clients[player1id].user.username.ToUpper(),item1,item1Count))
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[player1id].user.username.ToUpper(), item2, item2count))
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[player1id].user.username.ToUpper(), item3, item3count))
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[player1id].user.username.ToUpper(), item4, item4count))
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[player2id].user.username.ToUpper(), item1_2, item1Count_2))
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[player2id].user.username.ToUpper(), item2_2, item2count_2))
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[player2id].user.username.ToUpper(), item3_2, item3count_2))
            {
                return;
            }
            if (!Logic.CheckItemInInventory(Server.Clients[player2id].user.username.ToUpper(), item4_2, item4count_2))
            {
                return;
            }


            if (plrid == player1id)
            {
               
                player1accepted = true;
            }

            if (plrid == player2id)
            {
              
                player2accepted = true;

            }

            if(player1accepted == true && player2accepted == true)
            {
                //transfer
                ServerSend.SendChat(player1id, "Completing trade.");



                //Check can take 

                if (Logic.CanTakeItem(Server.Clients[player1id].user.username.ToUpper(), item1_2, item1Count_2) == false)
                {
                    ServerSend.SendChat(player1id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player2id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player1id);

                    return;
                }
                if (Logic.CanTakeItem(Server.Clients[player1id].user.username.ToUpper(), item2_2, item2count_2) == false)
                {
                    ServerSend.SendChat(player1id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player2id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player1id);

                    return;
                }
                if (Logic.CanTakeItem(Server.Clients[player1id].user.username.ToUpper(), item3_2, item3count_2) == false)
                {
                    ServerSend.SendChat(player1id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player2id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player1id);

                    return;
                }
                if (Logic.CanTakeItem(Server.Clients[player1id].user.username.ToUpper(), item4_2, item4count_2) == false)
                {
                    ServerSend.SendChat(player1id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player2id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player1id);

                    return;
                }


                if (Logic.CanTakeItem(Server.Clients[player2id].user.username.ToUpper(), item1, item1Count) == false)
                {
                    ServerSend.SendChat(player2id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player1id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player2id);

                    return;
                }
                if (Logic.CanTakeItem(Server.Clients[player2id].user.username.ToUpper(), item2, item2count) == false)
                {
                    ServerSend.SendChat(player2id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player1id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player2id);

                    return;
                }
                if (Logic.CanTakeItem(Server.Clients[player2id].user.username.ToUpper(), item3, item3count) == false)
                {
                    ServerSend.SendChat(player2id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player1id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player2id);

                    return;
                }
                if (Logic.CanTakeItem(Server.Clients[player2id].user.username.ToUpper(), item4, item4count) == false)
                {
                    ServerSend.SendChat(player2id, "Trade cancelled : Your Inventory is full");
                    ServerSend.SendChat(player1id, "Trade cancelled : Players inventory is full");
                    CancelTrade(player2id);

                    return;
                }

                //take items from player 1
                Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item1, -item1Count);
                Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item2, -item2count);
                Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item3, -item3count);
                Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item4, -item4count);

                //give items to player 2
                if(item1 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item1, item1Count);

                }
                if (item2 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item2, item2count);

                }
                if (item3 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item3, item3count);

                }
                if (item4 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item4, item4count);

                }

                //take items from player 2
                Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item1_2, -item1Count_2);
                Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item2_2, -item2count_2);
                Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item3_2, -item3count_2);
                Logic.AddItemToInventory(Server.Clients[player2id].user.username.ToUpper(), item4_2, -item4count_2);

                //give items to player 1
                if (item1_2 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item1_2, item1Count_2);

                }
                if (item2_2 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item2_2, item2count_2);

                }
                if (item3_2 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item3_2, item3count_2);

                }
                if (item4_2 > 0)
                {
                    Logic.AddItemToInventory(Server.Clients[player1id].user.username.ToUpper(), item4_2, item4count_2);

                }



                Logic.GetInventoryAndSend(player1id, Server.Clients[player1id].user.username.ToUpper());
                Logic.GetInventoryAndSend(player2id, Server.Clients[player2id].user.username.ToUpper());


                ServerSend.SendChat(player1id, "Trade completed.");

                //Save log




                if (Server.Clients[player1id].user.World != null)
                {
                    Logic.worlds[Server.Clients[player1id].user.World.ToUpper()].SendChattingBubbleToEveryone(player1id, 0);
                }
                if (Server.Clients[player2id].user.World != null)
                {
                    Logic.worlds[Server.Clients[player2id].user.World.ToUpper()].SendChattingBubbleToEveryone(player2id, 0);

                }





                if (player2id != 0)
                {
                    ServerSend.DisableTradeUI(player2id);
                    Server.Clients[player2id].user.Trade = null;


                }
                if (player1id != 0)
                {
                    ServerSend.DisableTradeUI(player1id);
                    Server.Clients[player1id].user.Trade = null;
                }

            }
            else
            {
                //Send waiting for player message
                ServerSend.SendChat(plrid, "Waiting for other player to accept.");
                if(player1accepted)
                {
                    ServerSend.SendChat(player2id, "Player accepted trade, If you accept, the trade will complete.");

                }
                if (player2accepted)
                {
                    ServerSend.SendChat(player1id, "Player accepted trade, If you accept, the trade will complete.");

                }

            }
        }

        public void CancelTrade(int plrid)
        {
            //Send cancelled message to players and destroy

            ServerSend.SendChat(player1id, "Trade cancelled.");
            ServerSend.SendChat(player2id, "Trade cancelled.");
           ServerSend.DisableTradeUI(player2id);
         ServerSend.DisableTradeUI(player1id);
            if (Server.Clients[player1id].user.World != null)
            {
                Logic.worlds[Server.Clients[player1id].user.World.ToUpper()].SendChattingBubbleToEveryone(player1id, 0);
            }
            if (Server.Clients[player2id].user.World != null)
            {
                Logic.worlds[Server.Clients[player2id].user.World.ToUpper()].SendChattingBubbleToEveryone(player2id, 0);
            }
            Server.Clients[player1id].user.Trade = null;
            Server.Clients[player2id].user.Trade = null;

            player1id = 0;
            player2id = 0;
       

        }

    }
}
