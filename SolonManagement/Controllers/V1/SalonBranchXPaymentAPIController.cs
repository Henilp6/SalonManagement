using AutoMapper;using Azure;using SalonManagement.Data;using SalonManagement.Models;using SalonManagement.Models.Dto;using SalonManagement.Models.VM;using SalonManagement.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http.HttpResults;using Microsoft.AspNetCore.Identity;using Microsoft.AspNetCore.JsonPatch;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using System.Data;using System.Drawing;using System.Net;using System.Security.Claims;using System.Text.Json;using static System.Runtime.InteropServices.JavaScript.JSType;namespace SalonManagement.Controllers.v1{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]    [ApiVersion("1.0")]    public class SalonBranchXPaymentAPIController : ControllerBase    {        protected APIResponse _response;        private readonly IUnitOfWork _unitOfWork;        private readonly IMapper _mapper;        private readonly ApplicationDbContext _db;        public SalonBranchXPaymentAPIController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext db)        {            _unitOfWork = unitOfWork;            _mapper = mapper;            _response = new();            _db = db;        }        [HttpGet(Name = "GetSalonBranchXPayments")]        [ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> GetSalonBranchXPayments([FromQuery(Name = "filterDisplayOrder")] int? Id,            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)        {            try            {                IEnumerable<SalonBranchXPayment> salonBranchXPaymentList;                if (Id > 0)                {                    salonBranchXPaymentList = await _unitOfWork.SalonBranchXPayment.GetAllAsync(u => u.Id == Id, includeProperties: "SalonBranch,Payment", pageSize: pageSize,                        pageNumber: pageNumber);                }                else                {                    salonBranchXPaymentList = await _unitOfWork.SalonBranchXPayment.GetAllAsync(includeProperties: "SalonBranch,Payment", pageSize: pageSize,                        pageNumber: pageNumber);                }                if (!string.IsNullOrEmpty(search))                {                    salonBranchXPaymentList = salonBranchXPaymentList.Where(u => u.SalonBranch.BranchName.ToLower().Contains(search));                }                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));                _response.Result = _mapper.Map<List<SalonBranchXPaymentDTO>>(salonBranchXPaymentList);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpGet("{id:int}", Name = "GetSalonBranchXPayment")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<APIResponse>> GetSalonBranchXPayment(int id)        {            try            {                if (id == 0)                {                    _response.StatusCode = HttpStatusCode.BadRequest;                    return BadRequest(_response);                }                var salonBranchXPayment = await _unitOfWork.SalonBranchXPayment.GetAsync(u => u.Id == id);                if (salonBranchXPayment == null)                {                    _response.StatusCode = HttpStatusCode.NotFound;                    return NotFound(_response);                }                _response.Result = _mapper.Map<SalonBranchXPaymentDTO>(salonBranchXPayment);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpPost(Name = "CreateSalonBranchXPayment")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        public async Task<ActionResult<APIResponse>> CreateSalonBranchXPayment([FromForm]SalonBranchXPaymentVM createDTO)        {
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
                return _response;        }        [ProducesResponseType(StatusCodes.Status204NoContent)]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status404NotFound)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [HttpDelete("{id:int}", Name = "DeleteSalonBranchXPayment")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteSalonBranchXPayment(int id)        {            try            {                if (id == 0)                {                    return BadRequest();                }                var salonBranchXPayment = await _unitOfWork.SalonBranchXPayment.GetAsync(u => u.Id == id);                if (salonBranchXPayment == null)                {                    return NotFound();                }                await _unitOfWork.SalonBranchXPayment.RemoveAsync(salonBranchXPayment);                _response.StatusCode = HttpStatusCode.NoContent;                _response.IsSuccess = true;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }

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
        }    }}