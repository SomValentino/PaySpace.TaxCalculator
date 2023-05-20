using PaySpace.TaxCalculator.WebApp.Models;

namespace PaySpace.TaxCalculator.WebApp.Services
{
    public interface ITaxService
    {
        Task<TaxResponse> GetTaxResponse(TaxModel taxModel);
    }
}
