using Microsoft.Extensions.DependencyInjection;
using PaySpace.TaxCalculator.API.Controllers;
using PaySpace.TaxCalculator.Application.Features.Processors;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Tests.TaxProcessors
{
    public class TaxProcessorTests
    {
        private readonly IServiceProvider _serviceProvider;

        public TaxProcessorTests()
        {
            var taxCalculatorApplication = new TaxCalculatorApplication<TaxController>();
            _serviceProvider = taxCalculatorApplication.Services.CreateScope().ServiceProvider;
        }
        [Test]
        public void FlatRateTaxProcessor_Given_AnnaulIncome_Returns_Correct_TaxValue()
        {
            var postalCodeEntry = new PostalCodeTaxEntry
            {
                PostalCode = "7000",
                TaxCalculationType = Domain.Enums.TaxCalculationType.FlatRate,
                Rate = 17.50M
            };

            var flatRateProcessor = _serviceProvider.GetRequiredService<FlatRateTaxProcessor>();

            var tax = flatRateProcessor.CalculateTax(postalCodeEntry, 350000.00M);

            var expectedValue = 61250.00M;

            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void FlatValueTaxProcessor_Given_Less_Than_200000_Returns_TaxValue()
        {
            var postalCodeEntry = new PostalCodeTaxEntry
            {
                PostalCode = "A100",
                TaxCalculationType = Domain.Enums.TaxCalculationType.FlatRate,
                Rate = 5.00M,
                Amount = 10000.00M,
                Threshold = 200000.00M
            };

            var flatValueProcessor = _serviceProvider.GetRequiredService<FlatValueTaxProcessor>();

            var tax = flatValueProcessor.CalculateTax(postalCodeEntry, 150987.83M);
            var expectedValue = 7549.39M;
            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void FlatValueTaxProcessor_Given_Greater_Than_200000_Returns_TaxValue()
        {
            var postalCodeEntry = new PostalCodeTaxEntry
            {
                PostalCode = "A100",
                TaxCalculationType = Domain.Enums.TaxCalculationType.FlatRate,
                Rate = 5.00M,
                Amount = 10000.00M
            };

            var flatValueProcessor = _serviceProvider.GetRequiredService<FlatValueTaxProcessor>();

            var tax = flatValueProcessor.CalculateTax(postalCodeEntry, 250987.83M);
            var expectedValue = 10000.00M;
            Assert.AreEqual(expectedValue, tax);
        }
    }
}
