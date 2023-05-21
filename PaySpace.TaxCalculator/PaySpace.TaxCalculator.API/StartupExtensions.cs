

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using PaySpace.TaxCalculator.API.Dto;
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
        public static void AddDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaxDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), sqlServerOptionsAction: sqlOptions => {
                sqlOptions.MigrationsAssembly(typeof(Program).GetTypeInfo().Assembly.GetName().Name);
            }));
        }
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            var serviceProvider = services.BuildServiceProvider();
            
            services.AddScoped<IEnumerable<ITaxProcessor>>(options => GetInstances<ITaxProcessor>(serviceProvider));
        }

        public static void UseException(this WebApplication app)
        {
            var logger = app.Services.GetService<ILogger<WebApplication>>();
            app.UseExceptionHandler(option => {
                option.Run(async context => {
                    context.Response.ContentType = "application/json";
                    var exception = context.Features.Get<IExceptionHandlerPathFeature>();
                    logger?.LogError(exception?.Error.ToString());
                    await context.Response.WriteAsync(JsonConvert.SerializeObject(new ErrorResponse
                    {
                        StatusCode = System.Net.HttpStatusCode.InternalServerError,
                        ErrorMessage = "An Error occurred while processing your request. Kindly try again later"
                    }));
                });
            });
        }

        public static void EnsureDatabaseSetup(this WebApplication application)
        {
            var context = application.Services.CreateScope().ServiceProvider.GetRequiredService<TaxDbContext>();
            
            context.Database.Migrate();

            TaxDataSeeder.Seed(context);
        }

        private static List<T> GetInstances<T>(IServiceProvider serviceProvider)
        {
            var instances = new List<T>();
            var foundInstances = Assembly.GetAssembly(typeof(T))?.GetTypes()
                                ?.Where(detector => detector.IsClass &&
                                !detector.IsAbstract && typeof(T).IsAssignableFrom(detector));

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
