using System.ComponentModel.DataAnnotations;

namespace PaySpace.TaxCalculator.WebApp.Models
{
    public class TaxModel
    {
        [Required]
        [Display(Name ="Postal Code")]
        public string PostalCode { get; set; }
        [Required]
        [Display(Name = "Annual Income")]
        public decimal AnnualIncome { get; set; }
        public string? ErrorMessage { get; set; }
        public string? SuccessMessage { get; set; }
    }
}
