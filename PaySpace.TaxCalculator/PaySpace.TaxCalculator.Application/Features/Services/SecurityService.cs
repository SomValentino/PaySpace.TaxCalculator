using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Application.Contracts.Services;
using PaySpace.TaxCalculator.Application.Models;
using PaySpace.TaxCalculator.Domain.Entities;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PaySpace.TaxCalculator.Application.Features.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly SecurityServiceConfiguration _configuration;

        public SecurityService(IUnitOfWork unitOfWork, IOptions<SecurityServiceConfiguration> configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration?.Value;
        }

        public async Task<(string tokenId, string token)> GenerateJwtFor(string clientId, string role)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.JwtKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenId = Guid.NewGuid().ToString();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, clientId),
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Role,_configuration.Role),
                new Claim(ClaimTypes.Role, role)
            };

            var token = new JwtSecurityToken(
                issuer: _configuration.JwtIssuer,
                audience: _configuration.JwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddSeconds(3600),
                signingCredentials: credentials);

            return (tokenId, new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task RegisterClient(ClientRegistration clientRegistration)
        {
            var client = await _unitOfWork.ClientRegistrationRepository.GetAsync(clientRegistration.ClientId);

            if (client != null)
            {
                client.TokenId = clientRegistration.TokenId;
                client.UpdatedAt = DateTime.UtcNow;
                _unitOfWork.ClientRegistrationRepository.Update(client);
            }
            else
            {
                await _unitOfWork.ClientRegistrationRepository.CreateAsync(clientRegistration);
            }

            await _unitOfWork.SaveToDatabaseAsync();
        }
    }
}
