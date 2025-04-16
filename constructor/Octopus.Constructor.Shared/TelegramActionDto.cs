using Octopus.Kernel.Domain.Enums;

namespace Octopus.Constructor.Shared;

public record TelegramActionDto(
    Guid Id,
    int Order, 
    TelegramActionType ActionType,
    IReadOnlyCollection<TelegramTextMessageDto>? Messages,
    IReadOnlyCollection<TelegramDocumentDto>? Documents,
    IReadOnlyCollection<TelegramButtonDto>? Buttons);