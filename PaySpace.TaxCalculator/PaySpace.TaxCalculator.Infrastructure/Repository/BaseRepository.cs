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
    public abstract class BaseRepository<TKey, TEntity> : IRepository<TKey, TEntity> where TEntity : class
    {
        private readonly TaxDbContext _context;
        private readonly DbSet<TEntity> _dataSet;

        public BaseRepository(TaxDbContext taxDbContext)
        {
            _context = taxDbContext;
            _dataSet = _context.Set<TEntity>();
        }
        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return (await _dataSet.AddAsync(entity)).Entity;
        }

        public void Delete(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
            _dataSet.Remove(entity);
        }

        public IEnumerable<TEntity> Get()
        {
            return _dataSet.ToList();
        }

        public async Task<TEntity> GetAsync(TKey key)
        {
            return await _dataSet.FindAsync(key);
        }

        public async Task<IEnumerable<TEntity>> GetAsync()
        {
            return await _dataSet.ToListAsync();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _dataSet.Update(entity);
        }
    }
}
