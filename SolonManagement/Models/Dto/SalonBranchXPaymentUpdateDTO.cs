using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models.Dto
{
    public class SalonBranchXPaymentUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [ForeignKey("SalonBranch")]
        public int SalonBranchId { get; set; }
        [ValidateNever]
        public SalonBranch SalonBranch { get; set; }

        [ForeignKey("Payment")]
        public int PaymentId { get; set; }
        [ValidateNever]
        public Payment Payment { get; set; }
    }
}
