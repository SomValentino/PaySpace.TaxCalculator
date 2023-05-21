using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Domain.Entities
{
    public class ClientRegistration
    {
        [Key]
        [Required]
        public string ClientId { get; set; }
        public string Name { get; set; }
        [Required]
        public string TokenId { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public bool Active { get; set; }
    }
}
