using FlightPlannerService.Models;
using FlightPlannerService.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerService
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        private readonly FlightStorage _storage;
        public AdminController(FlightStorage storage) 
        { 
            _storage = storage;
        }

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            return NotFound();
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
            if(FlightStorage.DeleteFlight(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [Route("flights")]
        [HttpPost]
        public IActionResult AddFlight(Flight flight)
        {
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

            _storage.AddFlight(flight);

            return Created("", flight);
        }
    }
}
