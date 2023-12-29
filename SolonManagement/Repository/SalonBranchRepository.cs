using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class SalonBranchRepository : Repository<SalonBranch>, ISalonBranchRepository
    {
        private readonly ApplicationDbContext _db;
        public SalonBranchRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<SalonBranch> UpdateAsync(SalonBranch entity)
        {
            _db.SalonBranchs.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
