using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class FirstServiceRepository : Repository<FirstService>, IFirstServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public FirstServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<FirstService> UpdateAsync(FirstService entity)
        {
            _db.FirstServices.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
