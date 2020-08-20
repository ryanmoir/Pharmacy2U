using System;

namespace Pharmacy2UClient
{
    class Program
    {
        static void Main(string[] args)
        {            
            Client client = new Client();
            while (true)
            {
                client.CurrencyExchangeRequest();
            }
        }
    }
}
