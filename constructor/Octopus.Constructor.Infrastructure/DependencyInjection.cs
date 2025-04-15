using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Npgsql;
using Octopus.Constructor.Domain;
using Octopus.Constructor.Domain.Repositories;
using Octopus.Constructor.Infrastructure.Configurations;
using Octopus.Constructor.Infrastructure.Repositories;

namespace Octopus.Constructor.Infrastructure;

public static class DependencyInjection
{
    public static IHostApplicationBuilder ImplementInfrastructure(this IHostApplicationBuilder builder)
    {
        var postgresOptions = builder.Configuration
            .GetRequiredSection(PostgresConfiguration.SectionName)
            .Get<PostgresConfiguration>() ?? throw new InvalidOperationException(
                "Please provide a valid Postgres configuration");

        var postgresConnection = new NpgsqlConnectionStringBuilder()
        {
            Host = postgresOptions.Host,
            Password = postgresOptions.Password,
            Port = postgresOptions.Port,
            Username = postgresOptions.User,
            Database = postgresOptions.Database,
            Pooling = true,
        };

        builder.Services.AddDbContext<DatabaseContext>(
            options => options.UseNpgsql(postgresConnection.ConnectionString));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<ITemplateRepository, TemplatesRepository>();

        return builder;
    }
}
