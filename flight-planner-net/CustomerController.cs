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
        [HttpGet]
        [Route("airports")]
        public IActionResult SearchAirports(string? name)
        {
            Console.WriteLine(name);
            return Ok();
        }
    }
}