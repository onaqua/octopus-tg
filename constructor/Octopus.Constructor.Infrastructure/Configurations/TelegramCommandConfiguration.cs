using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Infrastructure.Configurations;

internal sealed class TelegramCommandConfiguration : IEntityTypeConfiguration<TelegramCommand>
{
    public void Configure(EntityTypeBuilder<TelegramCommand> builder)
    {
        builder
            .HasMany(x => x.TelegramAction)
            .WithOne(x => x.Command)
            .HasForeignKey(x => x.TelegramCommandId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(x => x.TelegramAction)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}