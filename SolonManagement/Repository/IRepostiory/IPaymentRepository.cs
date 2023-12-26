using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface IPaymentRepository : IRepository<Payment>
    {
      
        Task<Payment> UpdateAsync(Payment entity);
  
    }
}
