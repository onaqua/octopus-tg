using Octopus.Bot.Domain.Entities;
using Octopus.Kernel.Domain.Repositories;

namespace Octopus.Bot.Domain.Repositories;

public interface ITelegramBotRepository : ITelegramBotReadRepository, ITelegramBotWriteRepository;

public interface ITelegramBotReadRepository : IReadRepository<Guid, TelegramBot>;

public interface ITelegramBotWriteRepository : IWriteRepository<TelegramBot>;