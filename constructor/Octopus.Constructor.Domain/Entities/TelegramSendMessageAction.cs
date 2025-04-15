namespace Octopus.Constructor.Domain;

public class TelegramSendMessageAction : TelegramCommandAction
{
    public string Value { get; private set; } = null!;
}
