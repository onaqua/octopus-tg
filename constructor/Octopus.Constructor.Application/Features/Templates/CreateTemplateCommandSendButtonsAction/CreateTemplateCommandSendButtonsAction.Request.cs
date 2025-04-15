using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Shared;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplateCommandAction;

public static partial class CreateTemplateCommandSendButtonsAction
{
    public sealed record Request([FromRoute] Guid TemplateId, [FromRoute] Guid CommandId, int Order, IReadOnlyCollection<TelegramButtonDto> Buttons) : IRequest<Result<TelegramSendButtonsAction>>;
}