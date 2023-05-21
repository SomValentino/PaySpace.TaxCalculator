using System.Net;

namespace PaySpace.TaxCalculator.WebApp.Models
{
    public class ApiErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }    
    }
}
