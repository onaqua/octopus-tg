using MediatR;
using Octopus.Bot.Domain.Entities;
using Octopus.Kernel.Application;

namespace Octopus.Bot.Application.Features.TelegramBots;

public static partial class CreateTelegramBot
{
    public class Endpoint : ResultEndpoint<Request, TelegramBot>
    {
        public override void Configure()
        {
            Post("/bot");
            Tags("Bots");
            Summary(s =>
            {
                s.Summary = "Create a bot";
                s.Description = "Create a bot";
            });
            Validator<Validator>();
            AllowAnonymous();       
        }
    }
}