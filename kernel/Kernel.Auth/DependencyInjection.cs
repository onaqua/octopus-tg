using Microsoft.Extensions.Hosting;

namespace Kernel.Auth;

public static class DependencyInjection
{
    public static IHostApplicationBuilder AddDefaultAuthentication(this IHostApplicationBuilder builder)
    {
        return builder;      
    }
}