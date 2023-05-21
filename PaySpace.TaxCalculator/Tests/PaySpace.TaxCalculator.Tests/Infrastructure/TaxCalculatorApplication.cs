using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PaySpace.TaxCalculator.API;
using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Infrastructure.Data;

namespace PaySpace.TaxCalculator.Tests.Infrastructure
{
    internal class TaxCalculatorApplication<T> : WebApplicationFactory<T> where T : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<TaxDbContext>));
                services.RemoveAll(typeof(ITaxProcessor));
                services.AddDbContext<TaxDbContext>(options => options.UseInMemoryDatabase("TaxDB"));
                services.AddInfrastructureServices();
                
            });
            
            return base.CreateHost(builder);
        }
    }
}
