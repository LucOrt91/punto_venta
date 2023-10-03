using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POS.Application.Interfaces;
using POS.Application.Services;
using System.Reflection;

namespace POS.Application.Extensions
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjectionApplication(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddSingleton(configuration);

            service.AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies().Where(p => !p.IsDynamic));
            });

            service.AddAutoMapper(Assembly.GetExecutingAssembly());

            service.AddScoped<ICategoryApplication, CategoryApplication>();

            return service;
        }
    }
}
