﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
namespace SalonManagement.Models.Dto
{
    public class CityDTO
    {
        [Required] 
        public int Id { get; set; }

        [Required]
        [DisplayName("City Name")]
        public string CityName { get; set; }

        public int CountryId { get; set; }
        [ValidateNever]
        public CountryDTO Country { get; set; }

        public int StateId { get; set; }
        [ValidateNever]
        public StateDTO State { get; set; }

        public bool IsActive { get; set; }


    }
}
