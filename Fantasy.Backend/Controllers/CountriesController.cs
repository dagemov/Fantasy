using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using System.Net;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _countryService;
    private readonly ApiResponse _response;

    public CountriesController(ICountryService countryService)
    {
        _countryService = countryService;
        _response = new ApiResponse();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CountryDTO countryDTO)
    {
        try
        {
            await _countryService.Add(countryDTO);
            _response.IsSuccesfuly = true;
            _response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            _response.IsSuccesfuly = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            _response.Result = await _countryService.GetAll();
            _response.IsSuccesfuly = true;
            _response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            _response.IsSuccesfuly = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCountry(int id)
    {
        try
        {
            _response.Result = await _countryService.Get(id);
            _response.IsSuccesfuly = true;
            _response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            _response.IsSuccesfuly = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _countryService.Delete(id);
            _response.IsSuccesfuly = true;
            _response.StatusCode = HttpStatusCode.NoContent;
        }
        catch (Exception ex)
        {
            _response.IsSuccesfuly = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> Update(CountryDTO countryDTO)
    {
        try
        {
            await _countryService.Update(countryDTO);
            _response.IsSuccesfuly = true;
            _response.StatusCode = HttpStatusCode.OK;
        }
        catch (Exception ex)
        {
            _response.IsSuccesfuly = false;
            _response.StatusCode = HttpStatusCode.BadRequest;
            _response.Message = ex.Message;
        }
        return Ok(_response);
    }
}