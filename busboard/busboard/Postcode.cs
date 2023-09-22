public class PostcodesResponse
{
    public int status { get; set; }
    public PostcodesResult result { get; set; }
}

public class PostcodesResult
{
    public double longitude { get; set; }
    public double latitude { get; set; }
}