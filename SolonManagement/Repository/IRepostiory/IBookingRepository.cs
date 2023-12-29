using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface IBookingRepository : IRepository<Booking>
    {
      
        Task<Booking> UpdateAsync(Booking entity);
  
    }
}
