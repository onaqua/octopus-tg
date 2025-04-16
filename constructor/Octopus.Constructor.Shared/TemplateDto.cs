namespace Octopus.Constructor.Shared;

public record TemplateDto(Guid Id, string Name, string Description, IReadOnlyCollection<TelegramCommandDto> Commands);

