using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SalonManagement.Models;

namespace SalonManagement.Models
{
    public class SalonBranchXService
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("SalonBranch")]
        public int SalonBranchId { get; set; }
        [ValidateNever]
        public SalonBranch SalonBranch { get; set; }
        [ForeignKey("FirstService")]
        public int FirstServiceId { get; set; }
        [ValidateNever]
        public FirstService FirstService { get; set; }
    }
}
