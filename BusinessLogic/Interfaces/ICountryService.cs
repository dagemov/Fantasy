using Models.DTOS;

namespace BusinessLogic.Interfaces;

public interface ICountryService
{
    Task<IEnumerable<CountryDTO>> GetAll();

    Task<CountryDTO> Add(CountryDTO countryDTO);

    Task<CountryDTO> Update(CountryDTO countryDTO);

    Task<CountryDTO> Delete(int id);
}