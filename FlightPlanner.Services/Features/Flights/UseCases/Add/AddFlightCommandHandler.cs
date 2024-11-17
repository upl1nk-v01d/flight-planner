using FlightPlanner.Core;
using FlightPlanner.Services.Features.Flights.Models;
using MediatR;

namespace FlightPlanner.Services.Features.Flights.UseCases.Add
{
    public class AddFlightCommandHandler : IRequestHandler<AddAirportCommand, Result<FlightViewModel>>
    {
        Task<Result<FlightViewModel>> IRequestHandler<AddAirportCommand, Result<FlightViewModel>>.Handle(
            AddAirportCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
