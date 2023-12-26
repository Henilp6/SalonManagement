

using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface ICityRepository : IRepository<City>
    {
      
        Task<City> UpdateAsync(City entity);
  
    }
}
