using RestSharp;

public class TflClient
{
    private static readonly string baseUrl = "https://api.tfl.gov.uk/StopPoint/";
    private RestClientOptions _options;
    private RestClient _client;

    public TflClient()
    {
        _options = new RestClientOptions(baseUrl);
        _client = new RestClient(_options);
    }

    public async Task<ArrivalsResponse> GetArrivals(string stopCode, int numberOfArrivals)
    {
        var request = new RestRequest($"{stopCode}/Arrivals");
        var response = await _client.ExecuteGetAsync<List<Service>>(request);

        return new ArrivalsResponse() { statusCode = (int)response.StatusCode, services = response.Data }.ExtractNextNArrivals(numberOfArrivals);
    }

    public async Task<StopPointsWithinRadiusResponse> GetStopPointsWithinRadius(double lat, double lon)
    {
        var stopTypes = "NaptanPublicBusCoachTram";

        var request = new RestRequest($"?lat={lat}&lon={lon}&stopTypes={stopTypes}");
        var response = await _client.ExecuteGetAsync<StopPointsWithinRadius>(request);

        return new StopPointsWithinRadiusResponse() { statusCode = (int)response.StatusCode, data = response.Data.GetNextNServices(2) };
    }
}