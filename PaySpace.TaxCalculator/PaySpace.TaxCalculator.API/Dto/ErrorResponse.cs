using System.Net;

namespace PaySpace.TaxCalculator.API.Dto
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }
    }
}
