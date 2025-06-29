using AICalendar.DTOs;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;
using System.Threading;

namespace AICalendar.Services
{
    public class PromptProcessor(IChatClient client, ChatOptions options)
    {
        public IAsyncEnumerable<ChatResponseUpdate> Process(string prompt)
        {
            List<ChatMessage> messages = [new(ChatRole.User, prompt)];

            return client.GetStreamingResponseAsync(messages, options);
        }
    }
}