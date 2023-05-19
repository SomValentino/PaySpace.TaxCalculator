

using Microsoft.EntityFrameworkCore;
using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Features.Processors;
using PaySpace.TaxCalculator.Domain.Enums;
using PaySpace.TaxCalculator.Infrastructure.Data;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace PaySpace.TaxCalculator.API
{
    public static class StartupExtensions
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            services.AddDbContext<TaxDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IEnumerable<ITaxProcessor>>(options => GetInstances<ITaxProcessor>(serviceProvider));
        }

        public static void EnsureDatabaseSetup(this WebApplication application)
        {
            var context = application.Services.CreateScope().ServiceProvider.GetRequiredService<TaxDbContext>();
            if(context.Database.IsRelational())
                context.Database.Migrate();

            TaxDataSeeder.Seed(context);
        }

        private static List<T> GetInstances<T>(IServiceProvider serviceProvider)
        {
            var instances = new List<T>();
            var foundInstances = Assembly.GetAssembly(typeof(T))?.GetTypes()
                                ?.Where(detector => detector.IsAssignableFrom(typeof(T)));

            if (foundInstances != null && foundInstances.Any())
            {
                foreach (var type in foundInstances)
                {
                    var typeDetector = (T?)serviceProvider.GetService(type);
                    if (typeDetector != null)
                        instances.Add(typeDetector);
                }
            }
            return instances;
        }
    }
}
