using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface IGenderRepository : IRepository<Gender>
    {
      
        Task<Gender> UpdateAsync(Gender entity);
  
    }
}
