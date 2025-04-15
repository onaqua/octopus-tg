using Ardalis.Result.AspNetCore;
using FastEndpoints;
using Octopus.Constructor.Application;
using Octopus.Constructor.Infrastructure;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllers(options =>
    {
        options.AddResultConvention(map =>
        {
            map.AddDefaultMap();
        });
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true;
    });


builder.Services.AddOpenApi();

builder.AddServiceDefaults();
builder.ImplementApplication();
builder.ImplementInfrastructure();

var app = builder.Build();

app.UsePathBase("/api/constructor");

app.MapFastEndpoints();

if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(x => x.Servers = [new ScalarServer("http://localhost:900", "Development")]);

    await using var scope = app.Services
        .CreateAsyncScope();

    await using var database = scope.ServiceProvider
        .GetRequiredService<DatabaseContext>();

    await database.Database.EnsureCreatedAsync();
}

app.MapDefaultEndpoints();
app.MapOpenApi();
app.UseAuthorization();
app.MapControllers();

await app.RunAsync();
await app.WaitForShutdownAsync();