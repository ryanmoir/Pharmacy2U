using Pharmacy2URyanMoir.Models;
using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Pharmacy2UClient
{
    class Client
    {
        HttpClient client;

        public Client()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44318/api/");
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public bool Send(string pathRequest, out HttpResponseMessage response)
        {
            response = new HttpResponseMessage();

            response = client.GetAsync(pathRequest).Result;
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void CurrencyExchangeRequest()
        {
            Console.WriteLine("Please Enter currency wish to convert to");
            var currency = Console.ReadLine();

            Console.WriteLine("Please Enter amount without decimal point");
            var amountStr = Console.ReadLine();

            currency = currency.Trim().ToUpper();
            if ((currency == "USD" || currency == "AUD" || currency == "EUR") && int.TryParse(amountStr, out int amount))
            {
                SendCurrencyExchangeRequest(currency, amount);
            }
            else
            {
                Console.WriteLine("Invalid input");
            }
        }

        public void SendCurrencyExchangeRequest(string currency, int amount)
        {
            Console.WriteLine("Getting exchange rate data");
            Send("ExchangeRates/ExchangeRateStrings/baseCurrency=GBP/convertedCurrency=" + currency, out var response);
            var jsonString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(jsonString);
            var exchangeRate = Newtonsoft.Json.JsonConvert.DeserializeObject<ExchangeRates>(jsonString);

            Console.WriteLine("Getting base currency data");
            Send("Currencies/Currency/arg=" + exchangeRate.BaseCurrecncy, out response);
            jsonString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(jsonString);
            var baseCurrency = Newtonsoft.Json.JsonConvert.DeserializeObject<Currencies>(jsonString);

            Console.WriteLine("Getting conversion currency data");
            Send("Currencies/Currency/arg=" + exchangeRate.ConvertedCurrency, out response);
            jsonString = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(jsonString);
            var ConversionCurrency = Newtonsoft.Json.JsonConvert.DeserializeObject<Currencies>(jsonString);

            float convertedAmount = amount * exchangeRate.ExchangeRate;

            Console.WriteLine(baseCurrency.Symbol + amount + " converted to " + ConversionCurrency.Name + " is " + ConversionCurrency.Symbol + convertedAmount);

            Console.WriteLine();
        }
    }
}
