using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.IoC
{
    public abstract class IoCServiceCollectionBuilder
    {
        public IoCServiceCollectionBuilder NextBuilder { get; set; }

        public IoCServiceCollectionBuilder SetNextBuilder(IoCServiceCollectionBuilder builder) => NextBuilder = builder;

        public IServiceCollection Setup(IServiceCollection services)
        {
            SetupServices(services);

            if (NextBuilder != null)
            {
                NextBuilder.Setup(services);
            }

            return services;
        }

        protected abstract void SetupServices(IServiceCollection services);
    }
}