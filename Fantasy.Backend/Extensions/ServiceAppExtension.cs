using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Data;
using Data.Helpers.Interfaces;
using Data.Helpers.Services;
using Data.Interfaces;
using Data.Repository;
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

        services.AddScoped<SeedDb>();

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
        //I'm only comment the inyeccion cuz the country working with the genericService and unitWork;but if we need
        //at more logic, we have yo inyect it.
        services.AddScoped<ICountryService, CountryService>();

        services.AddScoped<IUnitWork, UnitWork>();
        //Our unit Work implement the RepositoryGeneric and GenericService :)
        //services.AddScoped<(typeof (IRepositoryGeneric<>),typeof( RepositoryGeneric<>));

        services.AddScoped<IFileStorage, FileStorage>();
        return services;
    }
}