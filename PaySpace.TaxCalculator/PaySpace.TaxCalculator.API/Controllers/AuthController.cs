using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaySpace.TaxCalculator.API.Dto;
using PaySpace.TaxCalculator.Application.Contracts.Services;
using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;

        public AuthController(ISecurityService securityService,
            IConfiguration configuration,
            ILogger<AuthController> logger)
        {
            _securityService = securityService;
            _configuration = configuration;
            _logger = logger;
        }

        /// <summary>
        /// Registers a client an output token to access API
        /// </summary>
        /// <param name="authRequest"></param>
        /// <returns>token</returns>
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] AuthRequest authRequest)
        {
            var role = _configuration["role"];
            var (tokenId, token) = await _securityService.GenerateJwtFor(authRequest.ClientId, role);

            var clientRegistration = new ClientRegistration
            {
                ClientId = authRequest.ClientId,
                Name = authRequest.ClientName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TokenId = tokenId,
            };

            await _securityService.RegisterClient(clientRegistration);

            return Ok(token);
        }
    }
}
