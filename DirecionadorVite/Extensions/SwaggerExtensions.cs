using Microsoft.OpenApi.Models;

namespace DirecionadorVite.Extensions
{
    public static class SwaggerExtensions
    {
        public static void SwaggerConfiguration(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AvaliacaoVite.API",
                    Version = "v1",
                });               
            });
        }
    }
}
