using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Exceptions;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;

namespace PaySpace.TaxCalculator.Application.Features.Processors
{
    public class FlatValueTaxProcessor : ITaxProcessor
    {
        public TaxCalculationType GetTaxCalculationType => TaxCalculationType.FlatValue;

        public decimal CalculateTax(decimal annualIncome, PostalCodeTaxEntry entry)
        {
            if (!entry.Threshold.HasValue && !entry.Amount.HasValue)
                throw new TaxProcessorException($"Threshold and Amount value must be configured for {nameof(FlatValueTaxProcessor)}");

            if(annualIncome < entry.Threshold)
            {
                var tax = (entry.Rate / 100) * annualIncome;
                return Math.Round(tax.Value, 2);
            }
            return entry.Amount.Value;
        }
    }
}
