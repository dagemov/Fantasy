using Models.DTOS;
using Models.Entities;

namespace Data.Interfaces;

public interface ITeamRepository
{
    Task<IEnumerable<Team>> GetComboAsync(int countryId);

    Task<ApiResponse<Team>> AddAsync(TeamDTO teamDTO);

    Task<ApiResponse<Team>> UpdateAsync(TeamDTO teamDTO);

    Task<ApiResponse<Team>> GetAsync(int id);

    Task<ApiResponse<IEnumerable<Team>>> GetAsync();
}