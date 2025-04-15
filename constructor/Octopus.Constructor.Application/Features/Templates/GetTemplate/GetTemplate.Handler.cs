using Ardalis.Result;
using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;

namespace Octopus.Constructor.Application.Features.Templates.GetTemplate;

public static partial class GetTemplate
{
    public class Handler(ITemplateRepository templateRepository) : IRequestHandler<Request, Result<Template>>
    {
        public async Task<Result<Template>> Handle(Request request, CancellationToken cancellationToken)
        {
            var template = await templateRepository
                .GetByIdAsync(request.Id, cancellationToken);

            if (template is null)
            {
                return Result.NotFound("Template not found");
            }

            return template;
        }
    }
}
