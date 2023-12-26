using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using SalonManagement.Models;

namespace SalonManagement.Models
{
    public class SalonBranchXGender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("SalonBranch")]
        public int SalonBranchId { get; set; }
        [ValidateNever]
        public SalonBranch SalonBranch { get; set; }
        [ForeignKey("Gender")]
        public int GenderId { get; set; }
        [ValidateNever]
        public Gender Gender { get; set; }
    }
}
