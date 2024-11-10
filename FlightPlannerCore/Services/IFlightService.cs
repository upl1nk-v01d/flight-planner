using FlightPlanner.Core.Models;

namespace FlightPlanner.Core.Services
{
    public interface IFlightService : IEntityService<Flight>
    {
        Flight? GetFullFlightByID(int Id);
    }
}
