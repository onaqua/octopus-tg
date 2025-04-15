using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Infrastructure.Configurations;

internal sealed class TelegramCommandConfiguration : IEntityTypeConfiguration<TelegramCommand>
{
    public void Configure(EntityTypeBuilder<TelegramCommand> builder)
    {
        builder
            .HasMany(x => x.SendButtonActions)
            .WithOne(x => x.Command)
            .HasForeignKey(x => x.TelegramCommandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(x => x.SendButtonActions)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.SendDocumentActions)
            .WithOne(x => x.Command)
            .HasForeignKey(x => x.TelegramCommandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(x => x.SendDocumentActions)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.SendMessageActions)
            .WithOne(x => x.Command)
            .HasForeignKey(x => x.TelegramCommandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(x => x.SendMessageActions)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasMany(x => x.SendRequestActions)
            .WithOne(x => x.Command)
            .HasForeignKey(x => x.TelegramCommandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(x => x.SendRequestActions)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}