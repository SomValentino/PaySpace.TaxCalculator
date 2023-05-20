using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.API.Dto
{
    public record TaxResponse
    {
        public TaxResponse()
        {
            
        }

        public TaxResponse(TaxResult taxResult)
        {
            Tax = taxResult.Tax;
            CreatedAt = taxResult.CreatedAt;
        }
        public decimal Tax { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
