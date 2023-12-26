using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface ISecondServiceRepository : IRepository<SecondService>
    {
      
        Task<SecondService> UpdateAsync(SecondService entity);
  
    }
}
