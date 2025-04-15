using Microsoft.Extensions.Options;

namespace Octopus.Constructor.Infrastructure.Configurations;

internal sealed class PostgresConfiguration 
{
    public static readonly string SectionName = "Postgres";

    public int Port { get; set; }
    public string Host { get; set; } = null!;
    public string User { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Database { get; set; } = null!;
}