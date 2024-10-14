using flight_planner_net.Models;
using flight_planner_net.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController : ControllerBase
    {
        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            return NotFound();
        }

        [HttpPost]
        [Route("flights")]
        public IActionResult AddFlight(Flight flight)
        {
            FlightStorage.AddFlight(flight);

            return Created("", flight);
        }
    }
}
