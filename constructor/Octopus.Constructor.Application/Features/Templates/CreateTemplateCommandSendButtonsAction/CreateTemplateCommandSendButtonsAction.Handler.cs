using Ardalis.Result;
using MediatR;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplateCommandAction;

/// <summary>
/// Contains functionality for creating and adding button actions to Telegram commands within templates.
/// </summary>
public static partial class CreateTemplateCommandSendButtonsAction
{
    /// <summary>
    /// Handler for processing requests to create and add button actions to Telegram commands.
    /// </summary>
    public sealed class Handler(
        IUnitOfWork unitOfWork,
        ITemplateRepository templateRepository) : IRequestHandler<Request, Result<TelegramSendButtonsAction>>
    {
        /// <summary>
        /// Handles the request to create and add button actions to a specific command within a template.
        /// </summary>
        /// <param name="request">The request containing template ID, command ID, order, and button information.</param>
        /// <param name="cancellationToken">Cancellation token for async operations.</param>
        /// <returns>
        /// A Result containing the created TelegramSendButtonsAction if successful,
        /// or appropriate error information if the operation fails.
        /// </returns>
        public async Task<Result<TelegramSendButtonsAction>> Handle(Request request, CancellationToken cancellationToken)
        {
            // Create button objects from the request data and validate them
            var createButtonsResults = request.Buttons
                .Select(button => TelegramButton.Create(
                    text: button.Text,
                    trigger: button.Trigger))
                .ToList();

            // Return validation errors if any button creation failed
            if (createButtonsResults.Any(button => !button.IsSuccess))
            {
                return Result.Invalid(createButtonsResults.SelectMany(button => button.ValidationErrors));
            }

            // Retrieve the template by ID
            var template = await templateRepository
                .GetByIdAsync(request.TemplateId, cancellationToken);

            // Return not found error if template doesn't exist
            if (template is null)
            {
                return Result.NotFound("Template not found");
            }

            // Return not found error if command doesn't exist in the template
            if (template.Commands is null || template.Commands.All(command => command.Id != request.CommandId))
            {
                return Result.NotFound("Command not found");
            }

            // Get the specific command from the template
            var command = template.Commands.Single(command => command.Id == request.CommandId);

            // Create the send buttons action with the specified order and buttons
            var action = TelegramSendButtonsAction.Create(
                order: request.Order,
                buttons: createButtonsResults
                    .Select(button => button.Value)
                    .ToList());

            // Add the action to the command
            var result = command.AddSendButtonAction(action);

            // Save changes to the database
            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            // Return the result
            return result;
        }
    }
}