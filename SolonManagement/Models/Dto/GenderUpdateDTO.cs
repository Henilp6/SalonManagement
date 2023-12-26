using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalonManagement.Models.Dto
{
    public class GenderUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Gender Name")]
        public string GenderType { get; set; }

        public bool IsActive { get; set; }
    }
}
