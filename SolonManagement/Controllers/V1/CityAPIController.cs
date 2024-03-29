﻿using AutoMapper;
using SalonManagement.Data;
using SalonManagement.Models;
using SalonManagement.Models.Dto;
using SalonManagement.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Security.Claims;
using System.Text.Json;


namespace SalonManagement.Controllers.V1
{
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]
    [ApiVersion("1.0")]

    public class CityAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _db;

        public CityAPIController(IUnitOfWork unitOfWork, IMapper mapper,ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
            _db= db;
        }


		[HttpGet(Name = "GetCitys")]
		[ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetCitys([FromQuery(Name = "filterDisplayOrder")] int? Id,
           [FromQuery] string? search, int pageSize = 0, int pageNumber = 1)
        {
             try
            {
                IEnumerable<City> cityList = await _unitOfWork.City.GetAllAsync(includeProperties: "Country,State");


                if (!string.IsNullOrEmpty(search))
                {
                    string datasearch = search.ToLower();
                    cityList = cityList.Where(u => u.CityName.ToLower().Contains(datasearch) || u.Country.CountryName.ToLower().Contains(datasearch) ||
                                                 u.State.StateName.ToLower().Contains(datasearch));
                }
                // Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                if (pageNumber > 0)
                {
                   cityList = cityList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(cityList));
                _response.Result = _mapper.Map<List<CityDTO>>(cityList);
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


        [HttpGet("{stateId:int}", Name = "GetCitysByStateId")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetCitysByStateId(int stateId)
        {
            try
            {
                if (stateId == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var states = _db.Cities.Include(u => u.State).Include(u => u.Country).Where(u => u.StateId == stateId).ToList();

                if (states == null || states.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<List<CityDTO>>(states);
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

        [HttpGet("{id:int}", Name = "GetCity")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetCity(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var category = await _unitOfWork.City.GetAsync(u => u.Id == id, includeProperties: "Country,State");
                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<CityDTO>(category);
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


        [HttpPost(Name = "CreateCity")]
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateCity([FromForm] CityCreateDTO createDTO)
        {

            try
            {
                

                if (await _unitOfWork.City.GetAsync(u => u.CityName.Trim().ToLower() == createDTO.CityName.Trim().ToLower() && u.StateId == createDTO.StateId) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "City name already exists!");
                    return BadRequest(ModelState);
                }




                //if (await _unitOfWork.City.GetAsync(u => u.CityName.ToLower() == createDTO.CityName.ToLower() && u.StateId == createDTO.StateId) != null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "City already Exists!");
                //    return BadRequest(ModelState);
                //}

                if (await _unitOfWork.Country.GetAsync(u => u.Id == createDTO.CountryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Country name is Invalid!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.State.GetAsync(u => u.Id == createDTO.StateId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "State name is Invalid!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                City category = _mapper.Map<City>(createDTO);

                await _unitOfWork.City.CreateAsync(category);
                _response.Result = _mapper.Map<CityDTO>(category);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetCity", new { id = category.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteCity")]
      
        public async Task<ActionResult<APIResponse>> DeleteCity(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var category = await _unitOfWork.City.GetAsync(u => u.Id == id);
                if (category == null)
                {
                    return NotFound();
                }
                await _unitOfWork.City.RemoveAsync(category);
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

        [HttpPut("{id:int}", Name = "UpdateCity")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateCity(int id, [FromForm] CityUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.City.GetAsync(u => u.CityName.ToLower() == updateDTO.CityName.ToLower() && u.StateId == updateDTO.StateId && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "City already Exists!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.Country.GetAsync(u => u.Id == updateDTO.CountryId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                    return BadRequest(ModelState);
                }
                if (await _unitOfWork.Country.GetAsync(u => u.Id == updateDTO.StateId) == null)
                {
                    ModelState.AddModelError("ErrorMessages", "Category ID is Invalid!");
                    return BadRequest(ModelState);
                }
                City model = _mapper.Map<City>(updateDTO);
                await _unitOfWork.City.UpdateAsync(model);
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

