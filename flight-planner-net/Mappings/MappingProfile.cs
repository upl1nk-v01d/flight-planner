using AutoMapper;
using flight_planner_net.Models;
using FlightPlanner.Core.Models;

namespace flight_planner_net.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<FlightRequest, Flight>();
            CreateMap<Flight, FlightResponse>();

            CreateMap<AirportRequest, Airport>()
            .ForMember
            (
                airport => airport.AirportCode, 
                options => options.MapFrom(request => request.Airport)
            );

            CreateMap<Airport, AirportResponse>()
            .ForMember
            (
                airport => airport.Airport,
                options => options.MapFrom(request => request.AirportCode)
            );
        }
    }
}
