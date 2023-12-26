﻿using AutoMapper;
using System.Net;
namespace SalonManagement.Controllers.V1
	[Route("api/v{version:apiVersion}/[Controller]/[Action]")]
	[ApiController]


		[HttpGet(Name = "GetStates")]
		[ResponseCache(CacheProfileName = "Default30")]
            {
                IEnumerable<State> stateList = await _unitOfWork.State.GetAllAsync(includeProperties: "Country");


                if (!string.IsNullOrEmpty(search))
                {
                    string datasearch = search.ToLower();
                    stateList = stateList.Where(u => u.StateName.ToLower().Contains(datasearch));
                }
                // Pagination pagination = new() { PageNumber = pageNumber, PageSize = pageSize };
                if (pageNumber > 0)
                {
                    stateList = stateList.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
                }
                Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(stateList));
                _response.Result = _mapper.Map<List<StateDTO>>(stateList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }


        //[HttpGet(Name = "StateByPagination")]
        //        term = string.IsNullOrEmpty(term) ? "" : term.ToLower();

        //        StateIndexVM stateIndexVM = new StateIndexVM();
        //        IEnumerable<State> list = await _unitOfWork.State.GetAllAsync(includeProperties: "Country");

        //        var List = _mapper.Map<List<StateDTO>>(list);

        //        stateIndexVM.NameSortOrder = string.IsNullOrEmpty(orderBy) ? "countryName_desc" : "";

        //        if (!string.IsNullOrEmpty(term))
        //        {
        //            List = List.Where(u => u.StateName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase) ||
        //            u.Country.CountryName.ToLower().Contains(term, StringComparison.OrdinalIgnoreCase)).ToList();
        //        }

        //        switch (orderBy)
        //        {
        //            case "countryName_desc":
        //                List = List.OrderByDescending(a => a.StateName).ToList();
        //                break;

        //            default:
        //                List = List.OrderBy(a => a.StateName).ToList();
        //                break;
        //        }
        //        int totalRecords = List.Count();
        //        int pageSize = 10;
        //        int totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        //        List = List.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        //        // current=1, skip= (1-1=0), take=5 
        //        // currentPage=2, skip (2-1)*5 = 5, take=5 ,
        //        stateIndexVM.states = List;
        //        stateIndexVM.CurrentPage = currentPage;
        //        stateIndexVM.TotalPages = totalPages;
        //        stateIndexVM.Term = term;
        //        stateIndexVM.PageSize = pageSize;
        //        stateIndexVM.OrderBy = orderBy;

        //        _response.Result = _mapper.Map<StateIndexVM>(stateIndexVM);

        // StateAPIController.cs

        [HttpGet("{countryId:int}", Name = "GetStatesByCountryId")]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetStatesByCountryId(int countryId)
        {
            try
            {
                if (countryId == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var states =  _db.States.Include(u => u.Country).Where(u => u.CountryId == countryId).ToList();

                if (states == null || states.Count() == 0)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<List<StateDTO>>(states);
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
        //[ProducesResponseType(200, Type =typeof(CategoryDTO))]
        //        [ResponseCache(Location =ResponseCacheLocation.None,NoStore =true)]
        public async Task<ActionResult<APIResponse>> GetState(int id)



		[HttpPost(Name = "CreateState")]
		// [Authorize(Roles = "admin")]
		[ProducesResponseType(StatusCodes.Status201Created)]
                //if (!ModelState.IsValid)
                //{
                //    return BadRequest(ModelState);
                //}
                var existingState = await _unitOfWork.State.GetAsync(u => u.StateName.Trim().ToLower() == createDTO.StateName.Trim().ToLower() && u.CountryId == createDTO.CountryId);

                if (existingState != null)
                {
                        ModelState.AddModelError("ErrorMessages", "State name already Exists!");
                        return BadRequest(ModelState);
                }

                //if (await _unitOfWork.State.GetAsync(u => u.StateName.ToLower() == createDTO.StateName.ToLower() && u.CountryId == createDTO.CountryId) != null)
                //{
                //    ModelState.AddModelError("ErrorMessages", "State already Exists!");
                //    return BadRequest(ModelState);
                //}


                if (await _unitOfWork.Country.GetAsync(u => u.Id == createDTO.CountryId) == null)

        public async Task<ActionResult<APIResponse>> DeleteState(int id)