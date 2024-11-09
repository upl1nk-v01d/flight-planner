using FlightPlanner.Core.Models; 

namespace FlightPlanner.Core.Services
{
    public interface IDBClearingService : IDbService
    {
        ServiceResult Clear<T>() where T : Entity;
    }
}
