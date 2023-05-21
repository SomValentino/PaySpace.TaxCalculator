using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Infrastructure.Data;

namespace PaySpace.TaxCalculator.Infrastructure.Repository
{
    public class ProgressiveTableRepository : BaseRepository<int, ProgressiveTaxEntry>, IProgressiveTaxTableRepository
    {
        public ProgressiveTableRepository(TaxDbContext taxDbContext) : base(taxDbContext)
        {
        }
    }
}
