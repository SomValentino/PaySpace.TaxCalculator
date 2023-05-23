using Microsoft.Extensions.DependencyInjection;
using PaySpace.TaxCalculator.API.Controllers;
using PaySpace.TaxCalculator.Application.Features.Processors;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Tests.Infrastructure;

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
        public void FlatRateTaxProcessor_Given_AnnualIncome_Returns_Correct_TaxValue()
        {
            var postalCodeEntry = new PostalCodeTaxEntry
            {
                PostalCode = "7000",
                TaxCalculationType = Domain.Enums.TaxCalculationType.FlatRate,
                Rate = 17.50M
            };

            var flatRateProcessor = _serviceProvider.GetRequiredService<FlatRateTaxProcessor>();

            var tax = flatRateProcessor.CalculateTaxAsync(350000.00M,postalCodeEntry);

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

            var tax = flatValueProcessor.CalculateTaxAsync(150987.83M, postalCodeEntry);
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
                Amount = 10000.00M,
                Threshold = 200000.00M
            };

            var flatValueProcessor = _serviceProvider.GetRequiredService<FlatValueTaxProcessor>();

            var tax = flatValueProcessor.CalculateTaxAsync(250987.83M,postalCodeEntry);
            var expectedValue = 10000.00M;
            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void ProgressiveTaxProcessor_Given_Annual_Income_5691_69_Returns_TaxValue()
        {
            var progressiveTaxProcessor = _serviceProvider.GetRequiredService<ProgressiveTaxProcessor>();

            var tax = progressiveTaxProcessor.CalculateTaxAsync(5691.69M, new PostalCodeTaxEntry());

            var expectedValue = 569.17M;

            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void ProgressiveTaxProcessor_Given_Annual_Income_10000_Returns_TaxValue()
        {
            var progressiveTaxProcessor = _serviceProvider.GetRequiredService<ProgressiveTaxProcessor>();

            var tax = progressiveTaxProcessor.CalculateTaxAsync(10000.00M, new PostalCodeTaxEntry());

            var expectedValue = 1082.50M;

            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void ProgressiveTaxProcessor_Given_Annual_Income_43567_Returns_TaxValue()
        {
            var progressiveTaxProcessor = _serviceProvider.GetRequiredService<ProgressiveTaxProcessor>();

            var tax = progressiveTaxProcessor.CalculateTaxAsync(43567.00M, new PostalCodeTaxEntry());

            var expectedValue = 7079.35M;

            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void ProgressiveTaxProcessor_Given_Annual_Income_143567_Returns_TaxValue()
        {
            var progressiveTaxProcessor = _serviceProvider.GetRequiredService<ProgressiveTaxProcessor>();

            var tax = progressiveTaxProcessor.CalculateTaxAsync(143567.00M, new PostalCodeTaxEntry());

            var expectedValue = 33918.92M;

            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void ProgressiveTaxProcessor_Given_Annual_Income_243567_Returns_TaxValue()
        {
            var progressiveTaxProcessor = _serviceProvider.GetRequiredService<ProgressiveTaxProcessor>();

            var tax = progressiveTaxProcessor.CalculateTaxAsync(243567.00M, new PostalCodeTaxEntry());

            var expectedValue = 65519.92M;

            Assert.AreEqual(expectedValue, tax);
        }

        [Test]
        public void ProgressiveTaxProcessor_Given_Annual_Income_443567_Returns_TaxValue()
        {
            var progressiveTaxProcessor = _serviceProvider.GetRequiredService<ProgressiveTaxProcessor>();

            var tax = progressiveTaxProcessor.CalculateTaxAsync(443567.00M, new PostalCodeTaxEntry());

            var expectedValue = 132932.34M;

            Assert.AreEqual(expectedValue, tax);
        }
    }
}
