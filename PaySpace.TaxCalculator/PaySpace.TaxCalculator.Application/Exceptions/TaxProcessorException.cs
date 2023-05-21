namespace PaySpace.TaxCalculator.Application.Exceptions
{
    public class TaxProcessorException: Exception
    {
        public TaxProcessorException()
        {
            
        }
        public TaxProcessorException(string message): base(message)
        {
            
        }

        public TaxProcessorException(Exception ex, string message) : base(message,ex)
        {
            
        }
    }
}
