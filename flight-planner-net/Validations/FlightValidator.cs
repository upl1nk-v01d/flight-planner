using FlightPlanner.Core.Models;
using FluentValidation;

namespace flight_planner_net.Validations
{
    public class FlightValidator : AbstractValidator<Flight>
    {
        public FlightValidator() 
        {
            RuleFor(flight => flight.ArrivalTime).NotEmpty();
            RuleFor(flight => flight.DepartureTime).NotEmpty();
            RuleFor(flight => flight.DepartureTime).Must((flight, departureTime) => 
                DateTime.Parse(departureTime) < DateTime.Parse(flight.ArrivalTime)) 
                .When(flight => !string.IsNullOrEmpty(flight.ArrivalTime) 
                && !string.IsNullOrEmpty(flight.DepartureTime));

            RuleFor(flight => flight.Carrier).NotEmpty();
            RuleFor(flight => flight.From).NotNull();
            RuleFor(flight => flight.To).NotNull();
            RuleFor(flight => flight.From.AirportCode).NotEmpty();
        }
    }
}
