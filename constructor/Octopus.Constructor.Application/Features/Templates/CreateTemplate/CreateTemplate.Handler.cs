using Ardalis.Result;
using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;
using Octopus.Kernel.Domain;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplate;

public static partial class CreateTemplate
{
    internal sealed class Handler(
        IUnitOfWork unitOfWork,
        ITemplateRepository templatesRepository) : IRequestHandler<Request, Result<Template>>
    {
        public async Task<Result<Template>> Handle(Request request, CancellationToken cancellationToken)
        {
            var template = Template
                .Create(request.Name, request.IsPublic, request.Description);

            if (!template.IsSuccess)
            {
                return template;
            }
            
            await templatesRepository
                .AddAsync(template, cancellationToken);

            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            return template;
        }
    }
}
