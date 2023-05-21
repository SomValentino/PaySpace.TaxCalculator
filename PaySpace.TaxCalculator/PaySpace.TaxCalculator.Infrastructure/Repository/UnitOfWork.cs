using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Infrastructure.Data;

namespace PaySpace.TaxCalculator.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaxDbContext _taxDbContext;
        private IPostalCodeTaxMapRepository postalCodeTaxMapRepository;
        private IProgressiveTaxTableRepository progressiveTaxTableRepository;
        private ITaxResultRepository taxResultRepository;
        private IClientRegistrationRepository clientRegistrationRepository;

        public UnitOfWork(TaxDbContext taxDbContect)
        {
            _taxDbContext = taxDbContect;
        }
        public IPostalCodeTaxMapRepository PostalCodeTaxMapRepository
        {
            get
            {
                if(postalCodeTaxMapRepository == null)
                {
                    postalCodeTaxMapRepository = new PostalCodeMapRepository(_taxDbContext);
                }
                return postalCodeTaxMapRepository;
            }
        }

        public IProgressiveTaxTableRepository ProgressiveTaxTableRepository
        {
            get
            {
                if (progressiveTaxTableRepository == null)
                {
                    progressiveTaxTableRepository = new ProgressiveTableRepository(_taxDbContext);
                }
                return progressiveTaxTableRepository;
            }
        }

        public ITaxResultRepository TaxResultRepository
        {
            get
            {
                if (taxResultRepository == null)
                {
                    taxResultRepository = new TaxResultRepository(_taxDbContext);
                }
                return taxResultRepository;
            }
        }

        public IClientRegistrationRepository ClientRegistrationRepository
        {
            get
            {
                if(clientRegistrationRepository == null)
                {
                    clientRegistrationRepository = new ClientRegistrationRepository(_taxDbContext); 
                }
                return clientRegistrationRepository;
            }
        }

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
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
