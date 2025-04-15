using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Application.Features.Templates;

public partial class CreateTemplateCommand
{
    public record Request([FromRoute] Guid TemplateId, string Trigger, string? Description) : IRequest<Result<TelegramCommand>>;
}