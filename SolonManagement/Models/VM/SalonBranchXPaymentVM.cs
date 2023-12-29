using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models.Dto
{
    public class SalonBranchXPaymentVM
    {
        public int SalonBranchId { get; set; }
        public List<String> SelectedPaymentIds { get; set; }
    }
}
