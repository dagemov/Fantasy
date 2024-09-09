using Fantasy.Frontend.Repositories.Interfaces;
using Fantasy.Frontend.Repositories.Service;
using Radzen;

namespace Fantasy.Frontend.Extensions;

public static class ServiceAppExtension
{
    public static IServiceCollection AddServiceApp(this IServiceCollection services, IConfiguration config)
    {
        // Registrando el servicio ThemeService y otros servicios de Radzen
        services.AddScoped<ThemeService>();
        services.AddScoped<DialogService>();
        services.AddScoped<NotificationService>();
        services.AddScoped<TooltipService>();
        services.AddScoped<ContextMenuService>();
        // cuidadon con el / al final para rutear
        services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7231/") });

        services.AddScoped<IRepository, Repository>();
        //Multi Lenguajes
        services.AddLocalization();

        return services;
    }
}