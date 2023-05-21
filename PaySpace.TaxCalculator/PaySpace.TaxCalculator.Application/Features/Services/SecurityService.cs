using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Application.Contracts.Services;
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
        private readonly IConfiguration _configuration;

        public SecurityService(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
        }
        public async Task<(string tokenId, string token)> GenerateJwtFor(string clientId, string role)
        {
            
            var _key = _configuration["JwtKey"];
            var _issuer = _configuration["JwtIssuer"];
            var _audience = _configuration["JwtAudience"];

            var tokenId = Guid.NewGuid().ToString();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, clientId),
                new Claim(JwtRegisteredClaimNames.Jti, tokenId),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Role, "ApiKey"),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(180),
                signingCredentials: credentials);

            return (tokenId, new JwtSecurityTokenHandler().WriteToken(token));
        }

        public async Task RegisterClient(ClientRegistration clientRegistration)
        {
            var client = await _unitOfWork.ClientRegistrationRepository.GetAsync(clientRegistration.ClientId);

            if(client != null)
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
