using Ardalis.Result;
using MediatR;
using Octopus.Bot.Application.Clients;
using Octopus.Bot.Domain.Entities;
using Octopus.Bot.Domain.Repositories;
using Octopus.Bot.Domain.ValueObjects;
using Octopus.Kernel.Domain;

namespace Octopus.Bot.Application.Features.TelegramBots;

public static partial class CreateTelegramBot
{
    public sealed class Handler(
        IUnitOfWork unitOfWork,
        IConstructorApi constructorApi,
        ITelegramBotRepository telegramBotRepository) : IRequestHandler<Request, Result<TelegramBot>>
    {
        public async Task<Result<TelegramBot>> Handle(Request request, CancellationToken cancellationToken)
        {
            var templateResponse = await constructorApi
                .GetTemplateAsync(request.TemplateId, cancellationToken);

            if (!templateResponse.IsSuccessful)
            {
                return Result.NotFound("Template not found");
            }

            var telegramBotResult = TelegramBot.Create(
                name: request.Name,
                token: TelegramBotToken.Create(request.Token),
                ownerId: Guid.Empty,
                templateId: templateResponse.Content.Id,
                description: request.Description);

            if (!telegramBotResult.IsSuccess)
            {
                return telegramBotResult;
            }

            await telegramBotRepository
                .AddAsync(telegramBotResult, cancellationToken);

            await unitOfWork
                .SaveChangesAsync(cancellationToken);

            return telegramBotResult;
        }
    }
}