using Ardalis.Result;
using Octopus.Kernel.Domain.Entities;

namespace Octopus.Constructor.Domain;

public class Template : EntityBase<Guid>
{
    private HashSet<TelegramCommand>? _commands;

    private Template() { }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public bool IsPublic { get; private set; }

    public IReadOnlyCollection<TelegramCommand>? Commands => _commands;

    public Result<TelegramCommand> AddCommand(TelegramCommand command)
    {
        if (_commands is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(Commands)}) before add command");
        }

        if (_commands.Any(c => c.Trigger.Equals(command.Trigger)))
        {
            return Result.NotFound($"Command with trigger {command.Trigger} already exists");
        }

        _commands.Add(command);

        return Result.Created(command);
    }

    public void RemoveCommand(TelegramCommand command)
    {
        if (_commands is null)
        {
            throw new InvalidOperationException($"Need to _.Include(_ => _.{nameof(Commands)}) before remove command");
        }

        _commands.Remove(command);
    }

    public void SetPublic(bool isPublic)
    {
        IsPublic = isPublic;
    }

    public void SetName(string name)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(name);

        Name = name;
    }

    public void SetDescription(string? description)
    {
        Description = description;
    }

    public static Result<Template> Create(string name, bool isPublic, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            return Result.Invalid(new ValidationError("Name is required"));
        }

        return Result.Created(new Template { Name = name, IsPublic = isPublic, Description = description });
    }
}
