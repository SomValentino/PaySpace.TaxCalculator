using Microsoft.EntityFrameworkCore;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaxDbContext _taxDbContext;

        public UnitOfWork(TaxDbContext taxDbContect)
        {
            _taxDbContext = taxDbContect;
        }
        public IPostalCodeTaxMapRepository PostalCodeTaxMapRepository => new PostalCodeMapRepository(_taxDbContext);

        public IProgressiveTaxTableRepository ProgressiveTaxTableRepository => new ProgressiveTableRepository(_taxDbContext);

        public ITaxResultRepository TaxResultRepository => new TaxResultRepository(_taxDbContext);

        public async Task<int> SaveToDatabaseAsync()
        {
            return await _taxDbContext.SaveChangesAsync();
        }
        private bool disposed = false;


        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
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
