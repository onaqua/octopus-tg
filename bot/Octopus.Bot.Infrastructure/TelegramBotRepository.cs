using Microsoft.EntityFrameworkCore;
using Octopus.Bot.Domain.Entities;
using Octopus.Bot.Domain.Repositories;
using Octopus.Kernel.Infrastructure;

namespace Octopus.Bot.Infrastructure;

public class TelegramBotRepository(DatabaseContext databaseContext) : RepositoryBase<TelegramBot>(databaseContext.TelegramBots), ITelegramBotRepository
{
    public async Task AddAsync(TelegramBot entity, CancellationToken cancellationToken = default) =>
        await Set.AddAsync(entity, cancellationToken);

    public Task<TelegramBot?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        Set.SingleOrDefaultAsync(x => x.Id.Equals(id), cancellationToken);
}