using flight_planner_net.Models;
using flight_planner_net.Storage;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net
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
    }
}