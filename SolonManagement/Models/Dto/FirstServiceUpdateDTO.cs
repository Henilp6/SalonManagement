using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalonManagement.Models.Dto
{
    public class FirstServiceUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("FirstService Name")]
        public string FirstServiceName { get; set; }

        public bool IsActive { get; set; }
    }
}
