﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SalonManagement.Models.Dto
{
    public class CountryCreateDTO
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Country Name")]
        public string CountryName { get; set; }

        public bool IsActive { get; set; }


    }
}
