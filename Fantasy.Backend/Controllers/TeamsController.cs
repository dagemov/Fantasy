using AutoMapper;
using Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using Models.Entities;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TeamsController : BaseApiController<Team, TeamDTO>
{
    public TeamsController(IUnitWork unitWork, IMapper mapper) : base(unitWork, mapper)
    {
    }
}