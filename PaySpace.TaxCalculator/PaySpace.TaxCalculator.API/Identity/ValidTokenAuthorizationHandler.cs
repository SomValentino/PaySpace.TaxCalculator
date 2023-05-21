using Microsoft.AspNetCore.Authorization;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using System.IdentityModel.Tokens.Jwt;

namespace PaySpace.TaxCalculator.API.Identity
{
    public class ValidTokenAuthorizationHandler : AuthorizationHandler<ValidTokenRequirement>
    {
        private readonly IUnitOfWork _unitWork;

        public ValidTokenAuthorizationHandler(IUnitOfWork unitOfWork)
        {
            _unitWork = unitOfWork;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
            ValidTokenRequirement requirement)
        {
            var tokenId = context.User.Claims.FirstOrDefault(claim => claim.Type == JwtRegisteredClaimNames.Jti)?.Value;
            var clientId = context.User.Identity?.Name;

            var isTokenValid = await _unitWork.ClientRegistrationRepository.AnyAsync(
                registration => registration.ClientId == clientId
                                && registration.TokenId == tokenId
                                && registration.Active);

            if (isTokenValid)
            {
                context.Succeed(requirement);
            }
        }
    }
}
