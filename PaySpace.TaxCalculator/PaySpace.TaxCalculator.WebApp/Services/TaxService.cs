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

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                var apiErrorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(error);

                throw new Exception(apiErrorResponse?.ErrorMessage);
            }

            var data = await response.Content.ReadAsStringAsync();

            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(data);

            return taxResponse;
        }
    }
}
