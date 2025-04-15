using Microsoft.EntityFrameworkCore;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Infrastructure.Configurations;

namespace Octopus.Constructor.Infrastructure;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<Template> Templates { get; set; }

    public DbSet<TelegramButton> TelegramButtons { get; set; }
    public DbSet<TelegramCommand> TelegramCommands { get; set; }

    public DbSet<TelegramSendButtonsAction> TelegramSendButtonActions { get; set; }
    public DbSet<TelegramSendMessageAction> TelegramSendMessageActions { get; set; }
    public DbSet<TelegramSendRequestAction> TelegramSendRequestActions { get; set; }
    public DbSet<TelegramSendDocumentAction> TelegramSendDocumentActions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
    }
}