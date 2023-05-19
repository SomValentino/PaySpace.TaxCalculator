using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Domain.Entities
{
    public class TaxResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public decimal AnnualIncome { get; set; }
        [Required]
        public decimal Tax { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public PostalCodeTaxEntry PostalCodeTaxMap { get; set; }
    }
}
