using AutoMapper;
using flight_planner_net.Models;
using FlightPlanner.Core.Models;
using FlightPlanner.Core.Services;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IValidator = flight_planner_net.Validations.IValidator;

namespace FlightPlannerService
{
    [Route("admin-api")]
    [ApiController]
    [Authorize]
    public class AdminController
    (
        IFlightService flightService,
        IEnumerable<IValidator> validators,
        IValidator<Flight> validator,
        IMapper mapper
    ) : ControllerBase
    {
        private readonly IFlightService _flightService = flightService;
        private readonly IEnumerable<IValidator> _validators = validators;
        private readonly IValidator<Flight> _validator = validator;
        private readonly IMapper _mapper = mapper;

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            var result = _flightService.GetFullFlightByID(id);

            if (result == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<FlightResponse>(result);

            return Ok(result);
        }

        [Route("flights/{id}")]
        [HttpDelete]
        public IActionResult DeleteFlight(int id)
        {
           
            return NotFound();
        }

        [Route("flights")]
        [HttpPost]
        public IActionResult AddFlight(FlightRequest request)
        {
            var flight = _mapper.Map<Flight>(request);

            var validationResult = _validator.Validate(flight);

            if (!validationResult.IsValid)
            {
                return BadRequest();
            }

            if (!_validators.All(validator => validator.IsValid(flight)))
            {
                return BadRequest();
            }
            /*
            if (!validationResult.IsUnique)
            {
                return Conflict();
            }
            */

            var result = _flightService.Create(flight);
            var response = _mapper.Map<FlightResponse>(flight);

            response.Id = result.Entity.Id;

            return Created("", response);
        }       
    }
}
