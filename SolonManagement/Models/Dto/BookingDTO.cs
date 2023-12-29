﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models.Dto
{
    public class BookingDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        public int CustomerMobileNumber { get; set; }
        [Required]
        [DisplayName("Customer Email")]
        public string CustomerEmail { get; set; }
        [Required]
        public DateTime BookingDate { get; set; }
        public int Timming { get; set; }
        [ForeignKey("SalonBranch")]
        public int SalonBranchId { get; set; }
        [ValidateNever]
        public SalonBranch SalonBranch { get; set; }

    }
}
