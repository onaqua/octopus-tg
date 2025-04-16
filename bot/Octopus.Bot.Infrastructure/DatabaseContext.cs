using Microsoft.EntityFrameworkCore;
using Octopus.Bot.Domain.Entities;

namespace Octopus.Bot.Infrastructure;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<TelegramBot> TelegramBots { get; set; }
}