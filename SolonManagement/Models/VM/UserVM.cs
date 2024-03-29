﻿
using SalonManagement.Models.Dto;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalonManagement.Models.VM
{
    public class UserVM
    {
        public UserVM()
        {
            ApplicationUser = new ApplicationUserDTO();

            ApplicationUserRole = new ApplicationUserRoleDTO();
        }
        public ApplicationUserDTO ApplicationUser { get; set; }
        public ApplicationUserRoleDTO ApplicationUserRole { get; set; }


        [ValidateNever]
        public List<ApplicationUserRoleDTO> ApplicationUserRoleList { get; set; }

        [ValidateNever]
        public List<ApplicationRoleDTO> ApplicationRoleList { get; set; }
    }
}
