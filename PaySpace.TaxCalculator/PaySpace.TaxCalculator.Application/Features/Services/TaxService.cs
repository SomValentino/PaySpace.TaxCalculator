using PaySpace.TaxCalculator.Application.Contracts.Services;
using PaySpace.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Features.Services
{
    public class TaxService : ITaxService
    {
        public TaxResult CalculateTax(string postalCode, decimal annualIncome)
        {
            throw new NotImplementedException();
        }
    }
}
