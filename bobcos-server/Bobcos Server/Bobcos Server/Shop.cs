using GameServer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bobcos_Server
{
    class Shop
    {
        public static ShopItem[] ShopItems = new ShopItem[19];

        public static void Initiliaze()
        {

            ShopItems[0] = new ShopItem() { ItemName = "Grey Top hat", Price = 700, description = "Cool Grey Top Hat.", ItemID = 10, ShopId = 0, items = new ItemsToGet[1] { new ItemsToGet() { itemId = 10, ItemCountToGet = 1 } } };
            ShopItems[1] = new ShopItem() { ItemName = "Wooden Pack", Price = 100, description = "This pack contains 25 wood block and 25 wood background", ItemID = 12, ShopId = 1, items = new ItemsToGet[] { new ItemsToGet() { itemId = 12, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 13, ItemCountToGet = 25 } } };
            ShopItems[2] = new ShopItem() { ItemName = "Colored Block Pack", Price = 200, description = "This pack contains 25 per each colored blocks.", ItemID = 14, ShopId = 2, items = new ItemsToGet[] { new ItemsToGet() { itemId = 14, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 15, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 16, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 17, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 18, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 19, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 20, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 21, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 22, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 23, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 24, ItemCountToGet = 25 } } };
            ShopItems[3] = new ShopItem() { ItemName = "Neon Pack", Price = 600, description = "This pack contains 10 Red,Blue,Green neon Block", ItemID = 25, ShopId = 3, items = new ItemsToGet[] { new ItemsToGet() { itemId = 25, ItemCountToGet = 10 }, new ItemsToGet() { itemId = 26, ItemCountToGet = 10 }, new ItemsToGet() { itemId = 27, ItemCountToGet = 10 } } };
            ShopItems[4] = new ShopItem() { ItemName = "World Lock", Price = 1250, description = "You can lock world with this thing.", ItemID = 28, ShopId = 4, items = new ItemsToGet[] { new ItemsToGet() { itemId = 28, ItemCountToGet = 1 } } };
            ShopItems[5] = new ShopItem() { ItemName = "Colored Backgrounds Pack", Price = 275, description = "This pack contains 25 per each colored Backgrounds.", ItemID = 52, ShopId = 5, items = new ItemsToGet[] { new ItemsToGet() { itemId = 52, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 53, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 54, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 55, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 56, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 57, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 58, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 59, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 60, ItemCountToGet = 25 } } };
            ShopItems[6] = new ShopItem() { ItemName = "Decoration Pack", Price = 100, description = "This pack contains 25 per each Decoration blocks.", ItemID = 47, ShopId = 6, items = new ItemsToGet[] { new ItemsToGet() { itemId = 47, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 48, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 49, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 50, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 51, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 61, ItemCountToGet = 25 }, new ItemsToGet() { itemId = 63, ItemCountToGet = 25 } } };
            ShopItems[7] = new ShopItem() { ItemName = "Hair Pack", Price = 250, description = "This pack contains 1 Brown Male hair,1 Brown Female Hair, 1 Black Messy hair.", ItemID = 34, ShopId = 7, items = new ItemsToGet[] { new ItemsToGet() { itemId = 34, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 36, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 64, ItemCountToGet = 1 } } };
            ShopItems[8] = new ShopItem() { ItemName = "Red Cloth Pack", Price = 250, description = "This pack contains 1 Red shirt,1 Red shirt, 1 Red shoes", ItemID = 6, ShopId = 8, items = new ItemsToGet[] { new ItemsToGet() { itemId = 6, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 7, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 8, ItemCountToGet = 1 } } };
            ShopItems[9] = new ShopItem() { ItemName = "Green Cloth Pack", Price = 250, description = "This pack contains 1 Green shirt,1 Green shirt, 1 Green shoes", ItemID = 38, ShopId = 9, items = new ItemsToGet[] { new ItemsToGet() { itemId = 38, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 41, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 44, ItemCountToGet = 1 } } };
            ShopItems[10] = new ShopItem() { ItemName = "Blue Cloth Pack", Price = 250, description = "This pack contains 1 Blue shirt,1 Blue shirt, 1 Blue shoes", ItemID = 39, ShopId = 10, items = new ItemsToGet[] { new ItemsToGet() { itemId = 39, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 42, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 45, ItemCountToGet = 1 } } };
            ShopItems[11] = new ShopItem() { ItemName = "Yellow Cloth Pack", Price = 250, description = "This pack contains 1 Yellow shirt,1 Yellow shirt, 1 Yellow shoes", ItemID = 40, ShopId = 11, items = new ItemsToGet[] { new ItemsToGet() { itemId = 40, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 43, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 46, ItemCountToGet = 1 } } };
            ShopItems[12] = new ShopItem() { ItemName = "Angel Wings", Price = 12000, description = "You get 1 Angel Wings", ItemID = 35, ShopId = 12, items = new ItemsToGet[] { new ItemsToGet() { itemId = 35, ItemCountToGet = 1 } } };
            ShopItems[13] = new ShopItem() { ItemName = "Crafting Machines", Price = 50, description = "You get 5 Crafting Machine from this pack", ItemID = 30, ShopId = 13, items = new ItemsToGet[] { new ItemsToGet() { itemId = 30, ItemCountToGet = 5 } } };
            ShopItems[14] = new ShopItem() { ItemName = "Blonde Hair Pack", Price = 300, description = "This pack contains 1 Blonde Male hair,1 Blonde Female Hair", ItemID = 80, ShopId = 14, items = new ItemsToGet[] { new ItemsToGet() { itemId = 79, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 80, ItemCountToGet = 1 } } };
            ShopItems[15] = new ShopItem() { ItemName = "Fishing pack", Price = 5000, description = "This pack contains 1 Wooden rod and 5 of every lure", ItemID = 93, ShopId = 15, items = new ItemsToGet[] { new ItemsToGet() { itemId = 83, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 96, ItemCountToGet = 5 }, new ItemsToGet() { itemId = 97, ItemCountToGet = 5 }, new ItemsToGet() { itemId = 98, ItemCountToGet = 5 } } }; ShopItems[15] = new ShopItem() { ItemName = "Fishing pack", Price = 750, description = "This pack contains 1 Wooden rod and 5 of every lure", ItemID = 93, ShopId = 15, items = new ItemsToGet[] { new ItemsToGet() { itemId = 83, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 96, ItemCountToGet = 5 }, new ItemsToGet() { itemId = 97, ItemCountToGet = 5 }, new ItemsToGet() { itemId = 98, ItemCountToGet = 5 } } };
            ShopItems[16] = new ShopItem() { ItemName = "Galaxy pack", Price = 300000, description = "This galaxy pack contains 1 galaxy wings and galaxy sword", ItemID = 108, ShopId = 16, items = new ItemsToGet[] { new ItemsToGet() { itemId = 108, ItemCountToGet = 1 }, new ItemsToGet() { itemId = 109, ItemCountToGet = 1 } } };
            ShopItems[17] = new ShopItem() { ItemName = "Nuke ", Price = 12000, description = "Can Clear Worlds", ItemID = 112, ShopId = 17, items = new ItemsToGet[] { new ItemsToGet() { itemId = 112, ItemCountToGet = 1 }} };
            ShopItems[18] = new ShopItem() { ItemName = "Displayblock ", Price = 7500, description = "Can Be used display items", ItemID = 110, ShopId = 18, items = new ItemsToGet[] { new ItemsToGet() { itemId = 110, ItemCountToGet = 1 } } };

        }

        public static void BuyItem(int ShopItemId,int fromClient)
        {
            //Check player inventory item count
            List<InventoryTile> inv = Logic.GetInventory(Server.Clients[fromClient].user.username.ToUpper());

                foreach(ItemsToGet i in ShopItems[ShopItemId].items)
            {
                foreach(InventoryTile c in inv)
                {
                    if(c.id == i.itemId)
                    {
                        if(c.count + i.ItemCountToGet > 799)
                        {
                            ServerSend.SendChat(fromClient, "<color=red>Not enough space</color>");
                            return;
                        }
                    }

                }


            }


                if(Logic.GetGems(Server.Clients[fromClient].user.username.ToUpper()) < ShopItems[ShopItemId].Price)
            {
                ServerSend.SendChat(fromClient, "<color=red>Not enough Cash</color>");

                return;
            }

            //check Player gems

            foreach (ItemsToGet i in ShopItems[ShopItemId].items)
            {
                Logic.AddItemToInventory(Server.Clients[fromClient].user.username.ToUpper(), i.itemId, i.ItemCountToGet);



            }
            // give item to Player
            Logic.AddGems(Server.Clients[fromClient].user.username.ToUpper(), -ShopItems[ShopItemId].Price);

            // Remove gems From It
            // send inventory
            Logic.GetInventoryAndSend(fromClient, Server.Clients[fromClient].user.username.ToUpper());
            ServerSend.SendCash(fromClient, Logic.GetGems(Server.Clients[fromClient].user.username.ToUpper()));
            ServerSend.SendChat(fromClient, "<color=yellow>Bought Item!</color>");

        }
        public static void SendShopData(int ToClient)
        {
            ServerSend.SendLoading(ToClient, 0, "Loaded world");

            ServerSend.SendShopData(ToClient, ShopItems);



        }

    }

    class ShopItem

    {
        public string ItemName;
        public int Price;
        public string description;
        public int ShopId;
        public int ItemID;
        public ItemsToGet[] items;


    }

    class ItemsToGet
    {
        public int itemId;
        public int ItemCountToGet;

    }
}
