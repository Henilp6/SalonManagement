using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models
{
    public class SalonBranch
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Branch Name")]
        public string BranchName { get; set; }
        [Required]
        public int YearofEstablishment {  get; set; }
        public string Image {  get; set; }
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
        [ForeignKey("City")]
        public int CityId { get; set; }
        [ValidateNever]
        public City City { get; set; }

        [ForeignKey("State")]
        public int StateId { get; set; }
        [ValidateNever]
        public State State { get; set; }

        [ForeignKey("Country")]
        public int CountryId { get; set; }
        [ValidateNever]
        public Country Country { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
