using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PaySpace.TaxCalculator.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Tests.Infrastructure
{
    internal class TaxCalculatorApplication<T> : WebApplicationFactory<T> where T : class
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<TaxDbContext>));
                services.AddDbContext<TaxDbContext>(options => options.UseInMemoryDatabase("TaxDB"));
            });
            return base.CreateHost(builder);
        }
    }
}
