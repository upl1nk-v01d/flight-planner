namespace FlightPlannerService.Models
{
    public class SearchItems
    {
        public string[] Items { get; set; }
        public int Page { get; set; }
        public int TotalItems { get; set; }

        public SearchItems()
        {
            Items = [];
            Page = 0;
            TotalItems = 0;
        }
    }
}