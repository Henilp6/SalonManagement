

using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;
namespace SalonManagement.Repository.IRepostiory
{
    public interface IStateRepository : IRepository<State>
    {
      
        Task<State> UpdateAsync(State entity);
  
    }
}
