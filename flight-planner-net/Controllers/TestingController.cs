using FlightPlannerService.Storage;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerService
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
