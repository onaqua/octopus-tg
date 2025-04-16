namespace Octopus.Constructor.Shared;

public record TelegramSendRequestActionDto(Guid Id, int Order, string Uri, string Method, string? Payload, IReadOnlyDictionary<string, string> Headers);

