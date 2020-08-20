using NUnit.Framework;
using Pharmacy2URyanMoir.Controllers;
using Pharmacy2URyanMoir.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiTests
{
    class ExchangeRateControllerTests : BaseTests
    {
        ExchangeRatesController exchangeRateController = new ExchangeRatesController();

        [SetUp]
        public void Init()
        {
            ClearDb();
            Task SetUpCurrenciesT = SetUpCurrencies();
            SetUpCurrenciesT.Wait();
        }

        private async Task SetUpCurrencies()
        {
            List<Currencies> currencies = new List<Currencies>();
            currencies.Add(new Currencies()
            {
                Exponent = 2,
                Symbol = "£",
                Name = "GBP"
            });
            currencies.Add(new Currencies()
            {
                Exponent = 2,
                Symbol = "$",
                Name = "USD"
            });
            currencies.Add(new Currencies()
            {
                Exponent = 2,
                Symbol = "$",
                Name = "AUD"
            });
            currencies.Add(new Currencies()
            {
                Exponent = 2,
                Symbol = "€",
                Name = "EUR"
            });

            await AddCurrency(currencies);
        }

        [Test]
        public void TestAddExchangeRate()
        {

        }
    }
}
