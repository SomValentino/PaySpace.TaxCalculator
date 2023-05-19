using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Services
{
    public interface ITaxService
    {
        Task<TaxResult> CalculateTaxAsync(PostalCodeTaxEntry postalCodeTaxEntry, decimal annualIncome);

        Task<PostalCodeTaxEntry> GetPostalCodeTaxEntryAsync(string postalCode);
    }
}
