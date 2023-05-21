using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;

namespace PaySpace.TaxCalculator.Application.Contracts.Processors
{
    public interface ITaxProcessor
    {
        TaxCalculationType GetTaxCalculationType {  get; }
        decimal CalculateTax(decimal annualIncome, PostalCodeTaxEntry entry);
    }
}
