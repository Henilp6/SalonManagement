﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
        
        public async Task<ActionResult<APIResponse>> GetSalonBranchXService(int id)
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
            try {

                await _unitOfWork.SalonBranchXService.RemoveRangeAsync(u => u.SalonBranchId == createDTO.SalonBranchId , false);

                for (int i = 0; i < createDTO.SelectedServiceIds.Count; i++)
                {
                    SalonBranchXService salonBranchXService = new();
                    salonBranchXService.SalonBranchId = createDTO.SalonBranchId;
                    salonBranchXService.FirstServiceId = Convert.ToInt32(createDTO.SelectedServiceIds[i]);

                    await _unitOfWork.SalonBranchXService.CreateAsync(salonBranchXService);
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
        public async Task<ActionResult<APIResponse>> DeleteSalonBranchXService(int id)

        [HttpGet("{salonBranchId:int}", Name = "GetSalonBranchXServiceBySalonBranchId")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetSalonBranchXServiceBySalonBranchId(int salonBranchId)
        {
            try
            {
                if (salonBranchId == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var salonBranchXServices = _db.SalonBranchXServices.Include(u => u.FirstService).Include(u => u.SalonBranch).Where(u => u.SalonBranchId == salonBranchId).ToList();

                if (salonBranchXServices == null || salonBranchXServices.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<List<SalonBranchXServiceDTO>>(salonBranchXServices);
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