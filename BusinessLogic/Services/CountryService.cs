using AutoMapper;
using BusinessLogic.Interfaces;
using Data.Interfaces;
using Models.DTOS;
using Models.Entities;

namespace BusinessLogic.Services;

public class CountryService : ICountryService
{
    private readonly IMapper _mapper;
    private readonly IUnitWork _unitWork;

    public CountryService(IMapper mapper, IUnitWork unitWork)
    {
        _mapper = mapper;
        _unitWork = unitWork;
    }

    public async Task<CountryDTO> Add(CountryDTO countryDTO)
    {
        try
        {
            Country country = new Country()
            {
                Name = countryDTO.Name,
            };

            await _unitWork.CountryRepository.Add(country);
            await _unitWork.Save();

            if (country == null)
            {
                throw new TaskCanceledException("Error to created Country");
            }

            return _mapper.Map<CountryDTO>(country);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public Task<CountryDTO> Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CountryDTO>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<CountryDTO> Update(CountryDTO countryDTO)
    {
        throw new NotImplementedException();
    }
}