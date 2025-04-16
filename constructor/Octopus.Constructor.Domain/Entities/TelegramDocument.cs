using Ardalis.Result;
using System.Text.Json.Serialization;

namespace Octopus.Constructor.Domain;

public class TelegramDocument : TelegramCommandAction
{
    private TelegramDocument()
    {
        
    }

    [JsonIgnore]
    public TelegramAction Action { get; private set; } = null!;
    public Guid ActionId { get; private set; }

    public byte[] Payload { get; private set; }

    public static Result<TelegramDocument> Create(byte[] payload)
    {
        if (payload is null || payload.Length == 0)
        {
            return Result.Invalid(new ValidationError("Payload is required"));
        }

        return Result.Created(new TelegramDocument { Payload = payload });
    }
}
