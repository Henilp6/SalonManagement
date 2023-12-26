using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models
{
    public class SecondService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("SecondService Name")]
        public string SecondServiceName { get; set; }
        [ForeignKey("FirstService")]
        public int FirstServiceId { get; set; }
        [ValidateNever]
        public FirstService FirstService { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }

    }
}
