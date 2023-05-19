using PaySpace.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Services
{
    public interface ITaxService
    {
        TaxResult CalculateTax(string postalCode, decimal annualIncome);
    }
}
