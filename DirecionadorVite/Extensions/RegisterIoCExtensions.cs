using Application.Interfaces;
using Application.Services;
using Domain.Interfaces;
using Infra.Repositories;

namespace DirecionadorVite.Extensions
{
    public static class RegisterIoCExtensions
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IReadKmlRepository, ReadKmlRepository>();
            services.AddScoped<IPlacemarkRepository, PlacemarkRepository>();


            services.AddScoped<IPlacemarkAppService, PlacemarkAppService>();

        }
    }
}
