using RestSharp;

public class TflClient
{
    private static readonly string baseUrl = "https://api.tfl.gov.uk/StopPoint";
    private RestClientOptions _options;
    private RestClient _client;

    public TflClient()
    {
        _options = new RestClientOptions(baseUrl);
        _client = new RestClient(_options);
    }

    public async Task<ArrivalsResponse> GetArrivals(string stopCode)
    {
        var request = new RestRequest($"/{stopCode}/Arrivals");
        var response = await _client.ExecuteGetAsync<List<Service>>(request);

        return new ArrivalsResponse() { services = response.Data, statusCode = (int)response.StatusCode };
    }
}

public class ArrivalsResponse
{
    public int statusCode;
    public List<Service>? services;
}