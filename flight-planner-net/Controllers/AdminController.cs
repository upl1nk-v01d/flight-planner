using FlightPlannerService.Database;
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
            if (_storage.CheckDuplicates(flight))
            {
                return Conflict();
            }

            if(FlightStorage.CheckWrongValues(flight))
            {
                return BadRequest();
            }

            if(FlightStorage.CheckSameAirport(flight))
            {
                return BadRequest();
            }

            if(FlightStorage.CheckStrangeDates(flight))
            {
                return BadRequest();
            }

            _storage.AddFlight(flight);

            return Created("", flight);
        }
    }
}
