public class Service
{
    public string vehicleId { get; set; }
    public DateTime expectedArrival { get; set; }
    public string lineName { get; set; }
    public string lineId { get; set; }
    public string destinationName { get; set; }
    public int timeToStation { get; set; }

    public void DisplayService()
    {
        Console.WriteLine("Service {0} arriving at {1:h:mm tt} (service destination: '{2}')",
            lineId,
            expectedArrival,
            destinationName
        );
    }
}