using Newtonsoft.Json;
using PaySpace.TaxCalculator.WebApp.Models;

namespace PaySpace.TaxCalculator.WebApp.Services
{
    public class TaxService : ITaxService
    {
        private readonly HttpClient _client;

        public TaxService(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("Tax");
        }
        public async Task<TaxResponse> GetTaxResponse(TaxModel taxModel)
        {
            var response = await _client.PostAsJsonAsync("api/tax", taxModel);

            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();

            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(data);

            return taxResponse;
        }
    }
}
