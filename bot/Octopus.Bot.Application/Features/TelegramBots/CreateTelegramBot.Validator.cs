using FluentValidation;

namespace Octopus.Bot.Application.Features.TelegramBots;

public static partial class CreateTelegramBot
{
    public sealed class Validator : AbstractValidator<Request>
    {
        public Validator()
        {
            RuleFor(request => request.Token)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("Token is required");

            RuleFor(request => request.Name) 
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("Name is required");

            RuleFor(request => request.TemplateId)
                .NotEmpty()
                .WithErrorCode("400")
                .WithMessage("TemplateId is required");
        }
    }
}