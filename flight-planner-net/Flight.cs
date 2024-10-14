namespace flight_planner_net.Models
{
   public class Flight
   {
      public int Id;
      public Airport From;
      public Airport To;
      public string Carrier;
      public string DepartureTime;
      public string ArrivalTime;
   }
}