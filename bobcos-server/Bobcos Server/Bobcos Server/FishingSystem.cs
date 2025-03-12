using Bobcos_Server;
using GameServer;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading.Tasks;

namespace Bobcos_Server
{
    public class FishingSystem
    {
        public static FishingItem[] FishItems = new FishingItem[3];
        private static Random random = new Random(); // Single random instance

        static FishingSystem()
        {
            Initialize();
        }

        public static void Initialize()
        {
            FishItems[0] = new FishingItem(1, 93, new FishItemsToGet[] { new FishItemsToGet(93, 1) });
            FishItems[1] = new FishingItem(2, 94, new FishItemsToGet[] { new FishItemsToGet(94, 1) });
            FishItems[2] = new FishingItem(3, 95, new FishItemsToGet[] { new FishItemsToGet(95, 1) });
        }

        public static void StartFishing(int fromClient, int selectedItemID)
        {
            User user = Server.Clients[fromClient].user;

            // Ensure that the user and item are valid for fishing
            if (user != null && user.CurrentSelecteditem < itemdata.items.Length &&
                itemdata.items[user.CurrentSelecteditem].itemtype == "LURE")
            {
                ServerSend.SendChat(fromClient, "You have started fishing.");
                Logic.AddItemToInventory(user.username.ToUpper(), user.CurrentSelecteditem, -1);

                int randomDelayMilliseconds = random.Next(10000, 60001); // Random fishing delay

                Task.Run(async () =>
                {
                    await Task.Delay(randomDelayMilliseconds);
                    ServerSend.SendChat(fromClient, "Something Bit the Bait. Tap The Water To Catch it");
                    Server.Clients[fromClient].user.iscatchedfish = true;
                });
            }
            else
            {
                ServerSend.SendChat(fromClient, "Invalid fishing attempt.");
            }
        }

        public static void ReceiveReward(int fromClient)
        {
            if (Server.Clients.TryGetValue(fromClient, out var client) && client.user != null && client.user.iscatchedfish)
            {
                client.user.iscatchedfish = false; // Reset the caught fish flag

                string accountFilePath = "accounts/" + (client.user.username ?? "").ToUpper() + ".json";

                if (File.Exists(accountFilePath))
                {
                    try
                    {
                        var acc = JsonSerializer.Deserialize<useraccount>(File.ReadAllText(accountFilePath));
                        int randomFishIndex = random.Next(FishItems.Length); // Randomly select a fishing item
                        FishingItem fishingItem = FishItems[randomFishIndex];

                        foreach (FishItemsToGet itemToGet in fishingItem.items)
                        {
                            Logic.AddItemToInventory(client.user.username.ToUpper(), itemToGet.ItemId, itemToGet.ItemCountToGet);
                        }

                        acc.inventory = Logic.GetInventory(client.user.username.ToUpper());
                        string accountData = JsonSerializer.Serialize(acc);
                        File.WriteAllText(accountFilePath, accountData); // Save updated account info

                        ServerSend.SendChat(fromClient, "Fish has been caught.");
                        Logic.GetInventoryAndSend(fromClient, client.user.username.ToUpper());
                    }
                    catch (Exception ex)
                    {
                        // Log error if JSON or file operations fail
                        ServerSend.SendChat(fromClient, "Failed to process your reward. Please try again later.");
                        Console.WriteLine("Error processing fishing reward: " + ex.Message);
                    }
                }
                else
                {
                    ServerSend.SendChat(fromClient, "Account file not found.");
                }
            }
            else
            {
                ServerSend.SendChat(fromClient, "No fish caught. Reward cannot be given.");
            }
        }
    }

    public class FishingItem
    {
        public int FishId;
        public int ItemID;
        public FishItemsToGet[] items;

        public FishingItem(int fishId, int itemId, FishItemsToGet[] itemsToGet)
        {
            FishId = fishId;
            ItemID = itemId;
            items = itemsToGet;
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
