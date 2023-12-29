using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        private readonly ApplicationDbContext _db;
        public BookingRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Booking> UpdateAsync(Booking entity)
        {
            _db.Bookings.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
