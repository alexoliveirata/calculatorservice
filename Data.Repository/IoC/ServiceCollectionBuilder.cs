using Data.Repository.Implementations;
using Data.Repository.Interfaces;
using Data.Repository.Models;
using Infrastructure.IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Repository.IoC
{
    public class ServiceCollectionBuilder : IoCServiceCollectionBuilder
    {
        protected override void SetupServices(IServiceCollection services)
        {
            services.AddTransient<ICalculatorRepository, CalculatorRepository>();

            services.AddDbContext<CalculatorContext>(opt =>
                opt.UseInMemoryDatabase("CalculatorDatabase")
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
        }
    }
}