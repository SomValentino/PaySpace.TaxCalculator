using PaySpace.TaxCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Processors
{
    public interface ITaxProcessorFactory
    {
        ITaxProcessor? GetTaxProcessorInstance(TaxCalculationType taxCalculationType);
    }
}
