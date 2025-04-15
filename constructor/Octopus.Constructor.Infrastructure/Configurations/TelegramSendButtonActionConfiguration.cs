using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Infrastructure.Configurations;

internal sealed class TelegramSendButtonActionConfiguration : IEntityTypeConfiguration<TelegramSendButtonsAction>
{
    public void Configure(EntityTypeBuilder<TelegramSendButtonsAction> builder)
    {
        builder
            .HasMany(x => x.Buttons)
            .WithOne(x => x.Action)
            .HasForeignKey(x => x.ActionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .Navigation(x => x.Buttons)
            .AutoInclude()
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
