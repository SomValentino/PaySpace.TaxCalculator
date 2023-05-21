using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.Application.Contracts.Services
{
    public interface ITaxService
    {
        Task<TaxResult> CalculateTaxAsync(PostalCodeTaxEntry postalCodeTaxEntry, decimal annualIncome);

        Task<TaxResult> GetTaxResultAsync(int id);

        Task<PostalCodeTaxEntry> GetPostalCodeTaxEntryAsync(string postalCode);
    }
}
