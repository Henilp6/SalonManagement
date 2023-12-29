using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class SalonBranchXServiceRepository : Repository<SalonBranchXService>, ISalonBranchXServiceRepository
    {
        private readonly ApplicationDbContext _db;
        public SalonBranchXServiceRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<SalonBranchXService> UpdateAsync(SalonBranchXService entity)
        {
            _db.SalonBranchXServices.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
