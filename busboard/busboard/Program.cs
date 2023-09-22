// See https://aka.ms/new-console-template for more information

using RestSharp;

const string baseUrl = "https://api.tfl.gov.uk/StopPoint";
var options = new RestClientOptions(baseUrl);
var client = new RestClient(options);

const string stopCode = "490008660N";

var request = new RestRequest($"/{stopCode}/Arrivals");
var response = await client.GetAsync(request);
Console.WriteLine(response.Content);