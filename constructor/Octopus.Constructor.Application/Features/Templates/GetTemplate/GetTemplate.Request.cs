using Ardalis.Result;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Application.Features.Templates.GetTemplate;

public static partial class GetTemplate
{
    public record Request([FromRoute] Guid Id) : IRequest<Result<Template>>;
}
