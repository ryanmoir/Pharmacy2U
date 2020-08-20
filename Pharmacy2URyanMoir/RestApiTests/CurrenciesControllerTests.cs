using NUnit.Framework;
using Pharmacy2URyanMoir.Controllers;
using Pharmacy2URyanMoir.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestApiTests
{
    public class CurrenciesControllerTests
    {
        Pharmacy2URyanMoir.Controllers.CurrenciesController currenciesController = new Pharmacy2URyanMoir.Controllers.CurrenciesController();

        public CurrenciesControllerTests()
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

        [Test]
        public async Task TestEditCurrency()
        {
            var currencies = await currenciesController.GetCurrencies();    
            if (currencies.Value.Count() == 0)
            {
                List<Currencies> currenciesToAdd = new List<Currencies>();
                currenciesToAdd.Add(new Currencies()
                {
                    Exponent = 2,
                    Symbol = "&",
                    Name = "Test1"
                });
                await AddCurrency(currenciesToAdd);
                currencies = await currenciesController.GetCurrencies();
            }

            var currencyToEdit = currencies.Value.First();
            currencyToEdit.Name = currencyToEdit.Name + "Edited";
            await currenciesController.UpdateCurrency(currencyToEdit);
            var returnedCurrency = await currenciesController.GetCurrencies(currencyToEdit.Name);
            if (currencyToEdit != returnedCurrency.Value)
            {
                Assert.Fail();
            }
            else
            {
                Assert.Pass();
            }

        }

        [Test]
        public async Task TestDeleteCurrency()
        {
            var currencies = await currenciesController.GetCurrencies();
            if (currencies.Value.Count() == 0)
            {
                List<Currencies> currenciesToAdd = new List<Currencies>();
                currenciesToAdd.Add(new Currencies()
                {
                    Exponent = 2,
                    Symbol = "&",
                    Name = "TestToDelete"
                });
                await AddCurrency(currenciesToAdd);

                currencies = await currenciesController.GetCurrencies();
            }

            await currenciesController.DeleteCurrencies(currencies.Value.First().Id);
            var test  = await currenciesController.GetCurrencies(currencies.Value.First().Id);
            if(test.Value == null)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        private async Task AddCurrency(List<Currencies> currencies)
        {
            for (int i = 0; i < currencies.Count; i++)
            {
                await currenciesController.AddCurrency(currencies[i]);

                var returnedCurrency = await currenciesController.GetCurrencies(currencies[i].Name);
                if (currencies[i] != returnedCurrency.Value)
                {
                    Assert.Fail();
                }
            }
        }
    }
}