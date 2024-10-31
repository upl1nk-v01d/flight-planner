using FlightPlannerService.Storage;

namespace FlightPlannerService.Models
{
    public class SearchItems
    {
        public string[] items { get; set; }
        public int page { get; set; }
        public int totalItems { get; set; }

        public SearchItems()
        {
            items = [];
            page = 0;
            totalItems = 0;
        }
    }
}