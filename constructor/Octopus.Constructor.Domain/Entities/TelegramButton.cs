using Ardalis.Result;
using System.Text.Json.Serialization;

namespace Octopus.Constructor.Domain;

public class TelegramButton : EntityBase<Guid>
{
    public string Text { get; private set; } = null!;
    public string Trigger { get; private set; } = null!;

    [JsonIgnore]
    public TelegramSendButtonsAction Action { get; private set; } = null!;

    public Guid ActionId { get; private set; }

    public static Result<TelegramButton> Create(string text, string trigger)
    {
        if (string.IsNullOrWhiteSpace(text) || string.IsNullOrWhiteSpace(trigger))
        {
            return Result.Invalid(new ValidationError("Text and trigger is required"));
        }

        return Result.Created(new TelegramButton { Text = text, Trigger = trigger });
    }
}