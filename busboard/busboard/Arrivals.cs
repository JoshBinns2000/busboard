public class ArrivalsResponse
{
    public int statusCode;
    public List<Service>? services;

    public ArrivalsResponse ExtractNextNArrivals(int numberOfArrivals)
    {
        if (services == null)
        {
            return this;
        }

        services = services.OrderBy(x => x.timeToStation).ToList();
        
        try
        {
            services.RemoveRange(numberOfArrivals, services.Count - numberOfArrivals);
        }
        catch (ArgumentOutOfRangeException)
        {
            // log that response contains fewer than 5 arrivals
        }

        return this;
    }
}