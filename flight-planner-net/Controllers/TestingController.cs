using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightPlannerService
{
    [Route("testing-api")]
    [ApiController]
    public class TestingController(IDBClearingService dBClearingService) : ControllerBase
    {
        private readonly IDBClearingService _dBClearingService = dBClearingService;

        [HttpPost]
        [Route("clear")]

        public IActionResult Clear()
        {
            _dBClearingService.Clear<Airport>();
            _dBClearingService.Clear<Flight>();

            return Ok();
        }
    }
}
