using System;

namespace Pharmacy2UClient
{
    class Program
    {
        static void Main(string[] args)
        {            
            Client client = new Client();
            string input = "";
            while (input != "4")
            {
                Console.WriteLine("Please enter option");
                Console.WriteLine("1-convert currency");
                Console.WriteLine("2-get logs");
                Console.WriteLine("3-fill db with dummy data");
                Console.WriteLine("4-type exit to exit");
                input = Console.ReadLine();
                
                if (int.TryParse(input, out var choice))
                {
                    if (choice == 1)
                    {
                        client.CurrencyExchangeRequest();
                    }
                    else if (choice == 2)
                    {
                        client.GetLogs();
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input");
                }
            }
        }
    }
}
