using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalonManagement.Models.Dto
{
    public class SalonBranchDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
        [Required]
        public int YearofEstablishment { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public int MobileNumber { get; set; }
        [Required]
        public string Email { get; set; }
        public string OpenTime { get; set; }
        public string CloseTime { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Area { get; set; }
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
