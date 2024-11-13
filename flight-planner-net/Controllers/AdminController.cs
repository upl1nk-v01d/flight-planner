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
        IEnumerable<IValidator> validators    
    ) : ControllerBase
    {
        private readonly IFlightService _flightService = flightService;
        private readonly IEnumerable<IValidator> _validators = validators;
        private readonly IValidator<Flight> _validator;
        private readonly IMapper _mapper;
        /*public AdminController(FlightStorage storage) 
        { 
            _storage = storage;
        }*/

        [Route("flights/{id}")]
        [HttpGet]
        public IActionResult GetFlight(int id)
        {
            //var flight = _storage.FindFlightById(id);
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
            //var result = _flightService.Delete(id);
            /*
            if (result != null)
            {
                return Ok();
            }
            */
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

            var result = _flightService.Create(flight);
            var response = _mapper.Map<FlightResponse>(flight);

            response.Id = result.Entity.Id;


            /*
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
            */
            //_storage.AddFlight(flight);

            return Created("", flight);
        }       
    }
}
