﻿using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PaySpace.TaxCalculator.API;
using PaySpace.TaxCalculator.Application.Contracts.Processors;
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
                services.RemoveAll(typeof(ITaxProcessor));
                services.AddDbContext<TaxDbContext>(options => options.UseInMemoryDatabase("TaxDB"));
                services.AddInfrastructureServices();
                var context = services.BuildServiceProvider().CreateScope().ServiceProvider.GetService<TaxDbContext>();

                TaxDataSeeder.Seed(context);
            }).UseEnvironment("Test");
            return base.CreateHost(builder);
        }
    }
}
