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
            try
            {
                var postalCodeTaxEntry = await _taxService.GetPostalCodeTaxEntryAsync(taxRequest.PostalCode);

                if (postalCodeTaxEntry == null) return BadRequest($"No tax mapping found for postal code:{taxRequest.PostalCode}");

                var taxResult = await _taxService.CalculateTaxAsync(postalCodeTaxEntry, taxRequest.AnnualIncome);

                var taxResponse = new TaxResponse(taxResult);

                return CreatedAtAction("gettaxresult", new { id = taxResult.Id }, taxResponse);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
