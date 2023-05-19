using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Repository
{
    public interface IUnitOfWork
    {
        public IPostalCodeTaxMapRepository PostalCodeTaxMapRepository { get; }
        public IProgressiveTaxTableRepository ProgressiveTaxTableRepository { get;  }
        public ITaxResultRepository TaxResultRepository { get; }
    }
}
