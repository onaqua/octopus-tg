using Ardalis.Result;
using Octopus.Kernel.Domain.Entities;

namespace Octopus.Constructor.Domain;

public class TelegramCommand : EntityBase<Guid>
{
    private readonly HashSet<TelegramSendButtonsAction> _sendButtonActions = new();
    private readonly HashSet<TelegramSendDocumentAction> _sendDocumentActions = new();
    private readonly HashSet<TelegramSendMessageAction> _sendMessageActions = new();
    private readonly HashSet<TelegramSendRequestAction> _sendRequestActions = new();

    public string Trigger { get; private set; } = null!;
    public string? Description { get; private set; } = null;

    public virtual IReadOnlyCollection<TelegramSendButtonsAction> SendButtonActions => _sendButtonActions;
    public virtual IReadOnlyCollection<TelegramSendDocumentAction> SendDocumentActions => _sendDocumentActions;
    public virtual IReadOnlyCollection<TelegramSendMessageAction> SendMessageActions => _sendMessageActions;
    public virtual IReadOnlyCollection<TelegramSendRequestAction> SendRequestActions => _sendRequestActions;

    public Result RemoveSendButtonAction(TelegramSendButtonsAction action)
    {
        if (_sendButtonActions is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(SendButtonActions)}) before remove action");
        }

        _sendButtonActions.Remove(action);

        return Result.NoContent();
    }

    public Result<TelegramSendButtonsAction> AddSendButtonAction(TelegramSendButtonsAction action)
    {
        if (_sendButtonActions is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(SendButtonActions)}) before add action");
        }

        _sendButtonActions.Add(action);

        return Result.Created(action);
    }

    public Result<TelegramSendDocumentAction> AddSendDocumentAction(TelegramSendDocumentAction action)
    {
        if (_sendDocumentActions is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(SendDocumentActions)}) before add action");
        }

        _sendDocumentActions.Add(action);

        return Result.Created(action);
    }

    public Result<TelegramSendRequestAction> AddSendRequestAction(TelegramSendRequestAction action)
    {
        if (_sendRequestActions is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(SendRequestActions)}) before add action");
        }

        _sendRequestActions.Add(action);

        return Result.Created(action);
    }

    public Result<TelegramSendMessageAction> AddSendRequestAction(TelegramSendMessageAction action)
    {
        if (_sendMessageActions is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(TelegramSendMessageAction)}) before add action");
        }

        _sendMessageActions.Add(action);

        return Result.Created(action);
    }

    public static Result<TelegramCommand> Create(string trigger, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(trigger))
        {
            return Result.Invalid(new ValidationError("Trigger is required"));
        }

        return Result.Created(new TelegramCommand { Trigger = trigger, Description = description });
    }
}
