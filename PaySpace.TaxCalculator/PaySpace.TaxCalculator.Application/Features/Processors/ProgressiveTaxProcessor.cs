using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Application.Exceptions;
using PaySpace.TaxCalculator.Domain.Entities;
using PaySpace.TaxCalculator.Domain.Enums;

namespace PaySpace.TaxCalculator.Application.Features.Processors
{
    public class ProgressiveTaxProcessor : ITaxProcessor
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProgressiveTaxProcessor(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TaxCalculationType GetTaxCalculationType => TaxCalculationType.Progressive;

        public decimal CalculateTax(decimal annualIncome, PostalCodeTaxEntry entry)
        {
            var progressiveTable = _unitOfWork.ProgressiveTaxTableRepository.Get();

            if (progressiveTable == null || !progressiveTable.Any()) 
                throw new TaxProcessorException($"Could not obtain a progressive tax table for {nameof(ProgressiveTaxProcessor)}");

            var sum = 0.0M;
            var tax = 0.0M;

            foreach (var row in progressiveTable)
            {
                decimal? rangediff = row.ToAmount.HasValue ? 
                    row.ToAmount.Value - row.FromAmount : null;

                var sumdiff = annualIncome - sum;

                if(rangediff.HasValue && sumdiff > rangediff)
                {
                    sum += rangediff.Value;
                    tax += (rangediff.Value * row.Rate) / 100;
                }
                else
                {
                    tax += (sumdiff * row.Rate) / 100;
                    break;
                }
            }

            return Math.Round(tax, 2);
        }
    }
}
