using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenAiService
{
    public class ChatService
    {
        public static async Task<string> promptGpt4Async(Message[] messages, string token, bool returnJson = false)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
            var request = new HttpRequestMessage();

            request.Method = HttpMethod.Post;
            request.RequestUri = new Uri("https://api.openai.com/v1/chat/completions");

            var json = JsonConvert.SerializeObject(new ChatRequest()
            {
                model = "gpt-4o",
                messages = messages,
                response_format = new ResponseFormat()
                { 
                    type = returnJson ? "json_object" : "text",
                }
            });

            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
            client.Timeout = TimeSpan.FromSeconds(120);
            var response = await client.SendAsync(request);
            var responseText = await response.Content.ReadAsStringAsync();
            
            ChatResponse? chatResponse = JsonConvert.DeserializeObject<ChatResponse>(responseText);

            string text = chatResponse.choices[0].message.content;
            return text;
        }

        public static Task<string> promptGpt4Async(string prompt, string token, bool returnJson = false)
        {
            var messages = new Message[]
            {
                    new Message()
                    {
                        role = "user",
                        content = prompt
                    }
            };

            return promptGpt4Async(messages, token, returnJson);

        }

    }
}
