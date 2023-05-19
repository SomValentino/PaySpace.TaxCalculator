using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PaySpace.TaxCalculator.API.Controllers;
using PaySpace.TaxCalculator.API.Dto;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Tests.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Tests.API
{
    public class TaxControllerTests
    {
        private IServiceProvider _serviceProvider;
        private readonly HttpClient _client;

        public TaxControllerTests()
        {
            var taxCalculatorApplication = new TaxCalculatorApplication<TaxController>();
            _serviceProvider = taxCalculatorApplication.Services.CreateScope().ServiceProvider;
            _client = taxCalculatorApplication.CreateClient();
        }

        [Test]
        public async Task CalculateTax_Given_FlatRate_PostalCode_And_AnnualIncome_Returns_TaxValue()
        {
            var response = await _client.PostAsJsonAsync("api/tax", new TaxRequest
            {
                PostalCode = "7000",
                AnnualIncome = 559908.34M
            });
            var data = await response.Content.ReadAsStringAsync();
            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(data);

            var unitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            var taxResults = await unitOfWork.TaxResultRepository.GetAsync();

            var expected_tax = 97983.96M;
            var expected_number_taxResult = 1;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(expected_tax, taxResponse.Tax);
            Assert.AreEqual(expected_number_taxResult, taxResults.Count());

        }

    }
}
