using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface ISalonBranchXPaymentRepository : IRepository<SalonBranchXPayment>
    {
      
        Task<SalonBranchXPayment> UpdateAsync(SalonBranchXPayment entity);
  
    }
}
