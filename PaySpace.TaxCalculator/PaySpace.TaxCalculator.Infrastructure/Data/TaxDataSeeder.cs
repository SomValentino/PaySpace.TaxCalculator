using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Infrastructure.Data
{
    public class TaxDataSeeder
    {
        public static void Seed(TaxDbContext context)
        {
            if(!context.PostalCodeTaxMap.Any())
            {
                context.PostalCodeTaxMap.AddRange(new List<PostalCodeTaxEntry>
                {
                    new PostalCodeTaxEntry
                    {
                        PostalCode = "7441",
                        TaxCalculationType = TaxCalculationType.Progressive
                    },
                    new PostalCodeTaxEntry
                    {
                        PostalCode = "A100",
                        TaxCalculationType = TaxCalculationType.FlatValue,
                        Amount = 10000.00M,
                        Rate = 5.00M,
                        Threshold = 200000.00M
                    },
                    new PostalCodeTaxEntry
                    {
                        PostalCode = "7000",
                        TaxCalculationType = TaxCalculationType.FlatRate,
                        Rate = 17.50M
                    },
                    new PostalCodeTaxEntry
                    {
                        PostalCode = "1000",
                        TaxCalculationType = TaxCalculationType.Progressive
                    }
                });
            }
            
            if(!context.PostalCodeTaxMap.Any())
            {
                context.ProgressiveTaxTable.AddRange(new List<ProgressiveTaxEntry>
                {
                    new ProgressiveTaxEntry
                    {
                        Rate = 10.00M,
                        FromAmount = 0.00M,
                        ToAmount = 8350.00M,
                    },
                    new ProgressiveTaxEntry
                    {
                        Rate = 15.00M,
                        FromAmount = 8351.00M,
                        ToAmount = 33950.00M,
                    },
                    new ProgressiveTaxEntry
                    {
                        Rate = 25.00M,
                        FromAmount = 33951.00M,
                        ToAmount = 82250.00M,
                    },
                    new ProgressiveTaxEntry
                    {
                        Rate = 28.00M,
                        FromAmount = 82251.00M,
                        ToAmount = 171550.00M,
                    },
                    new ProgressiveTaxEntry
                    {
                        Rate = 33.00M,
                        FromAmount = 171551.00M,
                        ToAmount = 372950.00M,
                    },
                    new ProgressiveTaxEntry
                    {
                        Rate = 35.00M,
                        FromAmount = 372951.00M
                    },
                });
            }
            

            context.SaveChanges();
        }
    }
}
