using PaySpace.TaxCalculator.Domain.Enums;

namespace PaySpace.TaxCalculator.Application.Contracts.Processors
{
    public interface ITaxProcessorFactory
    {
        ITaxProcessor? GetTaxProcessorInstance(TaxCalculationType taxCalculationType);
    }
}
