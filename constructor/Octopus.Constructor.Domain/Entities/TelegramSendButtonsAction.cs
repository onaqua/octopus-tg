using Ardalis.Result;

namespace Octopus.Constructor.Domain;

public class TelegramSendButtonsAction : TelegramCommandAction
{
    private HashSet<TelegramButton> _buttons = new();

    public IReadOnlyCollection<TelegramButton> Buttons => _buttons;

    public static Result<TelegramSendButtonsAction> Create(int order, IReadOnlyCollection<TelegramButton> buttons)
    {
        if (buttons is null || buttons.Count == 0)
        { 
            return Result.Invalid(new ValidationError("Buttons is required"));
        }

        return Result.Created(new TelegramSendButtonsAction { Order = order, _buttons = buttons.ToHashSet() });
    }
}