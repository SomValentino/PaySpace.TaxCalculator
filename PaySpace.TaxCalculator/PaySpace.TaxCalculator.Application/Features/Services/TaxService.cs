using Microsoft.Extensions.Logging;
using PaySpace.TaxCalculator.Application.Contracts.Processors;
using PaySpace.TaxCalculator.Application.Contracts.Repository;
using PaySpace.TaxCalculator.Application.Contracts.Services;
using PaySpace.TaxCalculator.Application.Exceptions;
using PaySpace.TaxCalculator.Domain.Entities;

namespace PaySpace.TaxCalculator.Application.Features.Services
{
    public class TaxService : ITaxService
    {
        private readonly ITaxProcessorFactory _taxProcessorFactory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<TaxService> _logger;

        public TaxService(
            ITaxProcessorFactory taxProcessorFactory,
            IUnitOfWork unitOfWork,
            ILogger<TaxService> logger)
        {
            _taxProcessorFactory = taxProcessorFactory;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<TaxResult> CalculateTaxAsync(PostalCodeTaxEntry postalCodeTaxEntry, decimal annualIncome)
        {
            _logger.LogInformation("Getting tax processor for postal code: {code}", postalCodeTaxEntry.PostalCode);
            var taxProcessor = _taxProcessorFactory.GetTaxProcessorInstance(postalCodeTaxEntry.TaxCalculationType);

            if (taxProcessor == null)
            {
                throw new TaxProcessorNotFoundException($"No tax processor found for tax calculation type {postalCodeTaxEntry.TaxCalculationType}");
            }

            _logger.LogInformation("Obtained tax processor of type: {type}", taxProcessor.GetType());

            _logger.LogInformation("Calculating tax based on type: {type}", postalCodeTaxEntry.TaxCalculationType.ToString());
            var tax = taxProcessor.CalculateTax(annualIncome, postalCodeTaxEntry);
            _logger.LogInformation("Obtained tax with value: {value}", tax);

            var taxResult = new TaxResult
            {
                Tax = tax,
                PostalCode = postalCodeTaxEntry.PostalCode,
                AnnualIncome = annualIncome,
                CreatedAt = DateTime.UtcNow
            };

            _logger.LogInformation("Saving tax result to database");
            await _unitOfWork.TaxResultRepository.CreateAsync(taxResult);
            await _unitOfWork.SaveToDatabaseAsync();
            _logger.LogInformation("Saved tax result to database");

            return taxResult;
        }

        public async Task<PostalCodeTaxEntry> GetPostalCodeTaxEntryAsync(string postalCode)
        {
            return await _unitOfWork.PostalCodeTaxMapRepository.GetAsync(postalCode).ConfigureAwait(false);
        }

        public async Task<TaxResult> GetTaxResultAsync(int id)
        {
            return await _unitOfWork.TaxResultRepository.GetAsync(id).ConfigureAwait(false);
        }
    }

}