using FlightPlannerService.Database;
using FlightPlannerService.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightPlannerService.Storage
{
    public class FlightStorage
    {
        private readonly FlightPlannerDbContext _context;
        public FlightStorage(FlightPlannerDbContext context)
        {
            _context = context;
        }

        public Flight AddFlight(Flight flight)
        {
            _context.Flights.Add(flight);
            _context.SaveChanges();

            return flight;
        }

        public void ClearFlights()
        {
            _context.Flights.RemoveRange(_context.Flights);
            _context.Airports.RemoveRange(_context.Airports);
            _context.SaveChanges();
        }

        public bool CheckDuplicates(Flight flight)
        {
            var _flights = _context.Flights
                .Include(_flight => _flight.To)
                .Include(_flight => _flight.From)
                .Where(_flight => _flight.From.City == flight.From.City);

            foreach (var _flight in _flights)
            {
                if (_flight.ArrivalTime == flight.ArrivalTime)
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckWrongValues(Flight flight)
        {
            foreach (var p in flight.GetType().GetProperties())
            {
                if (p.GetValue(flight) == null)
                {
                    return true;
                }
                else if (flight.Carrier == "")
                {
                    return true;
                }
                else if
                (
                   flight.From.Country == null
                   || flight.From.City == null
                   || flight.From.AirportCode == null
                )
                {
                    return true;
                }
                else if
                (
                   flight.To.Country == null
                   || flight.To.City == null
                   || flight.To.AirportCode == null
                )
                {
                    return true;
                }
                else if
                (
                   flight.From.Country == ""
                   || flight.From.City == ""
                   || flight.From.AirportCode == ""
                )
                {
                    return true;
                }
                else if
                (
                   flight.To.Country == ""
                   || flight.To.City == ""
                   || flight.To.AirportCode == ""
                )
                {
                    return true;
                }
            }

            return false;
        }

        public static bool CheckSameAirport(Flight flight)
        {
            if (flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
            {
                return true;
            }

            return false;
        }

        public static bool CheckStrangeDates(Flight flight)
        {
            var departure = DateTime.Parse(flight.DepartureTime);
            var arrival = DateTime.Parse(flight.ArrivalTime);

            if (arrival <= departure)
            {
                return true;
            }

            return false;
        }

        public static bool DeleteFlight(int Id)
        {
            if (Id > -1)
            {
                return true;
            }

            return false;
        }

        public List<Airport> SearchAirports(string search = "")
        {
            search = search.Trim().ToLower();

            var list = new List<Airport>();
            var _airports = _context.Airports.ToList();
            
            foreach (var _airport in _airports)
            {
                string country = _airport.Country.ToLower();
                string city = _airport.City.ToLower();
                string airportCode = _airport.AirportCode.ToLower();

                if (country.Contains(search)
                   || city.Contains(search)
                   || airportCode.Contains(search)
                )
                {
                    list.Add(_airport);
                }
            }

            return list;
        }

        public SearchItems SearchFlights(SearchFlightsRequest search)
        {
            var items = new SearchItems();

            var _flights = _context.Flights
                .Include(flight => flight.To)
                .Include(flight => flight.From)
                .Where(flight => flight.From.AirportCode.Contains(search.From));

            foreach (var flight in _flights)
            {
                if (flight.From.AirportCode == search.From)
                {
                    items.totalItems += 1;
                    return items;
                }
            }

            return items;
        }

        public static bool CheckRequestErrors(SearchFlightsRequest request)
        {
            if (request.From == null)
                return true;
            if (request.To == null)
                return true;
            if (request.departureDate == null)
                return true;
            if (request.From == request.To)
                return true;

            return false;
        }

        public Flight FindFlightById(int id)
        {
            var found = _context.Flights
                .Include(flight => flight.To)
                .Include(flight => flight.From)
                .FirstOrDefault(flight => flight.Id == id);

            return found;
        }
    }
}