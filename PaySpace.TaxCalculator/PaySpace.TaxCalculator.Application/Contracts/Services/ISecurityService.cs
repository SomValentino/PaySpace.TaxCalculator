using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.Application.Contracts.Services
{
    public interface ISecurityService
    {
        Task RegisterClient(ClientRegistration clientRegistration);
        Task<(string tokenId, string token)> GenerateJwtFor(string clientId, string role);
    }
}
