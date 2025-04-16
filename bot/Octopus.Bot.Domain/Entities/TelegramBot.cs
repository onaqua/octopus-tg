using Ardalis.Result;
using Octopus.Bot.Domain.ValueObjects;
using Octopus.Kernel.Domain.Entities;

namespace Octopus.Bot.Domain.Entities;

public class TelegramBot : EntityBase<Guid>
{
    private TelegramBot() { }

    public TelegramBotToken Token { get; private set; } = null!;

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public bool IsEnabled { get; private set; }

    public Guid OwnerId { get; private set; }
    public Guid TemplateId { get; private set; }

    public Result SetEnabled(bool isEnabled)
    {
        IsEnabled = isEnabled;
        return Result.Success();
    }

    public Result SetTemplate(Guid templateId)
    {
        if (TemplateId == Guid.Empty)
        {
            return Result.Invalid(new ValidationError("Template is required."));
        }

        TemplateId = templateId;

        return Result.Success();
    }

    public Result SetDescription(string? description)
    {
        Description = description;
        return Result.Success();
    }

    public Result SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Invalid(new ValidationError("Name is required."));
        }

        Name = name;

        return Result.Success();
    }

    public Result SetToken(TelegramBotToken token)
    {
        if (token == null)
        {
            return Result.Invalid(new ValidationError("Token is required."));
        }

        Token = token;

        return Result.Success();
    }

    public static Result<TelegramBot> Create(string name, TelegramBotToken token, Guid ownerId, Guid templateId, string? description = null)
    {
        if (token == null)
        {
            return Result.Invalid(new ValidationError("Token is required."));
        }

        if (string.IsNullOrEmpty(name))
        {
            return Result.Invalid(new ValidationError("Name is required."));
        }

        return Result.Created(new TelegramBot
        {
            Token = token,
            OwnerId = ownerId,
            TemplateId = templateId,
            Description = description,
            Name = name,
            IsEnabled = true,
        });
    }
}