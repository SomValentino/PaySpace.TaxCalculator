using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Features.Processors
{
    public class FlatValueTaxProcessor : ITaxProcessor
    {
        public TaxCalculationType GetTaxCalculationType => TaxCalculationType.FlatValue;

        public decimal CalculateTax(PostalCodeTaxEntry entry, decimal annualIncome)
        {
            if(annualIncome < entry.Threshold)
            {
                var tax = (entry.Rate / 100) * annualIncome;
                return Math.Round(tax.Value, 2);
            }
            return entry.Amount.Value;
        }
    }
}
