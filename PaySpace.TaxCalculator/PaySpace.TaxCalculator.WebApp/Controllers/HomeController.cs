using Microsoft.AspNetCore.Mvc;
using PaySpace.TaxCalculator.WebApp.Models;
using PaySpace.TaxCalculator.WebApp.Services;
using System.Diagnostics;
using System.Globalization;


namespace PaySpace.TaxCalculator.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITaxService _taxService;

        public HomeController(ILogger<HomeController> logger, ITaxService taxService)
        {
            _logger = logger;
            _taxService = taxService;
        }

        public IActionResult Index()
        {
            return View(new TaxModel());
        }
        [HttpPost]
        public async Task<IActionResult> Index(TaxModel taxModel)
        {
            try
            {
                if(!ModelState.IsValid)
                {
                    return View(taxModel);
                }
                var taxResponse = await _taxService.GetTaxResponse(taxModel);
                taxModel.SuccessMessage = $"Tax to be paid is {taxResponse.Tax.ToString("C", new CultureInfo("en-ZA"))}";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                taxModel.ErrorMessage = "Something went wrong. Kindly try again";
            }
            return View(taxModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}