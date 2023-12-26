using SalonManagement.Models;
using SalonManagement.Models.Dto;
using SalonManagement.Repository.IRepostiory;

namespace SalonManagement.Repository.IRepository
{
	public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<ApplicationUserDTO> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
