using AutoMapper;
using BusinessLogic.Interfaces;
using Data.Interfaces;
using Models.DTOS;
using Models.Entities;

namespace BusinessLogic.Services;

public class TeamService : GenericService<Team, TeamDTO>, ITeamService
{
    public TeamService(IUnitWork unitWork, IMapper mapper) : base(unitWork, mapper)
    {
    }
}