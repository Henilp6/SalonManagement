using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface ISalonBranchRepository : IRepository<SalonBranch>
    {
      
        Task<SalonBranch> UpdateAsync(SalonBranch entity);
  
    }
}
