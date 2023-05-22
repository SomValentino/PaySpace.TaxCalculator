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
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if (!entry.Threshold.HasValue && !entry.Amount.HasValue && !entry.Rate.HasValue)
            {
                throw new InvalidOperationException($"Threshold, Amount and Rate value must be configured for {nameof(FlatValueTaxProcessor)}");
            }

            if (annualIncome < entry.Threshold.Value)
            {
                decimal rate = entry.Rate.Value;
                decimal tax = (rate / 100) * annualIncome;
                return decimal.Round(tax, 2, MidpointRounding.AwayFromZero);
            }

            return entry.Amount.Value;
        }
    }
}
