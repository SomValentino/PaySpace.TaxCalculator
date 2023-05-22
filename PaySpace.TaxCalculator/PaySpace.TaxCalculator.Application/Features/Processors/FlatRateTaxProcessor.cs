using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;

namespace PaySpace.TaxCalculator.Application.Features.Processors
{
    public class FlatRateTaxProcessor : ITaxProcessor
    {
        public TaxCalculationType GetTaxCalculationType => TaxCalculationType.FlatRate;

        public decimal CalculateTax(decimal annualIncome, PostalCodeTaxEntry entry)
        {
            if (entry == null)
            {
                throw new ArgumentNullException(nameof(entry));
            }

            if (!entry.Rate.HasValue)
            {
                throw new InvalidOperationException($"Rate value not configured for {nameof(FlatRateTaxProcessor)}");
            }

            decimal rate = entry.Rate.Value;

            decimal tax = (rate / 100) * annualIncome;

            return decimal.Round(tax, 2, MidpointRounding.AwayFromZero);
        }
    }
}
