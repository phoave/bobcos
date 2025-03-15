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
        public static CraftRecipe[] recipe = new CraftRecipe[5];

        public static void Initiliaze()
        {
            recipe[0] = new CraftRecipe() { constantResultCount = 50, CraftingTime = 60, item1id = 3, item2id = 3, item1idMinCount = 25, item2idMinCount = 0, ItemResult = 12, recipeid = 0 };
            recipe[1] = new CraftRecipe() { constantResultCount = 1, CraftingTime = 240, item1id = 37, item2id = 12, item1idMinCount = 5, item2idMinCount = 2, ItemResult = 31, recipeid = 1 };
            recipe[2] = new CraftRecipe() { constantResultCount = 10, CraftingTime = 800, item1id = 65, item2id = 12, item1idMinCount = 10, item2idMinCount = 20, ItemResult = 47, recipeid = 2 };
            recipe[3] = new CraftRecipe() { constantResultCount = 20, CraftingTime = 600, item1id = 71, item2id = 12, item1idMinCount = 10, item2idMinCount = 20, ItemResult = 48, recipeid = 3 };
            recipe[4] = new CraftRecipe() { constantResultCount = 10, CraftingTime = 30, item1id = 73, item2id = 66, item1idMinCount = 10, item2idMinCount = 10, ItemResult = 65, recipeid = 4 };

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
