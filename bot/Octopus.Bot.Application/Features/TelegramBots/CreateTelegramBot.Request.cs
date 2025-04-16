using Ardalis.Result;
using MediatR;
using Octopus.Bot.Domain.Entities;

namespace Octopus.Bot.Application.Features.TelegramBots;

public static partial class CreateTelegramBot
{
    public record Request(Guid TemplateId, string Name, string Token, string? Description = null) : IRequest<Result<TelegramBot>>;
}