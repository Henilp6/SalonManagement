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
    public class BookingAPIController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BookingAPIController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _response = new();
        }


        [HttpGet(Name = "GetBookings")]
        [ResponseCache(CacheProfileName = "Default30")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetBookings([FromQuery(Name = "filterDisplayOrder")] int? Id,
            [FromQuery] string search, int pageSize = 0, int pageNumber = 0)
        {
            try
            {
                IEnumerable<Booking> bookingList = await _unitOfWork.Booking.GetAllAsync(includeProperties: "Country");


                if (!string.IsNullOrEmpty(search))
                {
                    string datasearch = search.ToLower();
                    bookingList = bookingList.Where(u => u.CustomerName.ToLower().Contains(datasearch));
                }
                // Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                if (pageNumber > 0)
                {
                    bookingList = bookingList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(bookingList));
                _response.Result = _mapper.Map<List<BookingDTO>>(bookingList);
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


        [HttpGet("{id:int}", Name = "GetBooking")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(200, Type =typeof(BookingDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var booking = await _unitOfWork.Booking.GetAsync(u => u.Id == id);
                if (booking == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<BookingDTO>(booking);
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

        [HttpPost(Name = "CreateBooking")]
        // [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateBooking([FromForm] BookingCreateDTO createDTO)
        {

            try
            {
                if (await _unitOfWork.Booking.GetAsync(u => u.CustomerEmail.Trim().ToLower() == createDTO.CustomerEmail.Trim().ToLower()) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "Booking name already Exists!");
                    return BadRequest(ModelState);
                }
                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                Booking booking = _mapper.Map<Booking>(createDTO);

                await _unitOfWork.Booking.CreateAsync(booking);
                _response.Result = _mapper.Map<BookingDTO>(booking);
                _response.StatusCode = HttpStatusCode.Created;
                return CreatedAtRoute("GetBooking", new { id = booking.Id }, _response);
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
        [HttpDelete("{id:int}", Name = "DeleteBooking")]
        //   [Authorize(Roles = "admin")]
        public async Task<ActionResult<APIResponse>> DeleteBooking(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }
                var booking = await _unitOfWork.Booking.GetAsync(u => u.Id == id);
                if (booking == null)
                {
                    return NotFound();
                }
                await _unitOfWork.Booking.RemoveAsync(booking);
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
        [HttpPut("{id:int}", Name = "UpdateBooking")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateBooking(int id, [FromForm] BookingUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.Id)
                {
                    return BadRequest();
                }
                if (await _unitOfWork.Booking.GetAsync(u => u.CustomerEmail.ToLower() == updateDTO.CustomerEmail.ToLower() && u.Id != updateDTO.Id) != null)
                {
                    ModelState.AddModelError("ErrorMessages", "BookingName already Exists!");
                    return BadRequest(ModelState);
                }

                Booking model = _mapper.Map<Booking>(updateDTO);
                await _unitOfWork.Booking.UpdateAsync(model);
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
