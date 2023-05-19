using System.ComponentModel.DataAnnotations;

namespace PaySpace.TaxCalculator.API.Dto
{
    public record TaxRequest
    {
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public decimal AnnualIncome { get; set; }
    }
}
