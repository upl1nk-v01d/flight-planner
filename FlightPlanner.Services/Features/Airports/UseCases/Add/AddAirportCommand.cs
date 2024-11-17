using FlightPlanner.Core;
using FlightPlanner.Services.Features.Airports.Models;
using MediatR;

namespace FlightPlanner.Services.Features.Airports.UseCases.Add
{
    public class AddAirportCommand
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Airport { get; set; }
    }
}
