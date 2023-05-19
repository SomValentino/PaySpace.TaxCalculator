using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaySpace.TaxCalculator.API.Dto;
using PaySpace.TaxCalculator.Application.Contracts.Services;

namespace PaySpace.TaxCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;
        public TaxController(ITaxService taxService)
        {
            _taxService = taxService;
        }

        [HttpPost]
        public async Task<IActionResult> CalculateTax([FromBody] TaxRequest taxRequest)
        {
            throw new NotImplementedException();
        }
    }
}
