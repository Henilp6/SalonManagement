using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalonManagement.Models.Dto
{
    public class PaymentDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Payment Name")]
        public string PaymentName { get; set; }
        public bool IsActive { get; set; }

    }
}
