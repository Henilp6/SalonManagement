﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]
        
        public async Task<ActionResult<APIResponse>> GetSalonBranchXPayment(int id)
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
            try {

                await _unitOfWork.SalonBranchXPayment.RemoveRangeAsync(u => u.SalonBranchId == createDTO.SalonBranchId , false);

                for (int i = 0; i < createDTO.SelectedPaymentIds.Count; i++)
                {
                    SalonBranchXPayment salonBranchXPayment = new();
                    salonBranchXPayment.SalonBranchId = createDTO.SalonBranchId;
                    salonBranchXPayment.PaymentId = Convert.ToInt32(createDTO.SelectedPaymentIds[i]);

                    await _unitOfWork.SalonBranchXPayment.CreateAsync(salonBranchXPayment);
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
        public async Task<ActionResult<APIResponse>> DeleteSalonBranchXPayment(int id)

        [HttpGet("{salonBranchId:int}", Name = "GetSalonBranchXPaymentBySalonBranchId")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetSalonBranchXPaymentBySalonBranchId(int salonBranchId)
        {
            try
            {
                if (salonBranchId == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var salonBranchXPayments = _db.SalonBranchXPayments.Include(u => u.Payment).Include(u => u.SalonBranch).Where(u => u.SalonBranchId == salonBranchId).ToList();

                if (salonBranchXPayments == null || salonBranchXPayments.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<List<SalonBranchXPaymentDTO>>(salonBranchXPayments);
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