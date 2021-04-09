using CalculatorService.Server.Interfaces;
using CalculatorService.Server.Services;
using Infrastructure.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace CalculatorService.Server.IoC
{
    public class ServiceCollectionBuilder : IoCServiceCollectionBuilder
    {
        protected override void SetupServices(IServiceCollection services)
        {
            services.AddTransient<ICalculator, Calculator>();
            services.AddTransient<IJournal, Journal>();
            services.AddSingleton<ILoggerManager, LoggerManager>();
        }
    }
}