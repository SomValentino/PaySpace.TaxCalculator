using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Infrastructure.Repository
{
    public class TaxResultRepository : BaseRepository<int, TaxResult>, ITaxResultRepository
    {
        public TaxResultRepository(TaxDbContext taxDbContext) : base(taxDbContext)
        {
        }
    }
}
