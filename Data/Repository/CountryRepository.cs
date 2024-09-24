using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOS;
using Models.Entities;
using System.Net;

namespace Data.Repository;

public class CountryRepository : RepositoryGeneric<Country>, ICountryRepository
{
    private readonly DataContext _context;

    public CountryRepository(DataContext context) : base(context)
    {
        _context = context;
    }

    public override async Task<ApiResponse<Country>> GetAsync(int id)
    {
        var country = await _context.Countries
          .Include(x => x.Teams)
          .FirstOrDefaultAsync(x => x.Id == id);

        if (country == null)
        {
            return new ApiResponse<Country>
            {
                IsSuccesfuly = false,
                Message = "ERR001"
            };
        }

        return new ApiResponse<Country>
        {
            IsSuccesfuly = true,
            Result = country
        };
    }

    public override async Task<ApiResponse<IEnumerable<Country>>> GetAsync()
    {
        //TODO : Si falla cambiarlo por un CountryDTO
        var countries = await _context.Countries
            .Include(x => x.Teams)
            .ToListAsync();

        return new ApiResponse<IEnumerable<Country>>
        {
            StatusCode = HttpStatusCode.OK,
            IsSuccesfuly = true,
            Result = countries
        };
    }

    public async Task<IEnumerable<Country>> GetComboAsync()
    {
        return await _context.Countries
            .OrderBy(x => x.Name)
            .ToListAsync();
    }

    public void Update(Country country)
    {
        var countryDb = _context.Countries.FirstOrDefault(c => c.Id == country.Id);

        if (countryDb != null)
        {
            countryDb.Name = country.Name;
            _context.SaveChanges();
        }
    }
}