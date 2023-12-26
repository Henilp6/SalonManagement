using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalonManagement.Models
{
    public class FirstService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("FirstService Name")]
        public string FirstServiceName { get; set; }

        public bool IsActive { get; set; }
    }
}
