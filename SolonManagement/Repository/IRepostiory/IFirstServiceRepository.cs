using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface IFirstServiceRepository : IRepository<FirstService>
    {
      
        Task<FirstService> UpdateAsync(FirstService entity);
  
    }
}
