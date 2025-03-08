using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ReadLogsAndSolve.Helpers
{
    public class SendToAIModelAPI
    {
        public static async Task<string> SendToAIModelAPIAsync(string[] logLines)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.openai.com/v1/chat/completions");
                string apiToken= ConfigurationManager.AppSettings["APIKey"];

                request.Headers.Add("Authorization", $"Bearer {apiToken}");
                request.Headers.Add("Cookie", "__cf_bm=HN.63qYH8LBVmrlZ6Pnzb2I8ITKN2y6wk0Qjl85PE.o-1741425542-1.0.1.1-qwn9y_OwrQFSpTEnTazQIzIclIwuq82wUVkpyf9jr1QU.nGd35PAiI0sRF5qG1I1NTaRJewHFHW59NAvjssYaKNs6EGxLLvW3DsSo8xibL4; _cfuvid=APde.uMpvvLnyw98f_gF9gvc52rAj2hpZXlAx0ZOp9w-1741418191368-0.0.1.1-604800000");
                // Construct the JSON content dynamically
                var messages = new JArray
            {
                new JObject
                {
                    ["role"] = "user",
                    ["content"] = "Below are the errors from the log file, please have a look and give a detailed resolution to those:\n\n" +
                                  string.Join("\n", logLines)
                }
            };

                var jsonContent = new JObject
                {
                    ["model"] = "gpt-4o-mini",
                    ["store"] = true,
                    ["messages"] = messages
                };

                var content = new StringContent(jsonContent.ToString(), Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response to extract the 'content' field
                var jsonResponse = JObject.Parse(responseBody);
                var contentField = jsonResponse["choices"]?[0]?["message"]?["content"]?.ToString();

                if (string.IsNullOrEmpty(contentField))
                {
                    throw new Exception("The 'content' field is missing or empty in the API response.");
                }

                return contentField;
            }
            catch (Exception ex)
            {
                // Handle the exception here
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw; // Rethrow the exception
            }
        }
    }
}