using Models.DTOS;

namespace BusinessLogic.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDTO>> GetAll();

    Task<CountryDTO> Add(CountryDTO countryDTO);

    Task Update(CountryDTO countryDTO);

    Task Delete(int id);

    Task<CountryDTO> Get(int id);
}