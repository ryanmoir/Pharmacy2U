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
        public async Task TestAddExchangeRateStrings()
        {
            await exchangeRateController.AddExchangeRate("GBP","USD",1);

            var currenciesController = new CurrenciesController();
            var gbp = await currenciesController.GetCurrencies("GBP");
            var usd = await currenciesController.GetCurrencies("USD");
            if (gbp.Value == null || usd.Value == null)
            {
                Assert.Fail();
            }

            var exchangeRate =  await exchangeRateController.GetExchangeRate(gbp.Value.Id, usd.Value.Id);
            if (exchangeRate.Value != null && exchangeRate.Value.BaseCurrecncy == gbp.Value.Id && exchangeRate.Value.ConvertedCurrency == usd.Value.Id)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task TestAddExchangeRateIds()
        {
            var currenciesController = new CurrenciesController();
            var gbp = await currenciesController.GetCurrencies("GBP");
            var usd = await currenciesController.GetCurrencies("USD");
            if (gbp.Value == null || usd.Value == null)
            {
                Assert.Fail();
            }

            var exchangeRateToSend = new ExchangeRates {
                BaseCurrecncy = gbp.Value.Id,
                ConvertedCurrency = usd.Value.Id,
                ExchangeRate = 1
            };
            await exchangeRateController.AddExchangeRate(exchangeRateToSend);

            var exchangeRate = await exchangeRateController.GetExchangeRate(gbp.Value.Id, usd.Value.Id);
            if (exchangeRate.Value != null && exchangeRate.Value == exchangeRateToSend)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        [Test]
        public async Task TestEditExchangeRate()
        {
            var currenciesController = new CurrenciesController();
            var gbp = await currenciesController.GetCurrencies("GBP");
            var usd = await currenciesController.GetCurrencies("USD");
            if (gbp.Value == null || usd.Value == null)
            {
                Assert.Fail();
            }

            var exchangeRateToSend = new ExchangeRates
            {
                BaseCurrecncy = gbp.Value.Id,
                ConvertedCurrency = usd.Value.Id,
                ExchangeRate = 1
            };
            await exchangeRateController.AddExchangeRate(exchangeRateToSend);

            var exchangeRate = await exchangeRateController.GetExchangeRate(gbp.Value.Id, usd.Value.Id);
            if (exchangeRate.Value != null)
            {
                exchangeRate.Value.ExchangeRate = 2;

                await exchangeRateController.UpdateExchangeRate(exchangeRate.Value.Id, exchangeRate.Value);
                var exchangeRatePostEdit = await exchangeRateController.GetExchangeRate(exchangeRate.Value.Id);
                if(exchangeRatePostEdit.Value  != null && exchangeRate.Value == exchangeRatePostEdit.Value)
                {
                    Assert.Pass();
                }
            }
            Assert.Fail();
        }
    }
}
