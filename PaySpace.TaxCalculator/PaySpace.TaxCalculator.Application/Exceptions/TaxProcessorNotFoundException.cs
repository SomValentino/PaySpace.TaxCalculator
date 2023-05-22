namespace PaySpace.TaxCalculator.Application.Exceptions
{
    public class TaxProcessorNotFoundException: Exception
    {
        public TaxProcessorNotFoundException()
        {
            
        }
        public TaxProcessorNotFoundException(string message): base(message)
        {
            
        }

        public TaxProcessorNotFoundException(Exception ex, string message) : base(message,ex)
        {
            
        }
    }
}
