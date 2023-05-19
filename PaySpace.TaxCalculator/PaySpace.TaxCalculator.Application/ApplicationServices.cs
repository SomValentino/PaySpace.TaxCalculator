using Microsoft.Extensions.DependencyInjection;
using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Contracts.Services;
using PaySpace.TaxCalculator.Application.Features.Processors;
using PaySpace.TaxCalculator.Application.Features.Services;
using PaySpace.TaxCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application
{
    public static class ApplicationServices
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<FlatRateTaxProcessor>();
            services.AddScoped<FlatValueTaxProcessor>();
            services.AddScoped<ProgressiveTaxProcessor>();
            services.AddScoped<ITaxService, TaxService>();
        }
    }
}
