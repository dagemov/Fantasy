using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using System.Collections.Generic;
using System.Net;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;
    //private readonly ApiResponse _response;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
        //_response = new ApiResponse();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CountryDTO countryDTO)
    {
        var response = new ApiResponse<CountryDTO>();
        try
        {
            await _countryService.Add(countryDTO);
            response.IsSuccesfuly = true;
            response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            response.IsSuccesfuly = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Message = ex.Message;
        }
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = new ApiResponse<IEnumerable<CountryDTO>>();
        try
        {
            response.Result = await _countryService.GetAll();
            response.IsSuccesfuly = true;
            response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            response.IsSuccesfuly = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Message = ex.Message;
        }
        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        var response = new ApiResponse<CountryDTO>();
        try
        {
            response.Result = await _countryService.Get(id);
            response.IsSuccesfuly = true;
            response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            response.IsSuccesfuly = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Message = ex.Message;
        }
        return Ok(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var response = new ApiResponse<CountryDTO>();
        try
        {
            await _countryService.Delete(id);
            response.IsSuccesfuly = true;
            response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            response.IsSuccesfuly = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Message = ex.Message;
        }
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update(CountryDTO countryDTO)
    {
        var response = new ApiResponse<CountryDTO>();
        try
        {
            await _countryService.Update(countryDTO);
            response.IsSuccesfuly = true;
            response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            response.IsSuccesfuly = false;
            response.StatusCode = HttpStatusCode.BadRequest;
            response.Message = ex.Message;
        }
        return Ok(response);
    }
}