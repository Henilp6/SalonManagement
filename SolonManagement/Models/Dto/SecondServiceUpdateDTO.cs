﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace SalonManagement.Models.Dto
{
    public class SecondServiceUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayName("SecondService Name")]
        public string SecondServiceName { get; set; }
        public int FirstServiceId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
    }
}
