
using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;
namespace SalonManagement.Repository.IRepostiory
{
    public interface ICountryRepository : IRepository<Country>
    {
      
        Task<Country> UpdateAsync(Country entity);
  
    }
}
