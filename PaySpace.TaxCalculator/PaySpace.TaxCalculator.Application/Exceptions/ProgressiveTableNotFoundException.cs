namespace PaySpace.TaxCalculator.Application.Exceptions
{
    public class ProgressiveTableNotFoundException : Exception
    {
        public ProgressiveTableNotFoundException()
        {

        }
        public ProgressiveTableNotFoundException(string message) : base(message)
        {

        }

        public ProgressiveTableNotFoundException(Exception ex, string message) : base(message, ex)
        {

        }
    }
}
