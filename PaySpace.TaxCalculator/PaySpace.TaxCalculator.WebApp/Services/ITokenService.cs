namespace PaySpace.TaxCalculator.WebApp.Services
{
    public interface ITokenService
    {
        Task SetToken(HttpClient client,
            string clientId, string clientName);
    }
}
