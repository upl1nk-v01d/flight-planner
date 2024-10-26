namespace flight_planner_net.Models
{
    public class SearchFlightsRequest
    {
        public string From { get; set; }
        public string To { get; set; }
        public string departureDate { get; set; }
    }
}