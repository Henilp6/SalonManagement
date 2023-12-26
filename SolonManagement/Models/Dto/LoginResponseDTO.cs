namespace SalonManagement.Models.Dto
{
	public class LoginResponseDTO
    {
        public ApplicationUserDTO ApplicationUser { get; set; }
        public string Token { get; set; }
    }
}
