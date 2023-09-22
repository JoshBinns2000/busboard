using RestSharp;

public class PostcodesClient
{
    private static readonly string baseUrl = "https://api.postcodes.io/postcodes/";
    private RestClientOptions _options;
    private RestClient _client;

    public PostcodesClient()
    {
        _options = new RestClientOptions(baseUrl);
        _client = new RestClient(_options);
    }

    public async Task<PostcodesResponse?> GetPostcodeInformation(string postcode)
    {
        postcode = postcode.Replace(" ", "");
        var request = new RestRequest($"{postcode}");
        var response = await _client.ExecuteGetAsync<PostcodesResponse>(request);

        return response.Data;
    }
}