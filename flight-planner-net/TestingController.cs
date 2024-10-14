using flight_planner_net.Storage;
using Microsoft.AspNetCore.Mvc;

namespace flight_planner_net
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController : ControllerBase
    {
        [HttpPost]
        [Route("clear")]

        public IActionResult Clear()
        {
            FlightStorage.ClearFlights();
            
            return Ok();
        }
    }
}
