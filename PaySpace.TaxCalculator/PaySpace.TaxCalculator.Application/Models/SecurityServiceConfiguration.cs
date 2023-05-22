using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Models
{
    public class SecurityServiceConfiguration
    {
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string Role { get; set; }
    }
}
