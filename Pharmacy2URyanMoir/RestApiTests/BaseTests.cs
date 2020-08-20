using NUnit.Framework;
using Pharmacy2URyanMoir.Controllers;
using Pharmacy2URyanMoir.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace RestApiTests
{
    class BaseTests
    {
        public void ClearDb()
        {
            var utilityController = new UtilityController();
            utilityController.ClearDb();
        }

        public async Task AddCurrency(List<Currencies> currencies)
        {
            var currenciesController = new CurrenciesController();
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
