using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Repository;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository
{
    public class SalonBranchXPaymentRepository : Repository<SalonBranchXPayment>, ISalonBranchXPaymentRepository
    {
        private readonly ApplicationDbContext _db;
        public SalonBranchXPaymentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<SalonBranchXPayment> UpdateAsync(SalonBranchXPayment entity)
        {
            _db.SalonBranchXPayments.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
