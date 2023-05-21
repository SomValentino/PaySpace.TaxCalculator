using Microsoft.EntityFrameworkCore;
using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.Infrastructure.Data
{
    public class TaxDbContext : DbContext
    {
        public TaxDbContext(DbContextOptions<TaxDbContext> contextOptions) : base(contextOptions)
        {
            
        }

        public DbSet<TaxResult> TaxResults => Set<TaxResult>();
        public DbSet<PostalCodeTaxEntry> PostalCodeTaxMap => Set<PostalCodeTaxEntry>();
        public DbSet<ProgressiveTaxEntry> ProgressiveTaxTable => Set<ProgressiveTaxEntry>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PostalCodeTaxEntry>().HasMany(x => x.TaxResults)
                .WithOne(x => x.PostalCodeTaxMap).HasForeignKey(x => x.PostalCode);
            base.OnModelCreating(modelBuilder);
        }
    }
}
