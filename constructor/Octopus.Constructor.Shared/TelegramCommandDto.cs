namespace Octopus.Constructor.Shared;

public record TelegramCommandDto(
    Guid Id, 
    string Trigger, 
    string? Description, 
    IReadOnlyCollection<TelegramActionDto> Actions);

