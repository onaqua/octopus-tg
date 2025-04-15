using Microsoft.Extensions.Options;
using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var postgresHost = builder.AddParameter("PostgresHost");
var postgresPort = builder.AddParameter("PostgresPort");
var postgresUser = builder.AddParameter("PostgresUser");
var postgresPassword = builder.AddParameter("PostgresPassword");
var constructorDatabase = builder.AddParameter("ConstructorDatabase");
var environment = builder.AddParameter("Environment", "Development");

var postgres = builder
    .AddPostgres("postgres",
        postgresUser,
        postgresPassword, 5555)
    .WithPgAdmin()
    .WithDataVolume("octopus-data");

var keycloak = builder.AddKeycloak("keycloak", 5580);

var projects = new List<IResourceBuilder<ProjectResource>>
{
    builder
        .AddProject<Octopus_Constructor_API>("octopus-constructor-api", options => {
            options.ExcludeLaunchProfile = true;
            options.ExcludeKestrelEndpoints = true;
        })
        .WaitFor(postgres)
        .WithHttpEndpoint(port: 900)
        .WithEnvironment("ASPNETCORE_ENVIRONMENT", environment)
        .WithEnvironment("POSTGRES__HOST", postgresHost)
        .WithEnvironment("POSTGRES__PORT", postgresPort)
        .WithEnvironment("POSTGRES__USER", postgresUser)
        .WithEnvironment("POSTGRES__Password", postgresPassword)
        .WithEnvironment("POSTGRES__DATABASE", constructorDatabase)
};

foreach (var project in projects)
{
    project.WithReference(keycloak);
    project.WithReference(postgres);
}

builder.AddProject<Projects.Octopus_Bot_API>("octopus-bot-api");

var application = builder.Build();

await application.RunAsync();
