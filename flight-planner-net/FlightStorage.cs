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

      public static bool CheckDuplicates(Flight flight)
      {
         if(_flights.Any(f => f.DepartureTime == flight.DepartureTime)
            && _flights.Any(f => f.From.AirportCode == flight.From.AirportCode))
         {
            return true;
         }
         
         return false;
      }

      public static bool CheckWrongValues(Flight flight)
      {
         foreach(var p in flight.GetType().GetProperties())
         {
            if(p.GetValue(flight) == null)
            {
               return true;
            }
            else if(flight.Carrier == "")
            {
               return true;
            }
            else if(flight.From.Country == null || flight.From.City == null 
               || flight.From.AirportCode == null)
            {
               return true;
            }
            else if(flight.To.Country == null || flight.To.City == null 
               || flight.To.AirportCode == null)
            {
               return true;
            }
            else if(flight.From.Country == "" || flight.From.City == "" 
               || flight.From.AirportCode == "")
            {
               return true;
            }
            else if(flight.To.Country == "" || flight.To.City == "" 
               || flight.To.AirportCode == "")
            {
               return true;
            }
         }
         
         return false;
      }

      public static bool CheckSameAirport(Flight flight)
      {
         if(flight.From.AirportCode.ToLower().Trim() == flight.To.AirportCode.ToLower().Trim())
         {
            return true;
         }
                
         return false;
      }

      public static bool CheckStrangeDates(Flight flight)
      {
         var departure = DateTime.Parse(flight.DepartureTime);
         var arrival = DateTime.Parse(flight.ArrivalTime);

         if(arrival <= departure)
         {
            return true;
         }         
         
         return false;
      }

      public static bool DeleteFlight(int Id)
      {
         Console.WriteLine(Id);
         if(Id > -1)
         {
            return true;
         }

         return false;
      }

      public static Airport SearchAirports(string name)
      {
         Console.WriteLine(name);
         foreach(var f in _flights)
         if(f.From.Country.Contains(name)
            || f.From.Country.Contains(name)
            || f.From.Country.Contains(name)
         )
         {
            return f.From;
         }

         return null;
      }
   }
}