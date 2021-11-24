using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace WebManagement.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static void AddSwaggerSupport(this IServiceCollection services)
        {
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Bank Management Api", Version = "v1" });
                /*opt.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    Type         = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    In           = ParameterLocation.Header,
                    Scheme       = "bearer"
                });*/
                opt.CustomSchemaIds(x => x.FullName);
            });
        }
        
        public static void UseSwaggerMiddleware(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(swag =>
            {
                swag.SwaggerEndpoint("./swagger/v1/swagger.json", "Banking.Api");
                swag.RoutePrefix = "";
            });
        }
    }
}