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


        private const string webhookUrlSystemMessage = "https://discord.com/api/webhooks/1355360197551325318/Nmy2zfGdOU40QnnAHYTUmbXhrgSE9xfG4ie0UfG7ZFBQ0dcz3hAZ_iH9Kxo_H3tHL_ae";
        private const string webhookUrlPlayerChat = "https://discord.com/api/webhooks/1355360311246454845/6qpZ7wrQ6qyYue3hCHdJ8qX1upzrLptPpEfA1v-GRmpvdb-qJAdNmi7NlF3K-tcuPxMq";
        private const string webhookUrlGlobalMessageLog = "https://discord.com/api/webhooks/1355360460391845918/fdvKz2h8nyqu6HuEI4ebT8qn8FxvwHe_aoQUuuTP7A7zlLow0Soed6lcLTPPABJK08PC";
        private const string webhookUrlItemDrop = "https://discord.com/api/webhooks/1355360663857533049/Pa-XJBvx5YYl-1ZviRK65nbHpCYIFiVSsBF14s8Kj6SxuuT82i7MUgmAFNvmGwcOCZW_";

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
                    //Console.WriteLine("Webhook message sent successfully.");
                }
                else
                {
                    //Console.WriteLine($"Error sending webhook message: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Error sending webhook message: {ex.Message}");
            }
        }

        public static async Task SendReport(string world, string username, string reportMessage)
        {
            var payload = new
            {
                username = "Report",
                content = $"[{world}] **{username}** **Reaason:** {reportMessage}"
            };

            string webhookUrlReport = "https://discord.com/api/webhooks/1355360754294849536/spFFoFrDKOeQ0FHlLFkgf6md55dmLeMMhpXderH4GEbJa7N9ckddQG_JG2bKmb5Fiy2F";

            await SendWebhookMessage(webhookUrlReport, payload);
        }

        public static async Task SendWorldReport(string username, string world, string reportMessage)
        {
            var payload = new
            {
                username = "World Report",
                content = $"[{world}] Player **{username} has reported** {world} **Reason:** {reportMessage}"
            };
            
            string webhookUrlWorldReport = "https://discord.com/api/webhooks/1355360822343504103/-P9XGgI8T-L7v1LrQyXDVpP_BJzqepXiUAcRfnsrFCiLLTHGuGQ0YO_akCZ_ztqLx1xV";

            await SendWebhookMessage(webhookUrlWorldReport, payload);
        }

        public static async Task SendItemTake(string username, string world, string itemName, int itemCount)
        {
            var payload = new
            {
                username = "Item Take",
                content = $"[{world}] {username} took {itemCount}x {itemName}!"
            };

            string webhookUrlItemTake = "https://discord.com/api/webhooks/1355360885954056262/-31uKSU2MQGtcxOFJAzp-4ZXQJxSifHR4LrVqrriJeYs3ek_77eIFq-_ykxl2u7aqqnx";

            await SendWebhookMessage(webhookUrlItemTake, payload);
        }

        public static void SetUp()
        {
            Console.WriteLine("DiscordPart is set up and ready to handle Discord interactions.");
        }
    }
}
