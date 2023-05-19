﻿using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Infrastructure.Repository
{
    public class PostalCodeMapRepository : BaseRepository<string,PostalCodeTaxEntry>, IPostalCodeTaxMapRepository
    {
        public PostalCodeMapRepository(TaxDbContext taxDbContext): base(taxDbContext)
        {
            
        }
    }
}
