// See https://aka.ms/new-console-template for more information

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Slight bug where async operations are out of order, but it's not worth fixing on a Friday

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var tflClient = new TflClient();
            var postcodesClient = new PostcodesClient();
            
            while (true)
            {
                Console.WriteLine("Give me a postcode please :)");
                var postcode = Console.ReadLine();

                if (postcode == null)
                {
                    Console.WriteLine("Why no provide postcode? Try again smh");
                    continue;
                }

                var postcodesResponse = await postcodesClient.GetPostcodeInformation(postcode);

                if (postcodesResponse.result == null || postcodesResponse.status != 200)
                {
                    Console.WriteLine("Uhoh (postcode edition)! (Error code {0})", postcodesResponse.status);
                    continue;
                }

                var stopPointsWithinRadiusResponse = await tflClient.GetStopPointsWithinRadius(
                    postcodesResponse.result.latitude,
                    postcodesResponse.result.longitude
                );
                
                if (stopPointsWithinRadiusResponse.statusCode != 200 || stopPointsWithinRadiusResponse.data == null)
                {
                    Console.WriteLine("Uhoh (TfL edition)! (Error code {0})", stopPointsWithinRadiusResponse.statusCode);
                    continue;
                }
                
                Console.WriteLine($"Nearest Bus Stop: {stopPointsWithinRadiusResponse.data.stopPoints[0].stopLetter}");
                Console.WriteLine("Next two scheduled services:");

                var firstArrivalsResponse =
                    await tflClient.GetArrivals(stopPointsWithinRadiusResponse.data.stopPoints[0].naptanId, 2);
                
                if (firstArrivalsResponse.statusCode != 200 || firstArrivalsResponse.services == null)
                {
                    Console.WriteLine("Uhoh (TfL edition)! (Error code {0})", stopPointsWithinRadiusResponse.statusCode);
                    continue;
                }

                foreach (var service in firstArrivalsResponse.services)
                {
                    service.DisplayService();
                }
                
                Console.WriteLine($"Nearest Bus Stop: {stopPointsWithinRadiusResponse.data.stopPoints[1].stopLetter}");
                Console.WriteLine("Next two scheduled services:");

                var secondArrivalsResponse =
                    await tflClient.GetArrivals(stopPointsWithinRadiusResponse.data.stopPoints[1].naptanId, 2);
                
                if (secondArrivalsResponse.statusCode != 200 || secondArrivalsResponse.services == null)
                {
                    Console.WriteLine("Uhoh (TfL edition)! (Error code {0})", stopPointsWithinRadiusResponse.statusCode);
                    continue;
                }

                foreach (var service in secondArrivalsResponse.services)
                {
                    service.DisplayService();
                }

                // foreach (var service in tflResponse.services)
                // {
                //     service.DisplayService();
                // }
            }
        }
    }
}