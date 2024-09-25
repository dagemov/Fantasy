using AutoMapper;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using Models.Entities;
using System.Net;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : BaseApiController<Team, TeamDTO>
{
    private readonly IUnitWork _unitWork;
    private readonly IMapper _mapper;

    public TeamsController(IUnitWork unitWork, IMapper mapper) : base(unitWork, mapper)
    {
        _unitWork = unitWork;
        _mapper = mapper;
    }

    [HttpGet]
    public override async Task<IActionResult> GetAll()
    {
        var response = await _unitWork.TeamRepository.GetAsync();

        if (response.IsSuccesfuly)
        {
            var responseDTO = _mapper.Map<IEnumerable<TeamDTO>>(response.Result);
            return Ok(new ApiResponse<IEnumerable<TeamDTO>>
            {
                IsSuccesfuly = true,
                StatusCode = HttpStatusCode.OK,
                Result = responseDTO,
            });
        }

        return BadRequest(response.Message);
    }

    [HttpGet("{id}")]
    public override async Task<IActionResult> GetById(int id)
    {
        var response = await _unitWork.TeamRepository.GetAsync(id);

        if (response.IsSuccesfuly)
        {
            var responseDTO = _mapper.Map<TeamDTO>(response.Result);
            return Ok(new ApiResponse<TeamDTO>
            {
                IsSuccesfuly = true,
                StatusCode = HttpStatusCode.OK,
                Result = responseDTO,
            });
        }

        return NotFound(new ApiResponse<TeamDTO>
        {
            IsSuccesfuly = false,
            Message = response.Message,
            StatusCode = HttpStatusCode.NotFound
        });
    }

    [HttpGet("combo/{countryId:int}")]
    public async Task<IActionResult> GetComboAsync(int countryId)
    {
        return Ok(await _unitWork.TeamRepository.GetComboAsync(countryId));
    }

    [HttpPost("full")]
    public async Task<IActionResult> PostAsync(TeamDTO teamDTO)
    {
        var action = await _unitWork.TeamRepository.AddAsync(teamDTO);
        if (action.IsSuccesfuly)
        {
            return Ok(action.Result);
        }
        return BadRequest(action.Message);
    }
}