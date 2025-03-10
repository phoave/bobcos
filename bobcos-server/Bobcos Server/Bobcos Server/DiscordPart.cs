using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;


namespace Bobcos_Server
{
    public class DiscordPart
    {
        private static readonly HttpClient httpClient = new HttpClient();


        private const string webhookUrlSystemMessage = "https://discord.com/api/webhooks/1185834767200038993/j5G0F30la-y4-ReVvoxWSKjLX65Ui2zqBBC_iOvret7nsTNyhKJVGgWKtz8pojjhC_Zk";
        private const string webhookUrlPlayerChat = "https://discord.com/api/webhooks/1185834767200038993/j5G0F30la-y4-ReVvoxWSKjLX65Ui2zqBBC_iOvret7nsTNyhKJVGgWKtz8pojjhC_Zk";
        private const string webhookUrlGlobalMessageLog = "https://discord.com/api/webhooks/1185834767200038993/j5G0F30la-y4-ReVvoxWSKjLX65Ui2zqBBC_iOvret7nsTNyhKJVGgWKtz8pojjhC_Zk";
        private const string webhookUrlItemDrop = "https://discord.com/api/webhooks/1185834767200038993/j5G0F30la-y4-ReVvoxWSKjLX65Ui2zqBBC_iOvret7nsTNyhKJVGgWKtz8pojjhC_Zk";

        public static async Task SendSystemMessage(string message)
        {
            var payload = new
            {
                username = "System Message",
                content = message
            };

            await SendWebhookMessage(webhookUrlSystemMessage, payload);
        }

        public static async Task PlayerChat(string username, string message, string world)
        {
            var payload = new
            {
                username = "Player Chat",
                content = $"[{world}] {username}: {message}"
            };

            await SendWebhookMessage(webhookUrlPlayerChat, payload);
        }

        public static async Task GlobalMessageLog(string username, string message, string world)
        {
            var payload = new
            {
                username = "Global Message Log",
                content = $"[{world}] {username}: {message}"
            };

            await SendWebhookMessage(webhookUrlGlobalMessageLog, payload);
        }

        public static async Task SendItemDrop(string username, string world, string itemName, int itemCount)
        {
            var payload = new
            {
                username = "Item Drop",
                content = $"[{world}] {username} received {itemCount}x {itemName}!"
            };

            await SendWebhookMessage(webhookUrlItemDrop, payload);
        }

        private static async Task SendWebhookMessage(string webhookUrl, object payload)
        {
            try
            {
                var jsonPayload = JsonConvert.SerializeObject(payload);

                var response = await httpClient.PostAsync(webhookUrl, new StringContent(jsonPayload, Encoding.UTF8, "application/json"));

                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("Webhook message sent successfully.");
                }
                else
                {
                    Console.WriteLine($"Error sending webhook message: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending webhook message: {ex.Message}");
            }
        }

        public static async Task SendReport(string world, string username, string reportMessage)
        {
            var payload = new
            {
                username = "Report",
                content = $"[{world}] **{username}** **Reaason:** {reportMessage}"
            };

            string webhookUrlReport = "https://discord.com/api/webhooks/1185834767200038993/j5G0F30la-y4-ReVvoxWSKjLX65Ui2zqBBC_iOvret7nsTNyhKJVGgWKtz8pojjhC_Zk";

            await SendWebhookMessage(webhookUrlReport, payload);
        }

        public static async Task SendWorldReport(string username, string world, string reportMessage)
        {
            var payload = new
            {
                username = "World Report",
                content = $"[{world}] Player **{username} has reported** {world} **Reason:** {reportMessage}"
            };
            
            string webhookUrlWorldReport = "https://discord.com/api/webhooks/1185834767200038993/j5G0F30la-y4-ReVvoxWSKjLX65Ui2zqBBC_iOvret7nsTNyhKJVGgWKtz8pojjhC_Zk";

            await SendWebhookMessage(webhookUrlWorldReport, payload);
        }

        public static async Task SendItemTake(string username, string world, string itemName, int itemCount)
        {
            var payload = new
            {
                username = "Item Take",
                content = $"[{world}] {username} took {itemCount}x {itemName}!"
            };

            string webhookUrlItemTake = "https://discord.com/api/webhooks/1185834767200038993/j5G0F30la-y4-ReVvoxWSKjLX65Ui2zqBBC_iOvret7nsTNyhKJVGgWKtz8pojjhC_Zk";

            await SendWebhookMessage(webhookUrlItemTake, payload);
        }

        public static void SetUp()
        {
            Console.WriteLine("DiscordPart is set up and ready to handle Discord interactions.");
        }
    }
}
