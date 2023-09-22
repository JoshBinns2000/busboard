public class StopPointsWithinRadiusResponse
{
    public int statusCode;
    public StopPointsWithinRadius? data;
}

public class StopPointsWithinRadius
{
    public List<StopPoint> stopPoints { get; set; }

    public StopPointsWithinRadius GetNextNServices(int numberOfStopPoints)
    {
        stopPoints = stopPoints.OrderBy(x => x.distance).ToList();
        
        try
        {
            stopPoints.RemoveRange(numberOfStopPoints, stopPoints.Count - numberOfStopPoints);
        }
        catch (ArgumentOutOfRangeException)
        {
            // log that response contains fewer than 5 arrivals
        }
        
        return this;
    }
}

public class StopPoint
{
    public double lat { get; set; }
    public double lon { get; set; }
    public double distance { get; set; }
    public string stopType { get; set; }
    public string stopLetter { get; set; }
    public string naptanId { get; set; }
}