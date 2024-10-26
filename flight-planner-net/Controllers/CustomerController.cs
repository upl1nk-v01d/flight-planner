using FlightPlannerService.Models;
using FlightPlannerService.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerService
{
    [Route("api")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        [Route("airports")]
        [HttpGet("{search}")]
        public IActionResult SearchAirports([FromQuery] string search = "")
        {
            var list = FlightStorage.SearchAirports(search);

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

            object flights = FlightStorage.SearchFlights(request);

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
            var found = FlightStorage.FindFlightById(id);

            if(found == null)
            {
                return NotFound(0);
            }
            else
            {
                return Ok(found);
            }
        }
    }
}