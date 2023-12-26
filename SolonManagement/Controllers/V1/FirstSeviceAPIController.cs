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
    public class FirstServiceAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public FirstServiceAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet(Name = "GetFirstServices")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetFirstServices([FromQuery(Name = "filterDisplayOrder")] int? Id,
            [FromQuery] string search, int pageSize = 0, int pageNumber = 0)
        {
            try
            {
                IEnumerable<FirstService> firstServiceList = await _unitOfWork.FirstService.GetAllAsync();


                if (!string.IsNullOrEmpty(search))
                {
                    string datasearch = search.ToLower();
                    firstServiceList = firstServiceList.Where(u => u.FirstServiceName.ToLower().Contains(datasearch));
                }
                // Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                if (pageNumber > 0)
                {
                    firstServiceList = firstServiceList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(firstServiceList));
                _response.Result = _mapper.Map<List<FirstServiceDTO>>(firstServiceList);
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


        [HttpGet("{id:int}", Name = "GetFirstService")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(FirstServiceDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetFirstService(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var firstService = await _unitOfWork.FirstService.GetAsync(u => u.Id == id);
                if (firstService == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<FirstServiceDTO>(firstService);
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

        [HttpPost(Name = "CreateFirstService")]
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateFirstService([FromForm] FirstServiceCreateDTO createDTO)
        {

            try
            {
                if (await _unitOfWork.FirstService.GetAsync(u => u.FirstServiceName.Trim().ToLower() == createDTO.FirstServiceName.Trim().ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstService name already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                FirstService firstService = _mapper.Map<FirstService>(createDTO);

                await _unitOfWork.FirstService.CreateAsync(firstService);
                _response.Result = _mapper.Map<FirstServiceDTO>(firstService);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetFirstService", new { id = firstService.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteFirstService")]
        //   [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteFirstService(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var firstService = await _unitOfWork.FirstService.GetAsync(u => u.Id == id);
                if (firstService == null)
                {
                    return NotFound();
                }
                await _unitOfWork.FirstService.RemoveAsync(firstService);
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
        [HttpPut("{id:int}", Name = "UpdateFirstService")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateFirstService(int id, [FromForm] FirstServiceUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.FirstService.GetAsync(u => u.FirstServiceName.ToLower() == updateDTO.FirstServiceName.ToLower() && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "FirstServiceName already Exists!");
                    return BadRequest(ModelState);
                }

                FirstService model = _mapper.Map<FirstService>(updateDTO);
                await _unitOfWork.FirstService.UpdateAsync(model);
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

      
    }
}
