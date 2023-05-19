﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Contracts.Repository
{
    public interface IRepository<TKey, TEntity> where TEntity : class
    {
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> GetAsync(TKey entity);
        Task<IEnumerable<TEntity>> GetAsync();
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
