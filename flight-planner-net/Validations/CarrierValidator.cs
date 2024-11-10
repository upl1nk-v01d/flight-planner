using FlightPlanner.Core.Models;

namespace flight_planner_net.Validations
{
    public class CarrierValidator : IValidator
    {
        public bool IsValid(Flight? flight)
        {
            return !string.IsNullOrEmpty(flight?.Carrier);
        }
    }
}
