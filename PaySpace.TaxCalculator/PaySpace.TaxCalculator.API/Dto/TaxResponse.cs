namespace PaySpace.TaxCalculator.API.Dto
{
    public record TaxResponse
    {
        public decimal Tax { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
