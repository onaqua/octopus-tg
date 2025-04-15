using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Octopus.Constructor.Domain;

namespace Octopus.Constructor.Infrastructure.Configurations;

internal sealed class TemplateConfiguration : IEntityTypeConfiguration<Template>
{
    public void Configure(EntityTypeBuilder<Template> builder)
    {
        builder
            .Navigation(x => x.Commands)
            .AutoInclude();
    }
}