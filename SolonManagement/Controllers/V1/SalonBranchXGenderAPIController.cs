using AutoMapper;using Azure;using SalonManagement.Data;using SalonManagement.Models;using SalonManagement.Models.Dto;using SalonManagement.Models.VM;using SalonManagement.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http.HttpResults;using Microsoft.AspNetCore.Identity;using Microsoft.AspNetCore.JsonPatch;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using System.Data;using System.Drawing;using System.Net;using System.Security.Claims;using System.Text.Json;using static System.Runtime.InteropServices.JavaScript.JSType;namespace SalonManagement.Controllers.v1{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]    [ApiVersion("1.0")]    public class SalonBranchXGenderAPIController : ControllerBase    {        protected APIResponse _response;        private readonly IUnitOfWork _unitOfWork;        private readonly IMapper _mapper;        private readonly ApplicationDbContext _db;        public SalonBranchXGenderAPIController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext db)        {            _unitOfWork = unitOfWork;            _mapper = mapper;            _response = new();            _db = db;        }        [HttpGet(Name = "GetSalonBranchXGenders")]        [ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> GetSalonBranchXGenders([FromQuery(Name = "filterDisplayOrder")] int? Id,            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)        {            try            {                IEnumerable<SalonBranchXGender> salonBranchXGenderList;                if (Id > 0)                {                    salonBranchXGenderList = await _unitOfWork.SalonBranchXGender.GetAllAsync(u => u.Id == Id, includeProperties: "SalonBranch,Gender", pageSize: pageSize,                        pageNumber: pageNumber);                }                else                {                    salonBranchXGenderList = await _unitOfWork.SalonBranchXGender.GetAllAsync(includeProperties: "SalonBranch,Gender", pageSize: pageSize,                        pageNumber: pageNumber);                }                if (!string.IsNullOrEmpty(search))                {                    salonBranchXGenderList = salonBranchXGenderList.Where(u => u.SalonBranch.BranchName.ToLower().Contains(search));                }                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));                _response.Result = _mapper.Map<List<SalonBranchXGenderDTO>>(salonBranchXGenderList);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpGet("{id:int}", Name = "GetSalonBranchXGender")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<APIResponse>> GetSalonBranchXGender(int id)        {            try            {                if (id == 0)                {                    _response.StatusCode = HttpStatusCode.BadRequest;                    return BadRequest(_response);                }                var salonBranchXGender = await _unitOfWork.SalonBranchXGender.GetAsync(u => u.Id == id);                if (salonBranchXGender == null)                {                    _response.StatusCode = HttpStatusCode.NotFound;                    return NotFound(_response);                }                _response.Result = _mapper.Map<SalonBranchXGenderDTO>(salonBranchXGender);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpPost(Name = "CreateSalonBranchXGender")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        public async Task<ActionResult<APIResponse>> CreateSalonBranchXGender([FromForm]SalonBranchXGenderVM createDTO)        {
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
                return _response;        }        [ProducesResponseType(StatusCodes.Status204NoContent)]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status404NotFound)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [HttpDelete("{id:int}", Name = "DeleteSalonBranchXGender")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteSalonBranchXGender(int id)        {            try            {                if (id == 0)                {                    return BadRequest();                }                var salonBranchXGender = await _unitOfWork.SalonBranchXGender.GetAsync(u => u.Id == id);                if (salonBranchXGender == null)                {                    return NotFound();                }                await _unitOfWork.SalonBranchXGender.RemoveAsync(salonBranchXGender);                _response.StatusCode = HttpStatusCode.NoContent;                _response.IsSuccess = true;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }

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
        }    }}