using System.ClientModel;
using AICalendar.DTOs;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using System.Threading;
using ModelContextProtocol.Client;
using OpenAI;

namespace AICalendar.Services
{
    public static class PromptProcessor
    {
        public static async Task<string> Process(string prompt)
        {

            var chatClientBuilder = new ChatClientBuilder(new OpenAIClient(new ApiKeyCredential(Environment.GetEnvironmentVariable("AIKey")), new OpenAIClientOptions()
            {
                Endpoint = new Uri(Environment.GetEnvironmentVariable("AIUrl"))
            }).GetChatClient(Environment.GetEnvironmentVariable("AIModel")).AsIChatClient())
                .UseFunctionInvocation();

            var client = chatClientBuilder.Build();

            var mcpClient = await McpClientFactory.CreateAsync(new SseClientTransport(
                new SseClientTransportOptions()
                {
                    Endpoint = new Uri("https://aicalendarbackend.onrender.com/"),
                    Name = "AICalendar.ApiService"
                }));
            var tools = await mcpClient.ListToolsAsync();
            List<ChatMessage> messages = [new(ChatRole.User, prompt)];

            var result = client.GetStreamingResponseAsync(messages, new() { Tools = [.. tools] });
            string response = string.Empty;
            await foreach (var update in result)
            {
                response += update;
            }

            return response;
        }
    }
}