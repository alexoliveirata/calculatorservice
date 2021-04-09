using CalculatorService.Server.ExceptionHandler;
using Infrastructure.IoC;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog;
using System.IO;
using DataRepositoryIoC = Data.Repository.IoC;
using ServerIoC = CalculatorService.Server.IoC;

namespace CalculatorService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseDeveloperExceptionPage();
            //}

            // Hook in the global error-handling middleware
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Calculator service");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers(x => x.AllowEmptyInputInBodyModelBinding = true);

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            // Register the AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // configure and setup builder
            var builder = this.ConfigureBuilder();
            builder.Setup(services);
        }

        internal IoCServiceCollectionBuilder ConfigureBuilder()
        {
            // build IoC layers
            var serverIoC = new ServerIoC.ServiceCollectionBuilder();
            var dataRepositoryIoC = new DataRepositoryIoC.ServiceCollectionBuilder();

            // set builders
            dataRepositoryIoC.SetNextBuilder(serverIoC);

            return dataRepositoryIoC;
        }
    }
}