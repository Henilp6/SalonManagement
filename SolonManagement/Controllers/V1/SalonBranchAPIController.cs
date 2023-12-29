using AutoMapper;
using SalonManagement.Models;
using SalonManagement.Models.Dto;
using SalonManagement.Repository.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Net;
using System.Text.Json;

namespace SalonManagement.Controllers.v1
{
	//[Route("api/[controller]")]
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]
    [ApiVersion("1.0")]
    public class SalonBranchAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SalonBranchAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


		[HttpGet(Name = "GetSalonBranchs")]
		[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetSalonBranchs([FromQuery(Name = "filterDisplayOrder")] int? Id,
            [FromQuery] string search, int pageSize = 0, int pageNumber = 1)
        {
            try
            {

                IEnumerable<SalonBranch> salonBranchList = await _unitOfWork.SalonBranch.GetAllAsync(includeProperties: "Country,State,City");

             
                if (!string.IsNullOrEmpty(search))
                {
                    string datasearch = search.ToLower();
                    salonBranchList = salonBranchList.Where(u => u.BranchName.ToLower().Contains(datasearch) || u.Area.ToLower().Contains(datasearch) || u.City.CityName.ToLower().Contains(datasearch));
                }
                // Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                salonBranchList = salonBranchList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(salonBranchList));
                _response.Result = _mapper.Map<List<SalonBranchDTO>>(salonBranchList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }



        [HttpGet("{id:int}", Name = "GetSalonBranch")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetSalonBranch(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var salonBranch = await _unitOfWork.SalonBranch.GetAsync(u => u.Id == id,includeProperties: "Country,State,City");
                if (salonBranch == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<SalonBranchDTO>(salonBranch);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost(Name = "CreateSalonBranch")]
       // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateSalonBranch([FromForm] SalonBranchCreateDTO createDTO)
        {

            try
            {
                if (await _unitOfWork.SalonBranch.GetAsync(u => u.BranchName.Trim().ToLower() == createDTO.BranchName.Trim().ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "SalonBranch name already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }
              
                SalonBranch salonBranch = _mapper.Map<SalonBranch>(createDTO);
            
                await _unitOfWork.SalonBranch.CreateAsync(salonBranch);
                _response.Result = _mapper.Map<SalonBranchDTO>(salonBranch);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetSalonBranch", new { id = salonBranch.Id }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{id:int}", Name = "DeleteSalonBranch")]
     //   [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteSalonBranch(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var salonBranch = await _unitOfWork.SalonBranch.GetAsync(u => u.Id == id, includeProperties: "Country,State,City");
                if (salonBranch == null)
                {
                    return NotFound();
                }
                await _unitOfWork.SalonBranch.RemoveAsync(salonBranch);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    //    [Authorize(Roles = "admin")]
        [HttpPut("{id:int}", Name = "UpdateSalonBranch")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateSalonBranch(int id, [FromForm] SalonBranchUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.SalonBranch.GetAsync(u => u.BranchName.ToLower() == updateDTO.BranchName.ToLower() && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "SalonBranchName already Exists!");
                    return BadRequest(ModelState);
                }

                SalonBranch model = _mapper.Map<SalonBranch>(updateDTO);
                await _unitOfWork.SalonBranch.UpdateAsync(model);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

       
        [HttpGet(Name = "SalonBranchSearchByLazyLoading")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> SalonBranchSearchByLazyLoading([FromQuery] int pageNum, string? search)
        {



            try
            {
                const int RecordsPerPage = 3;

                IEnumerable<SalonBranch> salonBranchDTOList = await _unitOfWork.SalonBranch.GetAllAsync(includeProperties: "Country,State,City");

                if (!string.IsNullOrEmpty(search))
                {
                    salonBranchDTOList = salonBranchDTOList.Where(x => x.BranchName.Contains(search, StringComparison.OrdinalIgnoreCase)
                    || x.Area.Contains(search, StringComparison.OrdinalIgnoreCase));
                }
                int skip = pageNum * RecordsPerPage;
                var tempList = salonBranchDTOList.Skip(skip).Take(RecordsPerPage).ToList();

                if (pageNum == 0 && tempList.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { "Data does not exists" };
                }
                else
                {
                    _response.Result = _mapper.Map<List<SalonBranchDTO>>(tempList);

                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet(Name = "SalonBranchSearchLocationByLazyLoading")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> SalonBranchSearchLocationByLazyLoading([FromQuery] int pageNum, string? search, string? location)
        {

            try
            {
                const int RecordsPerPage = 3;
               
                IEnumerable<SalonBranch> salonBranchDTOList = await _unitOfWork.SalonBranch.GetAllAsync(includeProperties: "Country,State,City");
                 

                if (!string.IsNullOrEmpty(search))
                {
                    salonBranchDTOList = salonBranchDTOList.Where(x => x.BranchName.Contains(search, StringComparison.OrdinalIgnoreCase));
                }
                if (!string.IsNullOrEmpty(location))
                {
                    salonBranchDTOList = salonBranchDTOList.Where(x => x.City.CityName.Contains(location, StringComparison.OrdinalIgnoreCase)
                    || x.Area.Contains(location, StringComparison.OrdinalIgnoreCase) || x.State.StateName.Contains(location, StringComparison.OrdinalIgnoreCase) 
                    || x.Country.CountryName.Contains(location, StringComparison.OrdinalIgnoreCase));
                }
                int skip = pageNum * RecordsPerPage;
                var tempList = salonBranchDTOList.Skip(skip).Take(RecordsPerPage).ToList();

                if (pageNum == 0 && tempList.Count == 0)
                {
                    _response.IsSuccess = false;
                    _response.ErrorMessages
                         = new List<string>() { "Data does not exists" };
                }
                else
                {
                    _response.Result = _mapper.Map<List<SalonBranchDTO>>(tempList);

                    _response.StatusCode = HttpStatusCode.OK;
                    return Ok(_response);
                }

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                     = new List<string>() { ex.ToString() };
            }
            return _response;
        }

    }
}
