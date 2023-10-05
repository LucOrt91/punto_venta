using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace POS.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services) 
        {
            var openApi = new OpenApiInfo
            {
                Title = "POS API",
                Version = "v1",
                Description = "Punto de venta Api 2023",
                TermsOfService = new Uri("https://opensource.org/license/ecl-1-0/"),
                Contact = new OpenApiContact
                {
                    Name = "Lucas Ortega",
                    Email = "lucasnortega91@gmail.com",
                    Url = new Uri("https://pos-punto-venta.com"),
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri ("https://pos-punto-venta.com")
                },

            };
            services.AddSwaggerGen(x =>
            {
                openApi.Version = "V1";
                x.SwaggerDoc("v1", openApi);

                var securitySchema = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "JWT Bearer Token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "Jwt",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                x.AddSecurityDefinition(securitySchema.Reference.Id, securitySchema);
                x.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {securitySchema, new string[]{ } }
                });
            });

            return services;
        }
    }
}
