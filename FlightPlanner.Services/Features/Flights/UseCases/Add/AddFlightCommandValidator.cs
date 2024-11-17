using FluentValidation;

namespace FlightPlanner.Services.Features.Flights.UseCases.Add
{
    public class AddFlightCommandValidator : AbstractValidator<AddFlightCommand>
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
