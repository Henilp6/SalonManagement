using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class SalonBranchXGenderRepository : Repository<SalonBranchXGender>, ISalonBranchXGenderRepository
    {
        private readonly ApplicationDbContext _db;
        public SalonBranchXGenderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<SalonBranchXGender> UpdateAsync(SalonBranchXGender entity)
        {
            _db.SalonBranchXGenders.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
