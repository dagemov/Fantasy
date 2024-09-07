using Data;
using Fantasy.Backend.Errors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Utilyties;

namespace Fantasy.Backend.Extensions;

public static class ServiceAppExtension
{
    public static IServiceCollection AddServiceApp(this IServiceCollection services, IConfiguration config)
    {
        services.AddEndpointsApiExplorer();

        var connectionString = config.GetConnectionString("LocalConnection");
        services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = actionContext =>
            {
                var erros = actionContext.ModelState
                .Where(e => e.Value!.Errors.Count > 0)
                .SelectMany(x => x.Value!.Errors)
                .Select(x => x.ErrorMessage).ToArray();

                var errosResponse = new ApiValidationErrorReponse
                {
                    Erros = erros,
                };

                return new BadRequestObjectResult(errosResponse);
            };
        });

        //service CORS in API , to external connections
        services.AddCors();

        services.AddAutoMapper(typeof(MappingProfile));

        return services;
    }
}