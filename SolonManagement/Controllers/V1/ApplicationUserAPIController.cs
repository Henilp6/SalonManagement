﻿using AutoMapper;


        [HttpGet("{Id}", Name = "GetApplicationUser")]



        //[HttpGet("{companyId:int}", Name = "GetUserByRole")]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType(StatusCodes.Status401Unauthorized)]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<ActionResult<APIResponse>> GetUserByRole(int roleId)
        //{
        //    try
        //    {
        //        if (roleId == 0)
        //        {
        //            _response.StatusCode = HttpStatusCode.BadRequest;
        //            return BadRequest(_response);
        //        }
        //        var states = _db.ApplicationUsers.Include(u => u.Role).Include(u => u.UserName).Where(u => u.Id == roleId).ToList();

        //        if (states == null || states.Count() == 0)
        //        {
        //            _response.StatusCode = HttpStatusCode.NotFound;
        //            return NotFound(_response);
        //        }

        //        response.Result = mapper.Map<List<ApplicationUserDTO>>(states);
        //        _response.StatusCode = HttpStatusCode.OK;
        //        return Ok(_response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string>() { ex.ToString() };
        //    }

        //    return _response;
        //}

        [HttpPost(Name = "CreateApplicationUser")]


        //[HttpPut(Name = "UpdateApplicationUser")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> UpdateApplicationUser([FromForm] UserVM updateDTO)

        //{
        //    try
        //    {
        //        List<ApplicationRoleDTO> RoleList = _mapper.Map<List<ApplicationRoleDTO>>(updateDTO.ApplicationRoleList);
        //        // CarXColor carxcolor = _mapper.Map<CarXColor>(createDTO.CarXColor);
        //        ApplicationUserDTO User = _mapper.Map<ApplicationUserDTO>(updateDTO.ApplicationUser);

        //        await _unitOfWork.ApplicationUserRole.RemoveRangeAsync(x => x.UserId == User.Id, false);

        //        foreach (var roleList in RoleList)
        //        {
        //            if (roleList.IsChecked == true)
        //            {
        //                ApplicationUserRole applicationUserRole = new();
        //                applicationUserRole.UserId = User.Id;
        //                applicationUserRole.RoleId = roleList.Id;
        //                await _unitOfWork.ApplicationUserRole.CreateAsync(applicationUserRole);
        //            }
        //            else
        //            {
        //                continue;
        //            }
        //        }
        //        _response.StatusCode = HttpStatusCode.Created;
        //        return _response;
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages
        //             = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}
    }