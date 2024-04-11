using CaféCozyApp.Models;

namespace CaféCozyApp.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int? id);
    }
}
