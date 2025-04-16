using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Shared;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplateCommandAction;

public static partial class CreateTemplateCommandAction
{
    public sealed record Request(
        [FromRoute] Guid TemplateId, 
        [FromRoute] Guid CommandId, 
        int Order, 
        TelegramActionType ActionType,
        IReadOnlyCollection<TelegramTextMessageDto>? TextMessages = null,
        IReadOnlyCollection<TelegramDocumentDto>? Documents = null,
        IReadOnlyCollection<TelegramButtonDto>? Buttons = null) : IRequest<Result<TelegramAction>>;
}