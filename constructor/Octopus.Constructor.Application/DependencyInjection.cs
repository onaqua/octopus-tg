using FastEndpoints;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Octopus.Kernel.Application.Features.Templates;
using System.Reflection;

namespace Octopus.Constructor.Application;

public static class DependencyInjection
{
    public static readonly Assembly Assembly = typeof(DependencyInjection).Assembly;

    public static IHostApplicationBuilder ImplementApplication(this
        IHostApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
        {
            options.AddOpenBehavior(typeof(ValidationPipelineBehavior<,>));
            options.RegisterServicesFromAssembly(Assembly);
        });

        builder.Services.AddValidatorsFromAssemblyContaining(typeof(DependencyInjection), ServiceLifetime.Scoped);
        builder.Services.AddFastEndpoints(x => x.Assemblies = [Assembly]);

        return builder;
    }
}