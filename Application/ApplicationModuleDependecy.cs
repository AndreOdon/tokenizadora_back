using Application.Services;
using Domain.Interfaces.Core.Application;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Application
{
    [ExcludeFromCodeCoverage]
    public static class ApplicationModuleDependecy
    {
        public static void AddApplicationModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPlanService, PlanService>();
        }
    }
}