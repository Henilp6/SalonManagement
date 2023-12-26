using SalonManagement.Models;
using SalonManagement.Repository.IRepostiory;

namespace SalonManagement.Repository.IRepository
{
	public interface IApplicationRoleRepository : IRepository<ApplicationRole>
    {
        Task<ApplicationRole> UpdateAsync(ApplicationRole entity);
    }
}

