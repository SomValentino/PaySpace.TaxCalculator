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

        public decimal CalculateTaxAsync(decimal annualIncome, PostalCodeTaxEntry entry)
        {
            var progressiveTable = _unitOfWork.ProgressiveTaxTableRepository.Get();

            if (progressiveTable == null || !progressiveTable.Any())
            {
                throw new ProgressiveTableNotFoundException($"Could not obtain a progressive tax table for {nameof(ProgressiveTaxProcessor)}");
            }

            decimal incomeSum = 0.0M;
            decimal totalTax = 0.0M;

            foreach (var row in progressiveTable)
            {
                decimal? rangeDiff = row.ToAmount.HasValue ? row.ToAmount.Value - row.FromAmount : null;
                decimal sumDiff = annualIncome - incomeSum;

                if (rangeDiff.HasValue && sumDiff > rangeDiff)
                {
                    incomeSum += rangeDiff.Value;
                    totalTax += (rangeDiff.Value * row.Rate) / 100;
                }
                else
                {
                    totalTax += (sumDiff * row.Rate) / 100;
                    break;
                }
            }

            return decimal.Round(totalTax, 2, MidpointRounding.AwayFromZero);
        }
    }
}
