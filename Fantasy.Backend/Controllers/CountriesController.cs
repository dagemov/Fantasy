using AutoMapper;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using Models.Entities;
using System.Net;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : BaseApiController<Country, CountryDTO>
{
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;

    public CountriesController(IUnitWork unitWork, IMapper mapper)
        : base(unitWork, mapper)
    {
        _unitWork = unitWork;
        _mapper = mapper;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAll()
    {
        var response = await _unitWork.CountryRepository.GetAsync();
        if (response.IsSuccesfuly)
        {
            //Si falla mapee la respuesta a dto ( crea lista en el dto y en el mapping mapeas "
            var countriesDto = _mapper.Map<IEnumerable<CountryDTO>>(response.Result);
            return Ok(new ApiResponse<IEnumerable<CountryDTO>>
            {
                IsSuccesfuly = true,
                Result = countriesDto,
                StatusCode = HttpStatusCode.OK
            });
        }
        return BadRequest(response.Message);
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(int id)
    {
        var response = await _unitWork.CountryRepository.GetAsync(id);
        if (response.IsSuccesfuly)
        {
            // Mapea la entidad `Country` a DTO
            var countryDto = _mapper.Map<CountryDTO>(response.Result);
            return Ok(new ApiResponse<CountryDTO>
            {
                IsSuccesfuly = true,
                Result = countryDto,
                StatusCode = HttpStatusCode.OK
            });
        }

        return NotFound(new ApiResponse<CountryDTO>
        {
            IsSuccesfuly = false,
            Message = response.Message,
            StatusCode = HttpStatusCode.NotFound
        });
    }

    [HttpGet("combo")]
    public async Task<IActionResult> GetComboAsync()
    {
        return Ok(await _unitWork.CountryRepository.GetComboAsync());
    }
}