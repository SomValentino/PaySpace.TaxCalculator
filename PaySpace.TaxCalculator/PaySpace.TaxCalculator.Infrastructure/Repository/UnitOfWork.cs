using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Infrastructure.Data;

namespace PaySpace.TaxCalculator.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly TaxDbContext _taxDbContext;
        private Lazy<IPostalCodeTaxMapRepository> _postalCodeTaxMapRepository;
        private Lazy<IProgressiveTaxTableRepository> _progressiveTaxTableRepository;
        private Lazy<ITaxResultRepository> _taxResultRepository;
        private Lazy<IClientRegistrationRepository> _clientRegistrationRepository;

        public UnitOfWork(TaxDbContext taxDbContext)
        {
            _taxDbContext = taxDbContext;
            _postalCodeTaxMapRepository = new Lazy<IPostalCodeTaxMapRepository>(() => new PostalCodeMapRepository(_taxDbContext));
            _progressiveTaxTableRepository = new Lazy<IProgressiveTaxTableRepository>(() => new ProgressiveTableRepository(_taxDbContext));
            _taxResultRepository = new Lazy<ITaxResultRepository>(() => new TaxResultRepository(_taxDbContext));
            _clientRegistrationRepository = new Lazy<IClientRegistrationRepository>(() => new ClientRegistrationRepository(_taxDbContext));
        }

        public IPostalCodeTaxMapRepository PostalCodeTaxMapRepository => _postalCodeTaxMapRepository.Value;

        public IProgressiveTaxTableRepository ProgressiveTaxTableRepository => _progressiveTaxTableRepository.Value;

        public ITaxResultRepository TaxResultRepository => _taxResultRepository.Value;

        public IClientRegistrationRepository ClientRegistrationRepository => _clientRegistrationRepository.Value;

        public async Task<int> SaveToDatabaseAsync()
        {
            return await _taxDbContext.SaveChangesAsync();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _taxDbContext.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
