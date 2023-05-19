using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Repository
{
    public interface IUnitOfWork
    {
        IPostalCodeTaxMapRepository PostalCodeTaxMapRepository { get; }
        IProgressiveTaxTableRepository ProgressiveTaxTableRepository { get;  }
        ITaxResultRepository TaxResultRepository { get; }
        Task<int> SaveToDatabaseAsync();
    }
}
