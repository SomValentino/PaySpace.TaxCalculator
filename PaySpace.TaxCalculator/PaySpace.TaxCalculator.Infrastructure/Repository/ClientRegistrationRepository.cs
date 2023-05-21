using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Infrastructure.Data;

namespace PaySpace.TaxCalculator.Infrastructure.Repository
{
    public class ClientRegistrationRepository : BaseRepository<string,ClientRegistration>, IClientRegistrationRepository
    {
        public ClientRegistrationRepository(TaxDbContext taxDbContext) : base(taxDbContext)
        {
            
        }
    }
}
