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
        private readonly ILogger<TaxController> _logger;

        public TaxController(ITaxService taxService, ILogger<TaxController> logger)
        {
            _taxService = taxService;
            _logger = logger;
        }

        [HttpGet("{id}",Name = "gettaxresult")]
        public async Task<IActionResult> GetTaxResult(int id)
        {
            var taxResult = await _taxService.GetTaxResultAsync(id);

            if(taxResult == null)  return NotFound();

            return Ok(taxResult);   
        }

        [HttpPost]
        public async Task<IActionResult> CalculateTax([FromBody] TaxRequest taxRequest)
        {
            _logger.LogInformation("Getting the postal code tax entry for postal code: {code}", taxRequest.PostalCode);
            var postalCodeTaxEntry = await _taxService.GetPostalCodeTaxEntryAsync(taxRequest.PostalCode);
            _logger.LogInformation("Found postal code entry for postal code: {code}", taxRequest.PostalCode);

            if (postalCodeTaxEntry == null)
            {
                return BadRequest(new ErrorResponse
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessage = $"No tax mapping found for postal code:{taxRequest.PostalCode}"
                });
            }

            _logger.LogInformation("Calculating tax for annual income: {income}", taxRequest.AnnualIncome);
            var taxResult = await _taxService.CalculateTaxAsync(postalCodeTaxEntry, taxRequest.AnnualIncome);
            _logger.LogInformation("Calculated tax with value: {value}",taxResult.Tax);

            var taxResponse = new TaxResponse(taxResult);

            return CreatedAtAction("gettaxresult", new { id = taxResult.Id }, taxResponse);
        }
    }
}
