using Ardalis.Result;
using System.Text.Json.Serialization;

namespace Octopus.Constructor.Domain;

public class TelegramTextMessage : TelegramCommandAction
{
    private TelegramTextMessage()
    {
        
    }

    [JsonIgnore]
    public TelegramAction Action { get; private set; } = null!;
    public Guid ActionId { get; private set; }

    public string Text { get; private set; }

    public static Result<TelegramTextMessage> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            return Result.Invalid(new ValidationError("Text is required"));
        }

        return Result.Created(new TelegramTextMessage { Text = text });
    }
}