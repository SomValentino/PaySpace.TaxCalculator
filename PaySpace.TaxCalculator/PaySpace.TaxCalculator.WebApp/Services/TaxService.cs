using Newtonsoft.Json;
using PaySpace.TaxCalculator.WebApp.Models;

namespace PaySpace.TaxCalculator.WebApp.Services
{
    public class TaxService : ITaxService
    {
        private readonly HttpClient _client;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;

        public TaxService(IHttpClientFactory httpClientFactory,
            ITokenService tokenService,
            IConfiguration configuration)
        {
            _client = httpClientFactory.CreateClient("Tax");
            _tokenService = tokenService;
            _configuration = configuration;
        }
        public async Task<TaxResponse> GetTaxResponse(TaxModel taxModel)
        {
            await _tokenService.SetToken(_client, _configuration["clientId"], _configuration["clientName"]);
            var response = await _client.PostAsJsonAsync("api/tax", taxModel);

            if (!response.IsSuccessStatusCode)
            {
                var apiErrorResponse = new ApiErrorResponse();
                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized || 
                    response.StatusCode == System.Net.HttpStatusCode.Forbidden)
                {
                    apiErrorResponse.StatusCode = response.StatusCode;
                    apiErrorResponse.ErrorMessage = "Authentication failed";
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    apiErrorResponse = JsonConvert.DeserializeObject<ApiErrorResponse>(error);
                }
                throw new Exception(apiErrorResponse?.ErrorMessage);
            }

            var data = await response.Content.ReadAsStringAsync();

            var taxResponse = JsonConvert.DeserializeObject<TaxResponse>(data);

            return taxResponse;
        }
    }
}
