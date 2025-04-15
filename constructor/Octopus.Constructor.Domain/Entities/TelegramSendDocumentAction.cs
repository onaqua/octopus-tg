using Ardalis.Result;

namespace Octopus.Constructor.Domain;

public class TelegramSendDocumentAction : TelegramCommandAction
{
    public byte[] Payload { get; private set; } = [];

    public static Result<TelegramSendDocumentAction> Create(int order, byte[] payload)
    {
        if (payload is null || payload.Length == 0)
        {
            return Result.Invalid(new ValidationError("Payload is required"));
        }

        return Result.Created(new TelegramSendDocumentAction { Order = order, Payload = payload });
    }
}