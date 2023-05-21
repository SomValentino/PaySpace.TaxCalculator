using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Caching.Distributed;
using System.Net.Http.Headers;

namespace PaySpace.TaxCalculator.WebApp.Services
{
    public class TokenService : ITokenService
    {
        private readonly IDistributedCache _cache;

        public TokenService(IDistributedCache distributedCache)
        {
            _cache = distributedCache;
        }
        public async Task SetToken(HttpClient client,
            string clientId, string clientName)
        {
            var token = await _cache.GetStringAsync(clientId);

            if (token == null)
            {
                var response = await client.PostAsJsonAsync("api/auth", new { clientId, clientName });

                response.EnsureSuccessStatusCode();

                token = await response.Content.ReadAsStringAsync();

                token = token.Replace("\"", string.Empty);

                await _cache.SetStringAsync(clientId,token ,new DistributedCacheEntryOptions { SlidingExpiration = TimeSpan.FromSeconds(3000)});
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, token);
        }
    }
}
