using Ardalis.Result;
using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;

namespace Octopus.Constructor.Application.Features.Templates;

public partial class CreateTemplateCommand
{
    internal class Handler(
        IUnitOfWork unitOfWork,
        ITemplateRepository templatesRepository) : IRequestHandler<Request, Result<TelegramCommand>>
    {
        public async Task<Result<TelegramCommand>> Handle(Request request, CancellationToken cancellationToken)
        {
            var template = await templatesRepository
                .GetByIdAsync(request.TemplateId);

            if (template is null)
            {
                return Result.NotFound("Template not found");
            }

            var createCommandResult = TelegramCommand
                .Create(request.Trigger, request.Description);

            if (!createCommandResult.IsSuccess)
            {
                return createCommandResult;
            }

            var addCommandResult = template
                .AddCommand(createCommandResult);

            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            return addCommandResult;
        }
    }
}