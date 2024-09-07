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
    private ApiResponse _response;

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
}