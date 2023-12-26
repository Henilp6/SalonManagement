
using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;

namespace SalonManagement.Repository.IRepository
{
	public interface IApplicationUserRoleRepository : IRepository<ApplicationUserRole>
    {
        Task<ApplicationUserRole> UpdateAsync(ApplicationUserRole entity);
    }
}