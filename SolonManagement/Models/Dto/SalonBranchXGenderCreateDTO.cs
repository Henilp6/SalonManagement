using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models.Dto
{
    public class SalonBranchXGenderCreateDTO
    {
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
