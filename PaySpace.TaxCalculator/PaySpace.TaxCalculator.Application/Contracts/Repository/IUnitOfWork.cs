namespace PaySpace.TaxCalculator.Application.Contracts.Repository
{
    public interface IUnitOfWork:  IDisposable
    {
        IPostalCodeTaxMapRepository PostalCodeTaxMapRepository { get; }
        IProgressiveTaxTableRepository ProgressiveTaxTableRepository { get;  }
        ITaxResultRepository TaxResultRepository { get; }
        IClientRegistrationRepository ClientRegistrationRepository { get; }
        Task<int> SaveToDatabaseAsync();
    }
}
