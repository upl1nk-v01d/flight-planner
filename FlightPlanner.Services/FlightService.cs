using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FlightPlanner.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightPlanner.Services
{
    public class FlightService : EntityService<Flight>, IFlightService
    {
        public FlightService(FlightPlannerDbContext context) : base(context)
        {
        }

        public Flight? GetFullFlightByID(int Id)
        {
             return _context.Flights
                .Include(flight => flight.From)
                .Include(flight => flight.To)
                .SingleOrDefault(flight => flight.Id == Id);
        }
    }
}
