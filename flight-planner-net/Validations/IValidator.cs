using FlightPlanner.Core.Models;

namespace flight_planner_net.Validations
{
    public interface IValidator
    {
        bool IsValid(Flight? flight);
    }
}
