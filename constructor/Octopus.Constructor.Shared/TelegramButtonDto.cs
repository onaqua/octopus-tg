namespace Octopus.Constructor.Shared;

public record TelegramButtonDto(string Text, string Trigger);

public record TelegramDocumentDto(byte[] Payload);

public record TelegramTextMessageDto(string Text);