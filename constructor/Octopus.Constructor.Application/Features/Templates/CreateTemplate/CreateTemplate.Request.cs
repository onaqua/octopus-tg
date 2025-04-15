using Ardalis.Result;
using MediatR;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplate;

public static partial class CreateTemplate
{
    public record Request(string Name, bool IsPublic, string? Description) : IRequest<Result<Template>>;
}
