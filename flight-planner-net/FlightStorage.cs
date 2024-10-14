using flight_planner_net.Models;

namespace flight_planner_net.Storage
{
   public static class FlightStorage
   {
      private static List<Flight> _flights = new List<Flight>();
      private static int _id = 0;

      public static Flight AddFlight(Flight flight)
      {
         flight.Id = ++_id;
         _flights.Add(flight);

         return flight;
      }

      public static void ClearFlights()
      {
         _flights.Clear();
      }
   }
}