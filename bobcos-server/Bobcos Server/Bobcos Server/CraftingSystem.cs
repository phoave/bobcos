using GameServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Bobcos_Server
{
    class CraftingSystem
    {
        public static CraftRecipe[] recipe = new CraftRecipe[34 ];

        public static void Initiliaze()
        {
            recipe[0] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 60, item1id = 3, item2id = 3, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 12, recipeid = 0 };
            recipe[1] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 12, item2id = 69, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 14, recipeid = 1 };
            recipe[2] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 12, item2id = 70, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 15, recipeid = 2 };
            recipe[3] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 12, item2id = 72, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 16, recipeid = 3 };
            recipe[4] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 15, item2id = 16, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 17, recipeid = 4 };
            recipe[5] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 14, item2id = 20, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 18, recipeid = 5 };
            recipe[6] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 14, item2id = 100, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 19, recipeid = 6 };
            recipe[7] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 12, item2id = 71, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 20, recipeid = 7 };
            recipe[8] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 14, item2id = 15, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 21, recipeid = 8 };
            recipe[9] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 12, item2id = 73, item1idMinCount = 1, item2idMinCount = 2, ItemResult = 22, recipeid = 9 };
            recipe[10] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 100, item2id = 22, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 24, recipeid = 10 };
            recipe[11] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 90, item1id = 37, item2id = 66, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 33, recipeid = 11 };
            recipe[12] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 90, item1id = 10, item2id = 16, item1idMinCount = 10, item2idMinCount = 3, ItemResult = 47, recipeid = 12 };
            recipe[13] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 90, item1id = 33, item2id = 72, item1idMinCount = 5, item2idMinCount = 2, ItemResult = 48, recipeid = 13 };
            recipe[14] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 300, item1id = 12, item2id = 37, item1idMinCount = 3, item2idMinCount = 2, ItemResult = 49, recipeid = 14 };
            recipe[15] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 300, item1id = 12, item2id = 49, item1idMinCount = 3, item2idMinCount = 1, ItemResult = 50, recipeid = 15 };
            recipe[16] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 300, item1id = 12, item2id = 22, item1idMinCount = 3, item2idMinCount = 1, ItemResult = 63, recipeid = 16 };
            recipe[17] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 78, item1id = 73, item2id = 37, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 65, recipeid = 17 };
            recipe[18] = new CraftRecipe() { constantResultCount = 10, CraftingTime = 60, item1id = 37, item2id = 81, item1idMinCount = 3, item2idMinCount = 2, ItemResult = 66, recipeid = 18 };
            recipe[19] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 60, item1id = 3, item2id = 4, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 72, recipeid = 19 };
            recipe[20] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 12, item2id = 65, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 100, recipeid = 20 };
            recipe[21] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 60, item1id = 12, item2id = 4, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 13, recipeid = 21 };
            recipe[22] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 72, item1id = 65, item2id = 100, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 51, recipeid = 22 };
            recipe[23] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 14, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 52, recipeid = 23 };
            recipe[24] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 16, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 53, recipeid = 24 };
            recipe[25] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 15, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 54, recipeid = 25 };
            recipe[26] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 19, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 55, recipeid = 26 };
            recipe[27] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 21, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 56, recipeid = 27 };
            recipe[28] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 17, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 57, recipeid = 28 };
            recipe[29] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 22, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 58, recipeid = 29 };
            recipe[30] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 24, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 59, recipeid = 30 };
            recipe[31] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 100, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 60, recipeid = 31 };
            recipe[32] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 20, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 99, recipeid = 32 };
            recipe[33] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 120, item1id = 13, item2id = 18, item1idMinCount = 1, item2idMinCount = 1, ItemResult = 102, recipeid = 33 };

        }


        public static void BeginCrafting(int clientId, int item1id, int item2id)
        {
            // user

            ServerSend.SendWarning(clientId, "Menu crafting is disabled, Please use crafting machine.");
            return;
            foreach (CraftRecipe r in recipe)
            {
                if (r.item1id == item1id && r.item2id == item2id)
                {
                    //Recipe found, check inventory item Count

                    foreach (InventoryTile t in Logic.GetInventory(Server.Clients[clientId].user.username))
                    {
                        if (t.id == item1id)
                        {
                            if (t.count < r.item1idMinCount)
                            {

                                // You dont have enough item
                                ServerSend.SendChat(clientId, "<color=red>You dont have enough item to craft!</color>");

                                return;
                            }
                            else
                            {

                                foreach (InventoryTile t2 in Logic.GetInventory(Server.Clients[clientId].user.username))
                                {
                                    if (t2.id == item2id)
                                    {
                                        if (t2.count < r.item2idMinCount)
                                        {

                                            // You dont have enough item
                                            ServerSend.SendChat(clientId, "<color=red>You dont have enough item to craft!</color>");
                                            return;
                                        }
                                        else
                                        {
                                            Logic.AddItemToInventory(Server.Clients[clientId].user.username, t2.id, -r.item2idMinCount);

                                            Logic.AddItemToInventory(Server.Clients[clientId].user.username, t.id, -r.item1idMinCount);




                                            ServerSend.SendChat(clientId, "<color=yellow>Crafting item!</color>");

                                            CraftData data = new CraftData();
                                            data.resultItem = r.ItemResult;
                                            data.craftDate = DateTime.Now.AddSeconds(r.CraftingTime);
                                            data.recipeid = r.recipeid;
                                            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));

                                            acc.Craftdata1 = data;


                                            string dataofacc = JsonSerializer.Serialize(acc);

                                            File.WriteAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json", dataofacc);
                                            Logic.GetInventoryAndSend(clientId, Server.Clients[clientId].user.username.ToUpper());

                                            //save data
                                            return;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    return;








                }

                if (r.item1id == item2id && r.item2id == item1id)
                {
                    //Recipe found, check inventory item Count

                    foreach (InventoryTile t in Logic.GetInventory(Server.Clients[clientId].user.username))
                    {
                        if (t.id == item1id)
                        {
                            if (t.count < r.item1idMinCount)
                            {

                                // You dont have enough item
                                ServerSend.SendChat(clientId, "<color=red>You dont have enough item to craft!</color>");

                                return;
                            }
                            else
                            {




                                foreach (InventoryTile t2 in Logic.GetInventory(Server.Clients[clientId].user.username))
                                {
                                    if (t2.id == item2id)
                                    {
                                        if (t2.count < r.item2idMinCount)
                                        {

                                            // You dont have enough item
                                            ServerSend.SendChat(clientId, "<color=red>You dont have enough item to craft!</color>");
                                            return;
                                        }
                                        else
                                        {
                                            Logic.AddItemToInventory(Server.Clients[clientId].user.username, t2.id, -r.item2idMinCount);
                                            Logic.AddItemToInventory(Server.Clients[clientId].user.username, t.id, -r.item1idMinCount);

                                            ServerSend.SendChat(clientId, "<color=yellow>Crafting item!</color>");

                                            CraftData data = new CraftData();
                                            data.resultItem = r.ItemResult;
                                            data.craftDate = DateTime.Now.AddSeconds(r.CraftingTime);
                                            data.recipeid = r.recipeid;
                                            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json"));

                                            acc.Craftdata1 = data;


                                            string dataofacc = JsonSerializer.Serialize(acc);

                                            File.WriteAllText("accounts/" + Server.Clients[clientId].user.username.ToUpper() + ".json", dataofacc);
                                            Logic.GetInventoryAndSend(clientId, Server.Clients[clientId].user.username.ToUpper());

                                            //save data
                                            return;
                                        }
                                    }
                                }

                            }
                        }
                    }
                    return;








                }


            }


        }


        public static bool CheckIfCrafting(int cliemtId)
        {
            //user data
            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[cliemtId].user.username.ToUpper() + ".json"));



            if (acc.Craftdata1.resultItem == 0)
            {

                return false;
            }
            else
            {
                return true;
            }
        }


        public static void TryToGetCraftedItem(int clientid)
        {
            //user

            useraccount acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientid].user.username.ToUpper() + ".json"));
            if (!Logic.CheckItemInInventory(Server.Clients[clientid].user.username.ToUpper(), acc.Craftdata1.resultItem, recipe[acc.Craftdata1.recipeid].constantResultCount))
            {
                //inventory full
                ServerSend.SendChat(clientid, "<color=red>Inventory Full!</color>");

                return;
            }
            if (acc.Craftdata1.craftDate < DateTime.Now)
            {
                // get item
                Logic.AddItemToInventory(Server.Clients[clientid].user.username.ToUpper(), acc.Craftdata1.resultItem, recipe[acc.Craftdata1.recipeid].constantResultCount);
                Logic.GetInventoryAndSend(clientid, Server.Clients[clientid].user.username.ToUpper());
                ServerSend.SendChat(clientid, $"<color=yellow>Crafted Item! You got {recipe[acc.Craftdata1.recipeid].constantResultCount} {itemdata.items[acc.Craftdata1.resultItem].itemname} </color>");
                acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText("accounts/" + Server.Clients[clientid].user.username.ToUpper() + ".json"));
                //refresh acc
                acc.Craftdata1 = new CraftData() { resultItem = 0, recipeid = 0 };

                string dataofacc = JsonSerializer.Serialize(acc);

                File.WriteAllText("accounts/" + Server.Clients[clientid].user.username.ToUpper() + ".json", dataofacc);

                //get item



            }
            else
            {
                //Dont get item

                ServerSend.SendChat(clientid, "<color=red>Still crafting item! The crafting will complete at " + (acc.Craftdata1.craftDate - DateTime.Now).ToString() + "</color>");

            }

        }


        public static void BeginCrafting(int clientId, int item1id, int item2id, int order)
        {
            // user
            foreach (CraftRecipe r in recipe)
            {
                if (r.item1id == item1id && r.item2id == item2id)
                {

                    //Recipe found, check inventory item Count

                   if(! Logic.CheckItemInInventory(Server.Clients[clientId].user.username.ToUpper(), item1id,r.item1idMinCount))
                    {
                        return;
                    }
                    if (!Logic.CheckItemInInventory(Server.Clients[clientId].user.username.ToUpper(), item2id, r.item2idMinCount))
                    {
                        return;
                    }

                    Logic.AddItemToInventory(Server.Clients[clientId].user.username, r.item1id, -r.item2idMinCount);
                    Logic.AddItemToInventory(Server.Clients[clientId].user.username, r.item2id, -r.item1idMinCount);

                    ServerSend.SendChat(clientId, "<color=yellow>Crafting item!</color>");

                    CraftData data = new CraftData();
                    data.resultItem = r.ItemResult;
                    data.craftDate = DateTime.Now.AddSeconds(r.CraftingTime);
                    data.recipeid = r.recipeid;
                    WorldCraftData craftdatamap = JsonSerializer.Deserialize<WorldCraftData>(File.ReadAllText("worldcraftdata/" + Server.Clients[clientId].user.World.ToUpper() + ".json"));

                    if (craftdatamap.Data == null)
                    {
                        craftdatamap = new WorldCraftData();
                        craftdatamap.Data = new CraftData[4703];

                    }

                    craftdatamap.Data[order] = data;


                    string dataofacc = JsonSerializer.Serialize(craftdatamap);

                    File.WriteAllText("worldcraftdata/" + Server.Clients[clientId].user.World.ToUpper() + ".json", dataofacc);
                    Logic.GetInventoryAndSend(clientId, Server.Clients[clientId].user.username.ToUpper());

                    //save data
                    return;
















                }

                if (r.item1id == item2id && r.item2id == item1id)
                {

                    //Recipe found, check inventory item Count

                    if (!Logic.CheckItemInInventory(Server.Clients[clientId].user.username.ToUpper(), item1id, r.item1idMinCount))
                    {
                        return;
                    }
                    if (!Logic.CheckItemInInventory(Server.Clients[clientId].user.username.ToUpper(), item2id, r.item2idMinCount))
                    {
                        return;
                    }

                    Logic.AddItemToInventory(Server.Clients[clientId].user.username, r.item1id, -r.item2idMinCount);
                    Logic.AddItemToInventory(Server.Clients[clientId].user.username, r.item2id, -r.item1idMinCount);

                    ServerSend.SendChat(clientId, "<color=yellow>Crafting item!</color>");

                    CraftData data = new CraftData();
                    data.resultItem = r.ItemResult;
                    data.craftDate = DateTime.Now.AddSeconds(r.CraftingTime);
                    data.recipeid = r.recipeid;
                    WorldCraftData craftdatamap = JsonSerializer.Deserialize<WorldCraftData>(File.ReadAllText("worldcraftdata/" + Server.Clients[clientId].user.World.ToUpper() + ".json"));

                    if (craftdatamap.Data == null)
                    {
                        craftdatamap = new WorldCraftData();
                        craftdatamap.Data = new CraftData[4703];

                    }

                    craftdatamap.Data[order] = data;


                    string dataofacc = JsonSerializer.Serialize(craftdatamap);

                    File.WriteAllText("worldcraftdata/" + Server.Clients[clientId].user.World.ToUpper() + ".json", dataofacc);
                    Logic.GetInventoryAndSend(clientId, Server.Clients[clientId].user.username.ToUpper());

                    //save data
                    return;
















                }


            }


        }

        public static bool CheckIfCrafting(int cliemtId, int order)
        {
            //user data
            WorldCraftData acc = JsonSerializer.Deserialize<WorldCraftData>(File.ReadAllText("worldcraftdata/" + Server.Clients[cliemtId].user.World.ToUpper() + ".json"));
            if (acc.Data == null)
            {
                acc = new WorldCraftData();
                acc.Data = new CraftData[4703];
            }
            if (acc.Data[order] == null)
            {
                acc.Data[order] = new CraftData();
                File.WriteAllText("worldcraftdata/" + Server.Clients[cliemtId].user.World.ToUpper() + ".json", JsonSerializer.Serialize(acc));
            }

            if (acc.Data[order].resultItem == 0)
            {

                return false;
            }
            else
            {
                return true;
            }
        }
        public static void TryToGetCraftedItem(int clientid, int order)
        {
            //user

            WorldCraftData acc = JsonSerializer.Deserialize<WorldCraftData>(File.ReadAllText("worldcraftdata/" + Server.Clients[clientid].user.World.ToUpper() + ".json"));

            if (acc.Data == null)
            {
                acc = new WorldCraftData();
                acc.Data = new CraftData[4703];
            }
            if (acc.Data[order].craftDate < DateTime.Now)
            {
                // get item
                if (!Logic.CanTakeItem(Server.Clients[clientid].user.username.ToUpper(), acc.Data[order].resultItem, recipe[acc.Data[order].recipeid].constantResultCount))
                {
                    //inventory full
                    ServerSend.SendChat(clientid, "<color=red>Inventory Full!</color>");

                    return;
                }
                Logic.AddItemToInventory(Server.Clients[clientid].user.username.ToUpper(), acc.Data[order].resultItem, recipe[acc.Data[order].recipeid].constantResultCount);

                Logic.GetInventoryAndSend(clientid, Server.Clients[clientid].user.username.ToUpper());
                ServerSend.SendChat(clientid, $"<color=yellow>Crafted Item! You got {recipe[acc.Data[order].recipeid].constantResultCount} {itemdata.items[acc.Data[order].resultItem].itemname} </color>");
                //refresh acc
                acc.Data[order] = new CraftData() { resultItem = 0, recipeid = 0 };

                string dataofacc = JsonSerializer.Serialize(acc);

                File.WriteAllText("worldcraftdata/" + Server.Clients[clientid].user.World.ToUpper() + ".json", dataofacc);

                //get item



            }
            else
            {
                //Dont get item

                ServerSend.SendChat(clientid, "<color=red>Still crafting item! The crafting will complete at " + (acc.Data[order].craftDate - DateTime.Now).ToString() + "</color>");

            }

        }

    }

    class CraftRecipe
    {
        public int item1id;
        public int item1idMinCount;
        public int item2id;
        public int item2idMinCount;

        public int ItemResult;

        public int constantResultCount;
        public int[] RandomResultCountRange;

        public int CraftingTime;
        public int recipeid;
    }

    class CraftData
    {
        public int resultItem { get; set; }
        public DateTime craftDate { get; set; }
        public int recipeid { get; set; }


    }
    class WorldCraftData

    {
        public CraftData[] Data { get; set; }
    }


}
