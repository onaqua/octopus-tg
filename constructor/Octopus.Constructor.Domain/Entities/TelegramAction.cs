using Ardalis.Result;

namespace Octopus.Constructor.Domain;

public class TelegramAction : TelegramCommandAction
{
    private HashSet<TelegramButton>? _buttons = new();
    private HashSet<TelegramDocument>? _documents = new();
    private HashSet<TelegramTextMessage>? _textMessages = new();

    public IReadOnlyCollection<TelegramButton>? Buttons => _buttons;
    public IReadOnlyCollection<TelegramDocument>? Documents => _documents;
    public IReadOnlyCollection<TelegramTextMessage>? TextMessages => _textMessages;


    public TelegramActionType ActionType { get; private set; }

    public static Result<TelegramAction> CreateTelegramTextMessageAction(
        IReadOnlyCollection<TelegramTextMessage> textMessages)
    {
        if (textMessages is null || textMessages.Count == 0)
        {
            return Result.Invalid(new ValidationError("TextMessages is required"));
        }

        return Result.Created(new TelegramAction { ActionType = TelegramActionType.SendTextMessages, _textMessages = textMessages.ToHashSet() });
    }

    public static Result<TelegramAction> CreateTelegramDocumentAction(
        IReadOnlyCollection<TelegramDocument> documents)
    {
        if (documents is null || documents.Count == 0)
        {
            return Result.Invalid(new ValidationError("Documents is required"));
        }

        return Result.Created(new TelegramAction { ActionType = TelegramActionType.SendDocuments, _documents = documents.ToHashSet() });
    }

    public static Result<TelegramAction> CreateTelegramButtonsAction(
        IReadOnlyCollection<TelegramButton> buttons)
    {
        if (buttons is null || buttons.Count == 0)
        {
            return Result.Invalid(new ValidationError("Buttons is required"));
        }

        return Result.Created(new TelegramAction { ActionType = TelegramActionType.SendButtons, _buttons = buttons.ToHashSet() });
    }
}
