using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Features.Processors
{
    public class TaxProcessorFactory : ITaxProcessorFactory
    {
        private readonly IEnumerable<ITaxProcessor> _taxProcessors;

        public TaxProcessorFactory(IEnumerable<ITaxProcessor> taxProcessors)
        {
            _taxProcessors = taxProcessors;
        }

        public ITaxProcessor? GetTaxProcessorInstance(TaxCalculationType taxCalculationType)
        {
            return _taxProcessors.SingleOrDefault(x => x.GetTaxCalculationType == taxCalculationType);
        }
    }
}
