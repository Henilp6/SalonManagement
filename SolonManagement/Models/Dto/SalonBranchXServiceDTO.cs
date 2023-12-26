using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace SalonManagement.Models.Dto
{
    public class SalonBranchXServiceDTO
    {
        [Required]
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
