using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class SecondServiceRepository : Repository<SecondService>, ISecondServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public SecondServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<SecondService> UpdateAsync(SecondService entity)
        {
            _db.SecondServices.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
