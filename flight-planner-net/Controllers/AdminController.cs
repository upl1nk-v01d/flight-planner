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
        private static readonly object _locker = new object();

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
            lock (_locker)
            {
                if (FlightStorage.DeleteFlight(id))
                {
                    return Ok();
                }

                return NotFound();
            }
            
        }

        [Route("flights")]
        [HttpPost]
        public IActionResult AddFlight(Flight flight)
        {
            lock (_locker)
            {
                if (FlightStorage.CheckDuplicates(flight))
                {
                    return Conflict();
                }

                if (FlightStorage.CheckWrongValues(flight) || 
                    FlightStorage.CheckSameAirport(flight) || 
                    FlightStorage.CheckStrangeDates(flight))
                {
                    return BadRequest();
                }

                FlightStorage.AddFlight(flight);

                return Created("", flight);
            }
        }
    }
}
