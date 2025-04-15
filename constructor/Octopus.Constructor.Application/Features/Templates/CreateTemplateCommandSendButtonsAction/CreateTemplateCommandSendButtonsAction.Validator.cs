using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Constructor.Domain.Repositories;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplateCommandAction;

public static partial class CreateTemplateCommandSendButtonsAction
{
    public sealed class Validator : AbstractValidator<Request>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Validator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            RuleFor(request => request.Buttons.Count)
                .GreaterThan(0)
                .WithErrorCode("409")
                .WithMessage("Buttons count must be greater than 0");

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
        }
    }
}