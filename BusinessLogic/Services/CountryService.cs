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

    public async Task Delete(int id)
    {
        try
        {
            var countryDb = await _unitWork.CountryRepository.GetAsync(c => c.Id == id);

            if (countryDb == null) throw new TaskCanceledException("The record does not exist");

            _unitWork.CountryRepository.Delete(countryDb);
            await _unitWork.Save();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<CountryDTO> Get(int id)
    {
        try
        {
            var country = await _unitWork.CountryRepository.GetAsync(c => c.Id == id);

            if (country == null) throw new TaskCanceledException("No countries records match with the id : " + id);

            return _mapper.Map<CountryDTO>(_mapper.Map<CountryDTO>(country));
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<IEnumerable<CountryDTO>> GetAll()
    {
        try
        {
            var list = await _unitWork.CountryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CountryDTO>>(list);
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task Update(CountryDTO countryDTO)
    {
        try
        {
            var countryDb = await _unitWork.CountryRepository.GetAsync(c => c.Id == countryDTO.Id);

            if (countryDb == null) throw new TaskCanceledException("Record not found or not exist , ples check out");

            countryDb.Name = countryDTO.Name;

            _unitWork.CountryRepository.Update(countryDb);
            await _unitWork.Save();
        }
        catch (Exception)
        {
            throw;
        }
    }
}