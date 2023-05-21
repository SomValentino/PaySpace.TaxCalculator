using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.Application.Contracts.Repository
{
    public interface IProgressiveTaxTableRepository : IRepository<int,ProgressiveTaxEntry>
    {
    }
}
