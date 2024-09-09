using Fantasy.Frontend.Repositories.Interfaces;
using Fantasy.Frontend.Repositories.Service;

namespace Fantasy.Frontend.Extensions;

public static class ServiceAppExtension
{
    public static IServiceCollection AddServiceApp(this IServiceCollection services, IConfiguration config)
    {
        // cuidadon con el / al final para rutear
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7231/") });

        services.AddScoped<IRepository, Repository>();

        return services;
    }
}