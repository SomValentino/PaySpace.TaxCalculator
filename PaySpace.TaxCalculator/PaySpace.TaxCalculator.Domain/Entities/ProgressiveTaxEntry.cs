using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Domain.Entities
{
    public class ProgressiveTaxEntry
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public decimal Rate { get; set; }
        [Required]
        public decimal FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
    }
}
