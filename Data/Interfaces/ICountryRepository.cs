using Models.DTOS;
using Models.Entities;

namespace Data.Interfaces;

public interface ICountryRepository
{
    Task<ApiResponse<Country>> GetAsync(int id);

    Task<ApiResponse<IEnumerable<Country>>> GetAsync();

    Task<IEnumerable<Country>> GetComboAsync();
}