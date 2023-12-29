﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
        
        public async Task<ActionResult<APIResponse>> GetSalonBranchXGender(int id)
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
            try {

                await _unitOfWork.SalonBranchXGender.RemoveRangeAsync(u => u.SalonBranchId == createDTO.SalonBranchId , false);

                for (int i = 0; i < createDTO.SelectedGenderIds.Count; i++)
                {
                    SalonBranchXGender salonBranchXGender = new();
                    salonBranchXGender.SalonBranchId = createDTO.SalonBranchId;
                    salonBranchXGender.GenderId = Convert.ToInt32(createDTO.SelectedGenderIds[i]);

                    await _unitOfWork.SalonBranchXGender.CreateAsync(salonBranchXGender);
                }
                _response.StatusCode = HttpStatusCode.Created;
                return _response;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
                return _response;
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteSalonBranchXGender(int id)

        [HttpGet("{salonBranchId:int}", Name = "GetSalonBranchXGenderBySalonBranchId")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetSalonBranchXGenderBySalonBranchId(int salonBranchId)
        {
            try
            {
                if (salonBranchId == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var salonBranchXGenders = _db.SalonBranchXGenders.Include(u => u.Gender).Include(u => u.SalonBranch).Where(u => u.SalonBranchId == salonBranchId).ToList();

                if (salonBranchXGenders == null || salonBranchXGenders.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<List<SalonBranchXGenderDTO>>(salonBranchXGenders);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }