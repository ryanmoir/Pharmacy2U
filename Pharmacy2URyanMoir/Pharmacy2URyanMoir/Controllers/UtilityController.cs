using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Pharmacy2URyanMoir.Data;
using Pharmacy2URyanMoir.Models;

namespace Pharmacy2URyanMoir.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UtilityController : ControllerBase
    {
        [HttpDelete]
        [ActionName("Clear")]
        //https://localhost:44318/api/Utility/Clear
        public void ClearDb()
        {
            using (var context = new DbSetContext())
            {
                context.Currencies.RemoveRange(context.Currencies);
                context.ExchangeRates.RemoveRange(context.ExchangeRates);
                context.Logs.RemoveRange(context.Logs);
                context.SaveChanges();
            }
        }

        [HttpPost]
        [ActionName("DummyData")]
        //https://localhost:44318/api/Utility/DummyData
        public async Task<IActionResult> FillWithDummyData()
        {
            ClearDb();

            var currenciesController = new CurrenciesController();
            await currenciesController.AddCurrency(new Currencies
            {
                Exponent = 2,
                Symbol = "£",
                Name = "GBP"
            });
            await currenciesController.AddCurrency(new Currencies
            {
                Exponent = 2,
                Symbol = "$",
                Name = "USD"
            });
            await currenciesController.AddCurrency(new Currencies
            {
                Exponent = 2,
                Symbol = "$",
                Name = "AUD"
            });
            await currenciesController.AddCurrency(new Currencies
            {
                Exponent = 2,
                Symbol = "€",
                Name = "EUR"
            });

            ExchangeRatesController exchangeRateController = new ExchangeRatesController();
            await exchangeRateController.AddExchangeRate("GBP", "USD", 1);
            await exchangeRateController.AddExchangeRate("GBP", "AUD", (float)1.2);
            await exchangeRateController.AddExchangeRate("GBP", "EUR", (float)1.9);

            return NoContent();
        }
    }
}
