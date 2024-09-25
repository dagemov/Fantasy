using Data.Helpers.Interfaces;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOS;
using Models.Entities;
using System.Net;

namespace Data.Repository;

public class TeamRepository : RepositoryGeneric<Team>, ITeamRepository
{
    private readonly DataContext _context;
    private readonly IFileStorage _fileStorage;

    public TeamRepository(DataContext context, IFileStorage fileStorage) : base(context)
    {
        _context = context;
        _fileStorage = fileStorage;
    }

    public async Task<ApiResponse<Team>> AddAsync(TeamDTO teamDTO)
    {
        var country = await _context.Countries.FindAsync(teamDTO.CountryId);
        if (country == null)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                StatusCode = HttpStatusCode.NotFound,
                Message = "ERR004",
            };
        }

        var team = new Team
        {
            Country = country,
            Name = teamDTO.Name,
        };

        //CHeck if imagen is not null
        if (!string.IsNullOrEmpty(teamDTO.Image))
        {
            var imageBase64 = Convert.FromBase64String(teamDTO.Image);
            team.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
        }
        _context.SaveChanges();

        try
        {
            await _context.SaveChangesAsync();
            return new ApiResponse<Team>
            {
                IsSuccesfuly = true,
                StatusCode = HttpStatusCode.Created,
                Result = team,
                Message = "RecordOk"
            };
        }
        catch (DbUpdateException)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                Message = "ERR003"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                Message = ex.Message,
            };
        }
    }

    public override async Task<ApiResponse<IEnumerable<Team>>> GetAsync()
    {
        var teams = await _context.Teams
            .Include(x => x.Country)
            .OrderBy(x => x.Name)
            .ToListAsync();
        return new ApiResponse<IEnumerable<Team>>
        {
            IsSuccesfuly = true,
            Result = teams,
            StatusCode = HttpStatusCode.OK,
        };
    }

    public override async Task<ApiResponse<Team>> GetAsync(int id)
    {
        var team = await _context.Teams
            .Include(x => x.Country)
            .FirstOrDefaultAsync(x => x.Id == id);
        if (team == null)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                Message = "ERR001",
                StatusCode = HttpStatusCode.NotFound
            };
        }
        return new ApiResponse<Team>
        {
            IsSuccesfuly = true,
            Result = team,
            StatusCode = HttpStatusCode.OK,
            Message = "RecordOk"
        };
    }

    public async Task<IEnumerable<Team>> GetComboAsync(int countryId)
    {
        return await _context.Teams
            .Where(x => x.CountryId == countryId)
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public async Task<ApiResponse<Team>> UpdateAsync(TeamDTO teamDTO)
    {
        var currentTeam = await _context.Teams.FindAsync(teamDTO.Id);
        if (currentTeam == null)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                Message = "ERR005"
            };
        }

        var country = await _context.Countries.FindAsync(teamDTO.CountryId);
        if (country == null)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                Message = "ERR004"
            };
        }

        if (!string.IsNullOrEmpty(teamDTO.Image))
        {
            var imageBase64 = Convert.FromBase64String(teamDTO.Image);
            currentTeam.Image = await _fileStorage.SaveFileAsync(imageBase64, ".jpg", "teams");
        }

        currentTeam.CountryId = teamDTO.CountryId;
        currentTeam.Name = teamDTO.Name;

        _context.Teams.Update(currentTeam);

        try
        {
            await _context.SaveChangesAsync();

            return new ApiResponse<Team>
            {
                IsSuccesfuly = true,
                StatusCode = HttpStatusCode.OK,
                Result = currentTeam,
                Message = "RecordOk"
            };
        }
        catch (DbUpdateException)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                Message = "ERR003"
            };
        }
        catch (Exception ex)
        {
            return new ApiResponse<Team>
            {
                IsSuccesfuly = false,
                Message = ex.Message,
            };
        }
    }
}