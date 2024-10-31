using FlightPlannerService.Models;
using FlightPlannerService.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerService
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly FlightStorage _storage;
        public CustomerController(FlightStorage storage)
        {
            _storage = storage;
        }

        [Route("airports")]
        [HttpGet("{search}")]
        public IActionResult SearchAirports([FromQuery] string search = "")
        {
            var list = _storage.SearchAirports(search);

            return Ok(list);
        }

        [Route("flights/search")]
        [HttpPost("request")]
        public IActionResult SearchFlights(SearchFlightsRequest request)
        {
            if(FlightStorage.CheckRequestErrors(request))
            {
                return BadRequest();
            }

            object flights = _storage.SearchFlights(request);

            if(flights == null)
            {
                return NotFound();
            }

            return Ok(flights);
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult FindFlightById(int id)
        {
            var found = _storage.FindFlightById(id);

            if(found == null)
            {
                return NotFound();
            }
            
            return Ok(found);
        }
    }
}