using Microsoft.Extensions.DependencyInjection;
using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Contracts.Services;
using PaySpace.TaxCalculator.Application.Features.Processors;
using PaySpace.TaxCalculator.Application.Features.Services;

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
            services.AddScoped<ITaxProcessorFactory, TaxProcessorFactory>();
        }
    }
}
