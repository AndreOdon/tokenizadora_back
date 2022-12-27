using Domain.Interfaces.Adapters.Infra.DataBase;
using Infra.DataBase.Repository;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Infra.DataBase
{
    [ExcludeFromCodeCoverage]
    public static class DataBaseModuleDependency
    {
        public static void AddDataBaseModule(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IRegionTaxRepository, RegionTaxRepository>();
            services.AddScoped<IPlanRepository, PlanRepository>();
        }
    }
}