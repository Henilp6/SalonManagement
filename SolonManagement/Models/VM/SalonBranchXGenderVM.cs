using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models.Dto
{
    public class SalonBranchXGenderVM
    {
        public int SalonBranchId { get; set; }
        public List<String> SelectedGenderIds { get; set; }
    }
}
