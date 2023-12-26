
using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;

namespace SalonManagement.Repository.IRepository
{
	public interface IApplicationUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser> UpdateAsync(ApplicationUser entity);
    }
}