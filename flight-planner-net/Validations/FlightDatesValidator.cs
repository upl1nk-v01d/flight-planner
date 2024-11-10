using FlightPlanner.Core.Models;

namespace flight_planner_net.Validations
{
    public class FlightDatesValidator : IValidator
    {
        public bool IsValid(Flight? flight)
        {
            if (!DateTime.TryParse(flight?.DepartureTime, out var departureTime))
            {
                return false;
            }

            if (!DateTime.TryParse(flight?.ArrivalTime, out var arrivalTime))
            {
                return false;
            }

            return arrivalTime > departureTime;
        }
    }
}
