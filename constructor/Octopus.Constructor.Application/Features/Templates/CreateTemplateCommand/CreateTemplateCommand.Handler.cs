using Ardalis.Result;
using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;
using Octopus.Kernel.Domain;

namespace Octopus.Constructor.Application.Features.Templates;

/// <summary>
/// Command handler for creating a new Telegram command within an existing template.
/// This class handles the process of adding a new command to a template and persisting the changes.
/// </summary>
public partial class CreateTemplateCommand
{
    /// <summary>
    /// Handler implementation for processing CreateTemplateCommand requests.
    /// Retrieves the template, creates a new Telegram command, and adds it to the template.
    /// </summary>
    internal class Handler(
        IUnitOfWork unitOfWork,
        ITemplateRepository templatesRepository) : IRequestHandler<Request, Result<TelegramCommand>>
    {
        /// <summary>
        /// Handles the request to create a new Telegram command within a template.
        /// </summary>
        /// <param name="request">The request containing template ID, trigger, and description for the new command</param>
        /// <param name="cancellationToken">Cancellation token for async operations</param>
        /// <returns>Result containing the created TelegramCommand if successful, or appropriate error result</returns>
        public async Task<Result<TelegramCommand>> Handle(Request request, CancellationToken cancellationToken)
        {
            // Retrieve the template by ID from the repository
            var template = await templatesRepository
                .GetByIdAsync(request.TemplateId);

            // Return NotFound result if template doesn't exist
            if (template is null)
            {
                return Result.NotFound("Template not found");
            }

            // Create a new Telegram command with the provided trigger and description
            var createCommandResult = TelegramCommand
                .Create(request.Trigger, request.Description);

            // Return error result if command creation failed
            if (!createCommandResult.IsSuccess)
            {
                return createCommandResult;
            }

            // Add the newly created command to the template
            var addCommandResult = template
                .AddCommand(createCommandResult);

            // Persist changes to the database
            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            // Return the result of adding the command to the template
            return addCommandResult;
        }
    }
}