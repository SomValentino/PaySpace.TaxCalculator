using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Processors
{
    public interface ITaxProcessor
    {
        TaxCalculationType GetTaxCalculationType {  get; }
        decimal CalculateTax(decimal annualIncome, PostalCodeTaxEntry entry = null);
    }
}
