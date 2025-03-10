using Bobcos_Server;
using GameServer;
using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bobcos_Server
{
    public class FishingSystem
    {
        public static FishingItem[] FishItems = new FishingItem[4];
        private static Random random = new Random();

        public static void Initialize()
        { 
            FishItems[0] = new FishingItem(1, 93, new FishItemsToGet[] { new FishItemsToGet(93, 1) }, new int[] { 83 });
            FishItems[1] = new FishingItem(2, 94, new FishItemsToGet[] { new FishItemsToGet(94, 1) }, new int[] { 83 });
            FishItems[2] = new FishingItem(3, 95, new FishItemsToGet[] { new FishItemsToGet(95, 1) }, new int[] { 83 });
            FishItems[3] = new FishingItem(4, 85, new FishItemsToGet[] { new FishItemsToGet(85, 1) }, new int[] { 83 }); //
        }

        public static void StartFishing(int fromClient, int selectedItemID)
        {
            User user = Server.Clients[fromClient].user;
            useraccount accountOfUser = JsonSerializer.Deserialize<useraccount>(File.ReadAllText($"accounts/{Server.Clients[fromClient].user.username.ToUpper()}.json"));

            TimeSpan cooldown = TimeSpan.FromSeconds(10); 
            if (DateTime.UtcNow - user.lastFishingTime < cooldown)
            {
                ServerSend.SendChat(fromClient, "You must wait before fishing again.");
                return;
            }

            if (user != null && user.CurrentSelecteditem < itemdata.items.Length &&
                itemdata.items[user.CurrentSelecteditem].itemtype == "LURE")
            {
                bool canFish = false;
                foreach (var fishItem in FishItems)
                {
                    if (Array.Exists(fishItem.ValidBaits, bait => bait == user.CurrentSelecteditem))
                    {
                        canFish = true;
                        break;
                    }
                }

                if (canFish)
                {
                    user.lastFishingTime = DateTime.UtcNow; 

                    int baseDelayMilliseconds = random.Next(10000, 60001);

                    // Apply clothing effects
                    if (accountOfUser.shirt == 89) // Fisher Coat
                    {
                        baseDelayMilliseconds = (int)(baseDelayMilliseconds * 0.75); 
                    }

                    if (accountOfUser.pants == 90) // Fisher Pants
                    {
                        baseDelayMilliseconds = (int)(baseDelayMilliseconds * 0.75); 
                    }

                    if (accountOfUser.shoes == 91) // Fisher Shoes
                    {
                        baseDelayMilliseconds = (int)(baseDelayMilliseconds * 0.75); 
                    }

                    ServerSend.SendChat(fromClient, "You have started fishing.");
                    Logic.AddItemToInventory(user.username.ToUpper(), user.CurrentSelecteditem, -1);

                    Task.Run(async () =>
                    {
                        await Task.Delay(baseDelayMilliseconds);
                        ServerSend.SendChat(fromClient, "Something bit the bait. Tap the water to catch it!");
                        Server.Clients[fromClient].user.iscatchedfish = true;
                    });
                }
                else
                {
                    ServerSend.SendChat(fromClient, "This bait cannot catch any fish here.");
                }
            }
        }

        public static void ReceiveReward(int fromClient)
        {
            if (Server.Clients.TryGetValue(fromClient, out var client) && client.user != null && client.user.iscatchedfish)
            {
                client.user.iscatchedfish = false;

                string accountFilePath = $"accounts/{(client.user.username ?? "").ToUpper()}.json";

                if (File.Exists(accountFilePath))
                {
                    var acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText(accountFilePath));
                    FishingItem fishingItem = DetermineFishingItem(client.user.CurrentSelecteditem);

                    foreach (FishItemsToGet itemToGet in fishingItem.items)
                    {
                        Logic.AddItemToInventory(client.user.username.ToUpper(), itemToGet.ItemId, itemToGet.ItemCountToGet);
                    }

                    acc.inventory = Logic.GetInventory(client.user.username.ToUpper());
                    string accountData = JsonSerializer.Serialize(acc);
                    File.WriteAllText(accountFilePath, accountData);

                    ServerSend.SendPlayerChatBubble(fromClient, "Fish has been caught.", 0);
                    Logic.GetInventoryAndSend(fromClient, client.user.username.ToUpper());
                }
            }
            else
            {
                ServerSend.SendChat(fromClient, "No fish caught. Reward cannot be given.");
            }
        }

        private static FishingItem DetermineFishingItem(int selectedItemID)
        {
            int randomFishIndex = GetRandomFishIndex(selectedItemID);

            return FishItems[randomFishIndex];
        }

        private static int GetRandomFishIndex(int selectedItemID)
        {
         
            var rod = itemdata.items[selectedItemID];
            if (rod.RandomPick != null)
            {
                int randomIndex = random.Next(rod.RandomPick.randoms.Length);
                return rod.RandomPick.randoms[randomIndex];
            }

            return random.Next(FishItems.Length);
        }
    }

    public class FishingItem
    {
        public int FishId;
        public int ItemID;
        public FishItemsToGet[] items;
        public int[] ValidBaits;

        public FishingItem(int fishId, int itemId, FishItemsToGet[] itemsToGet, int[] validBaits)
        {
            FishId = fishId;
            ItemID = itemId;
            items = itemsToGet;
            ValidBaits = validBaits;
        }
    }

    public class FishItemsToGet
    {
        public int ItemId;
        public int ItemCountToGet;

        public FishItemsToGet(int itemId, int itemCountToGet)
        {
            ItemId = itemId;
            ItemCountToGet = itemCountToGet;
        }
    }
}
