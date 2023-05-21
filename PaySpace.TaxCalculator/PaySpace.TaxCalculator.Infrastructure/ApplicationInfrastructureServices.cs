using Microsoft.Extensions.DependencyInjection;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Infrastructure.Repository;

namespace PaySpace.TaxCalculator.Infrastructure
{
    public static class ApplicationInfrastructureServices
    {
        public static void AddApplicationInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
