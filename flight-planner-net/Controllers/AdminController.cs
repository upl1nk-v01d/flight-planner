using flight_planner_net.Models;
using flight_planner_net.Validations;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FlightPlannerService
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController(IFlightService flightService) : ControllerBase
    {
        private readonly IFlightService _flightService = flightService;
        private readonly IEnumerable<IValidator> _validators;
        /*public AdminController(FlightStorage storage) 
        { 
            _storage = storage;
        }*/

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            //var flight = _storage.FindFlightById(id);
            var result = _flightService.GetFullFlightByID(id);

            if (result == null)
            {
                return NotFound();
            }

            var response = GetFromFlight(result);

            return Ok(result);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            //var result = _flightService.Delete(id);
            /*
            if (result != null)
            {
                return Ok();
            }
            */
            return NotFound();
        }

        [Route("flights")]
        [HttpPost]
        public IActionResult AddFlight(FlightRequest request)
        {
            var flight = GetFromRequest(request);

            if (!_validators.All(validator => validator.IsValid(flight)))
            {
                return BadRequest();
            }

            var result = _flightService.Create(flight);
            var response = GetFromFlight(flight);

            response.Id = result.Entity.Id;


            /*
            if (_storage.FlightExists(flight))
            {
                return Conflict();
            }

            if(FlightStorage.IsValidFlight(flight))
            {
                return BadRequest();
            }

            if(FlightStorage.IsSameAirport(flight))
            {
                return BadRequest();
            }

            if(FlightStorage.HaveValidDates(flight))
            {
                return BadRequest();
            }
            */
            //_storage.AddFlight(flight);

            return Created("", flight);
        }

        private Flight GetFromRequest(FlightRequest request)
        {
            return new Flight
            {
                ArrivalTime = request.ArrivalTime,
                Carrier = request.Carrier,
                DepartureTime = request.DepartureTime,
                From = new Airport
                {
                    AirportCode = request.From.Airport,
                    City = request.From.City,
                    Country = request.From.Country,
                },
                To = new Airport
                {
                    AirportCode = request.To.Airport,
                    City = request.To.City,
                    Country = request.To.Country
                }
            };
        }

        private FlightResponse GetFromFlight(Flight flight)
        {
            return new FlightResponse
            {
                Id = flight.Id,
                ArrivalTime = flight.ArrivalTime,
                Carrier = flight.Carrier,
                DepartureTime = flight.DepartureTime,
                From = new AirportResponse
                {
                    Airport = flight.From.AirportCode,
                    City = flight.From.City,
                    Country = flight.From.Country,
                },
                To = new AirportResponse
                {
                    Airport = flight.To.AirportCode,
                    City = flight.To.City,
                    Country = flight.To.Country
                }
            };
        }
    }
}
