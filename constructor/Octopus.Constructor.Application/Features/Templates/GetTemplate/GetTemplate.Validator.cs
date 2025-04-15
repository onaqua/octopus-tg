using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Octopus.Constructor.Domain.Repositories;

namespace Octopus.Constructor.Application.Features.Templates.GetTemplate;

public static partial class GetTemplate
{
    public class Validator : AbstractValidator<Request>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Validator(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;

            RuleFor(request => request.Id)
                .NotEmpty()
                .WithMessage(request => $"{nameof(request.Id)} must not be null or empty.");

            RuleFor(request => request.Id)
                .MustAsync(async (_, id, _, cancellationToken) =>
                {
                    await using var scope = _serviceScopeFactory
                        .CreateAsyncScope();

                    var templateRepository = scope.ServiceProvider
                        .GetRequiredService<ITemplateRepository>();

                    return await templateRepository.IsExistsAsync(id, cancellationToken);
                });
        }
    }
}
