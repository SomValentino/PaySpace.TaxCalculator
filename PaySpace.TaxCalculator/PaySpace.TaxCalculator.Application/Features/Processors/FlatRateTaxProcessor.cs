using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Exceptions;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;

namespace PaySpace.TaxCalculator.Application.Features.Processors
{
    public class FlatRateTaxProcessor : ITaxProcessor
    {
        public TaxCalculationType GetTaxCalculationType => TaxCalculationType.FlatRate;

        public decimal CalculateTax(decimal annualIncome, PostalCodeTaxEntry entry)
        {
            if (!entry.Rate.HasValue) throw new TaxProcessorException($"Rate value not configured for {nameof(FlatRateTaxProcessor)}");
            
            var tax = (entry.Rate / 100) * annualIncome;

            return Math.Round(tax.Value, 2);
        }
    }
}
