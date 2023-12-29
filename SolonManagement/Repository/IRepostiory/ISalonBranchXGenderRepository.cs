using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;
using System.Linq.Expressions;

namespace SalonManagement.Repository.IRepostiory
{
    public interface ISalonBranchXGenderRepository : IRepository<SalonBranchXGender>
    {
      
        Task<SalonBranchXGender> UpdateAsync(SalonBranchXGender entity);
  
    }
}
