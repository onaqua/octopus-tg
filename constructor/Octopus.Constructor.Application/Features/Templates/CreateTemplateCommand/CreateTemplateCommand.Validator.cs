using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Constructor.Domain.Repositories;

namespace Octopus.Constructor.Application.Features.Templates;

public partial class CreateTemplateCommand
{
    internal class Validator : AbstractValidator<Request>
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public Validator(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;

            RuleFor(request => request.Trigger)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Trigger)} must not be null or empty.");

            RuleFor(request => request.Trigger)
                .Must(request => request.StartsWith('/'))
                .WithMessage(request => $"{nameof(request.Trigger)} must starts with '/' character");

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
        }
    }
}