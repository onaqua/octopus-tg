using Ardalis.Result;
using Octopus.Kernel.Domain.Entities;

namespace Octopus.Constructor.Domain;

public class TelegramCommand : EntityBase<Guid>
{
    private readonly HashSet<TelegramAction> _telegramAction = new();

    public string Trigger { get; private set; } = null!;
    public string? Description { get; private set; } = null;

    public virtual IReadOnlyCollection<TelegramAction> TelegramAction => _telegramAction;

    public Result RemoveAction(TelegramAction action)
    {
        if (_telegramAction is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(TelegramAction)}) before remove action");
        }

        _telegramAction.Remove(action);

        return Result.NoContent();
    }

    public Result<TelegramAction> AddAction(TelegramAction action)
    {
        if (_telegramAction is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(TelegramAction)}) before add action");
        }

        _telegramAction.Add(action);

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
