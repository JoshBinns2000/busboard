// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new TflClient();
            
            while (true)
            {
                Console.WriteLine("What station would you like to check?");
                var stopCode = Console.ReadLine();

                var response = await client.GetArrivals(stopCode);
                
                if (response.statusCode != 200 || response.services == null)
                {
                    Console.WriteLine("Uhoh! (Error code {0})", response.statusCode);
                    continue;
                }

                try
                {
                    response.services.RemoveRange(5, response.services.Count - 5);
                }
                catch (ArgumentOutOfRangeException)
                {
                    // log that response contains fewer than 5 arrivals
                }
            
                foreach (var service in response.services)
                {
                    service.DisplayService();
                }
            }
        }
    }
}