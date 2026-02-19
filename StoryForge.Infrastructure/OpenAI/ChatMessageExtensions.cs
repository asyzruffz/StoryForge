using OpenAI.Chat;
using StoryForge.Core.AI.Chat;

namespace StoryForge.Infrastructure.OpenAI;

internal static class ChatMessageExtensions
{
    public static ChatMessage ToOpenAIMessage(this Message message)
    {
        return message.Role switch
        {
            MessageRole.System => ChatMessage.CreateSystemMessage(message.Content),
            MessageRole.User => ChatMessage.CreateUserMessage(message.Content),
            MessageRole.Assistant => ChatMessage.CreateAssistantMessage(message.Content),
            MessageRole.Tool => ChatMessage.CreateToolMessage(message.Id, message.Content),
            _ => ChatMessage.CreateSystemMessage(message.Content),
        };
    }
}
