using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Application.Contracts.Services;
using PaySpace.TaxCalculator.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaySpace.TaxCalculator.Application.Features.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxProcessorFactory _taxProcessorFactory;
        private readonly IUnitOfWork _unitOfWork;

        public TaxService(ITaxProcessorFactory taxProcessorFactory,IUnitOfWork unitOfWork)
        {
            _taxProcessorFactory = taxProcessorFactory;
            _unitOfWork = unitOfWork;
        }
        public async Task<TaxResult> CalculateTaxAsync(PostalCodeTaxEntry postalCodeTaxEntry, decimal annualIncome)
        {
            var taxProcessor = _taxProcessorFactory.GetTaxProcessorInstance(postalCodeTaxEntry.TaxCalculationType);

            if (taxProcessor == null) throw new ArgumentNullException($"No tax processor found for tax calculation type {postalCodeTaxEntry.TaxCalculationType}");

            var tax = taxProcessor.CalculateTax(annualIncome, postalCodeTaxEntry);

            var taxResult = new TaxResult
                            {
                                Tax = tax,
                                PostalCode = postalCodeTaxEntry.PostalCode,
                                AnnualIncome = annualIncome,
                                CreatedAt = DateTime.UtcNow
                            };

            await _unitOfWork.TaxResultRepository.CreateAsync(taxResult);
            await _unitOfWork.SaveToDatabaseAsync();
            return taxResult;
        }

        public async Task<PostalCodeTaxEntry> GetPostalCodeTaxEntryAsync(string postalCode)
        {
            return await _unitOfWork.PostalCodeTaxMapRepository.GetAsync(postalCode);
        }

        public async Task<TaxResult> GetTaxResultAsync(int id)
        {
            return await _unitOfWork.TaxResultRepository.GetAsync(id);
        }
    }
}
