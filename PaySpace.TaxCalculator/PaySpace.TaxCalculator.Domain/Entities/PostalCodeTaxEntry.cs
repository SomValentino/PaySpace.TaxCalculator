using PaySpace.TaxCalculator.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PaySpace.TaxCalculator.Domain.Entities
{
    public class PostalCodeTaxEntry
    {
        [Key]
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public TaxCalculationType TaxCalculationType { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Threshold { get; set; }
        public ICollection<TaxResult> TaxResults { get; set; }
    }
}
