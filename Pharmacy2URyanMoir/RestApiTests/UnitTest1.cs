using NUnit.Framework;
using Pharmacy2URyanMoir.Controllers;
using Pharmacy2URyanMoir.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiTests
{
    public class Tests
    {
        CurrenciesController currenciesController = new CurrenciesController();

        [SetUp]
        public void Setup()
        {
            var utilityController = new UtilityController();
            utilityController.ClearDb();
        }

        [Test]
        public async Task TestAddCurrency()
        {
            List<Currencies> currencies = new List<Currencies>();
            currencies.Add(new Currencies()
            {
                Exponent = 2,
                Symbol = "&",
                Name = "Test1"
            });

            currencies.Add(new Currencies()
            {
                Exponent = 3,
                Symbol = "!",
                Name = "Test2"
            });

            await AddCurrency(currencies);
            Assert.Pass();
        }

        private async Task AddCurrency(List<Currencies> currencies)
        {
            for (int i = 0; i < currencies.Count; i++)
            {
                await currenciesController.PostCurrencies(currencies[i]);

                var returnedCurrency = await currenciesController.GetCurrencies(currencies[i].Name);
                if (currencies[i] != returnedCurrency.Value)
                {
                    Assert.Fail();
                }
            }
        }
    }
}