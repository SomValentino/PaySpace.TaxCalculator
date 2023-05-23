using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;

namespace PaySpace.TaxCalculator.Application.Contracts.Processors
{
    public interface ITaxProcessor
    {
        TaxCalculationType GetTaxCalculationType {  get; }
        decimal CalculateTaxAsync(decimal annualIncome, PostalCodeTaxEntry entry);
    }
}
