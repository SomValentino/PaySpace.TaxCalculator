using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.Application.Contracts.Repository
{
    public interface ITaxResultRepository : IRepository<int,TaxResult>
    {
    }
}
