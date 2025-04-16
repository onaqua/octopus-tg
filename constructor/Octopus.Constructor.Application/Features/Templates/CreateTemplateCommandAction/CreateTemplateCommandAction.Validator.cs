using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplateCommandAction;

public static partial class CreateTemplateCommandAction
{
    public sealed class Validator : AbstractValidator<Request>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Validator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            RuleFor(request => request.TemplateId)
                .MustAsync(async (id, cancellationToken) =>
                {
                    using var scope = _scopeFactory.CreateScope();
                    var repo = scope.ServiceProvider.GetRequiredService<ITemplateRepository>();
                    return await repo.IsExistsAsync(id, cancellationToken);
                })
                .WithMessage("Template not found")
                .WithSeverity(Severity.Error)
                .WithErrorCode("404");

            RuleFor(request => request.Order)
                .GreaterThanOrEqualTo(0)
                .WithErrorCode("409")
                .WithMessage("Order must be greater than or equal to 0");

            RuleFor(request => request.ActionType)
                .Must((request, actionType) =>
                {
                    if (actionType is TelegramActionType.SendTextMessages && request.TextMessages is not null && request.TextMessages.Count > 0)
                        return true;

                    if (actionType is TelegramActionType.SendButtons && request.Buttons is not null && request.Buttons.Count > 0)
                        return true;

                    if (actionType is TelegramActionType.SendDocuments && request.Documents is not null && request.Documents.Count > 0)
                        return true;

                    return false;
                })
                .WithMessage("Not valid action type")
                .WithErrorCode("409");
        }
    }
}