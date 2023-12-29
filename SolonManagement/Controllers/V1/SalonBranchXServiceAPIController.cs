using AutoMapper;using Azure;using SalonManagement.Data;using SalonManagement.Models;using SalonManagement.Models.Dto;using SalonManagement.Models.VM;using SalonManagement.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;using Microsoft.AspNetCore.Http.HttpResults;using Microsoft.AspNetCore.Identity;using Microsoft.AspNetCore.JsonPatch;using Microsoft.AspNetCore.Mvc;using Microsoft.EntityFrameworkCore;using System.Data;using System.Drawing;using System.Net;using System.Security.Claims;using System.Text.Json;using static System.Runtime.InteropServices.JavaScript.JSType;namespace SalonManagement.Controllers.v1{
    [Route("api/v{version:apiVersion}/[controller]/[Action]")]
    [ApiController]    [ApiVersion("1.0")]    public class SalonBranchXServiceAPIController : ControllerBase    {        protected APIResponse _response;        private readonly IUnitOfWork _unitOfWork;        private readonly IMapper _mapper;        private readonly ApplicationDbContext _db;        public SalonBranchXServiceAPIController(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext db)        {            _unitOfWork = unitOfWork;            _mapper = mapper;            _response = new();            _db = db;        }        [HttpGet(Name = "GetSalonBranchXServices")]        [ResponseCache(CacheProfileName = "Default30")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        public async Task<ActionResult<APIResponse>> GetSalonBranchXServices([FromQuery(Name = "filterDisplayOrder")] int? Id,            [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)        {            try            {                IEnumerable<SalonBranchXService> salonBranchXServiceList;                if (Id > 0)                {                    salonBranchXServiceList = await _unitOfWork.SalonBranchXService.GetAllAsync(u => u.Id == Id, includeProperties: "SalonBranch,FirstService", pageSize: pageSize,                        pageNumber: pageNumber);                }                else                {                    salonBranchXServiceList = await _unitOfWork.SalonBranchXService.GetAllAsync(includeProperties: "SalonBranch,FirstService", pageSize: pageSize,                        pageNumber: pageNumber);                }                if (!string.IsNullOrEmpty(search))                {                    salonBranchXServiceList = salonBranchXServiceList.Where(u => u.SalonBranch.BranchName.ToLower().Contains(search));                }                Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(pagination));                _response.Result = _mapper.Map<List<SalonBranchXServiceDTO>>(salonBranchXServiceList);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpGet("{id:int}", Name = "GetSalonBranchXService")]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status200OK)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<APIResponse>> GetSalonBranchXService(int id)        {            try            {                if (id == 0)                {                    _response.StatusCode = HttpStatusCode.BadRequest;                    return BadRequest(_response);                }                var salonBranchXService = await _unitOfWork.SalonBranchXService.GetAsync(u => u.Id == id);                if (salonBranchXService == null)                {                    _response.StatusCode = HttpStatusCode.NotFound;                    return NotFound(_response);                }                _response.Result = _mapper.Map<SalonBranchXServiceDTO>(salonBranchXService);                _response.StatusCode = HttpStatusCode.OK;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }        [HttpPost(Name = "CreateSalonBranchXService")]
        //[Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [ProducesResponseType(StatusCodes.Status500InternalServerError)]        public async Task<ActionResult<APIResponse>> CreateSalonBranchXService([FromForm]SalonBranchXServiceVM createDTO)        {
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
                return _response;        }        [ProducesResponseType(StatusCodes.Status204NoContent)]        [ProducesResponseType(StatusCodes.Status403Forbidden)]        [ProducesResponseType(StatusCodes.Status401Unauthorized)]        [ProducesResponseType(StatusCodes.Status404NotFound)]        [ProducesResponseType(StatusCodes.Status400BadRequest)]        [HttpDelete("{id:int}", Name = "DeleteSalonBranchXService")]
        //[Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteSalonBranchXService(int id)        {            try            {                if (id == 0)                {                    return BadRequest();                }                var salonBranchXService = await _unitOfWork.SalonBranchXService.GetAsync(u => u.Id == id);                if (salonBranchXService == null)                {                    return NotFound();                }                await _unitOfWork.SalonBranchXService.RemoveAsync(salonBranchXService);                _response.StatusCode = HttpStatusCode.NoContent;                _response.IsSuccess = true;                return Ok(_response);            }            catch (Exception ex)            {                _response.IsSuccess = false;                _response.ErrorMessages                     = new List<string>() { ex.ToString() };            }            return _response;        }

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
        }    }}