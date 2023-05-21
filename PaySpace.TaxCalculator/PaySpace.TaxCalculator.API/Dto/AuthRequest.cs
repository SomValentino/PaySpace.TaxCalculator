using System.ComponentModel.DataAnnotations;

namespace PaySpace.TaxCalculator.API.Dto
{
    public class AuthRequest
    {
        [Required]
        public string ClientId { get; set; }
        [Required]
        public string ClientName { get; set; }
    }
}
