using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using PaySpace.TaxCalculator.API.Controllers;
using PaySpace.TaxCalculator.API.Dto;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Tests.Infrastructure;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PaySpace.TaxCalculator.Tests.API
{
    public class TaxControllerTests
    {
        private IServiceProvider _serviceProvider;
        private readonly HttpClient _client;
        private readonly string _clientId;

        public TaxControllerTests()
        {
            var taxCalculatorApplication = new TaxCalculatorApplication<TaxController>();
            _serviceProvider = taxCalculatorApplication.Services.CreateScope().ServiceProvider;
            _client = taxCalculatorApplication.CreateClient();
            _clientId = Guid.NewGuid().ToString();  
        }

        

        [Test]
        public async Task CalculateTax_Given_No_Token_Returns_UnAuthorized()
        {
            _client.DefaultRequestHeaders.Authorization = null;
            var response = await _client.PostAsJsonAsync("api/tax", new TaxRequest
            {
                PostalCode = "7000",
                AnnualIncome = 559908.34M
            });

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Test]
        public async Task CalculateTax_Given_FlatRate_PostalCode_And_AnnualIncome_Returns_TaxValue()
        {
            RegisterClientForToken(_clientId, "testClient").Wait();
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

        [Test]
        public async Task CalculateTax_Given_FlatValue_PostalCode_And_AnnualIncome_Less_Than_200000_Returns_TaxValue()
        {
            RegisterClientForToken(_clientId, "testClient").Wait();
            var response = await _client.PostAsJsonAsync("api/tax", new TaxRequest
            {
                PostalCode = "A100",
                AnnualIncome = 150987.83M
            });
            var data = await response.Content.ReadAsStringAsync();
            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(data);

            var unitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            var taxResults = await unitOfWork.TaxResultRepository.GetAsync();

            var expected_tax = 7549.39M;
            var expected_number_taxResult = 1;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(expected_tax, taxResponse.Tax);
        }

        [Test]
        public async Task CalculateTax_Given_FlatValue_PostalCode_And_AnnualIncome_Greater_Than_200000_Returns_TaxValue()
        {
            RegisterClientForToken(_clientId, "testClient").Wait();
            var response = await _client.PostAsJsonAsync("api/tax", new TaxRequest
            {
                PostalCode = "A100",
                AnnualIncome = 250987.83M
            });
            var data = await response.Content.ReadAsStringAsync();
            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(data);

            var unitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            var taxResults = await unitOfWork.TaxResultRepository.GetAsync();

            var expected_tax = 10000.00M;
            var expected_number_taxResult = 1;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(expected_tax, taxResponse.Tax);
        }

        [Test]
        public async Task CalculateTax_Given_Progressive_PostalCode_And_AnnualIncome_Returns_TaxValue()
        {
            RegisterClientForToken(_clientId, "testClient").Wait();
            var response = await _client.PostAsJsonAsync("api/tax", new TaxRequest
            {
                PostalCode = "7441",
                AnnualIncome = 443567.00M
            });
            var data = await response.Content.ReadAsStringAsync();
            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(data);

            var unitOfWork = _serviceProvider.GetService<IUnitOfWork>();
            var taxResults = await unitOfWork.TaxResultRepository.GetAsync();

            var expected_tax = 132932.34M;
            var expected_number_taxResult = 1;

            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            Assert.AreEqual(expected_tax, taxResponse.Tax);
        }

        private async Task RegisterClientForToken(string clientId, string clientName)
        {
            var response = await _client
                .PostAsJsonAsync($"/api/auth", new { clientId, clientName});

           var token =  await response.Content.ReadAsStringAsync();
            _client.DefaultRequestHeaders.Authorization =
             new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
        }
    }
}
