using FluentValidation;

namespace Octopus.Constructor.Application.Features.Templates.CreateTemplate;

public static partial class CreateTemplate
{
    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name must not be null or empty.");
        }
    }
}
