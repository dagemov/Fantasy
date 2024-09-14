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
    private readonly IRepositoryGeneric<Country> _countryRepository;

    public CountryService(IMapper mapper, IUnitWork unitWork)
    {
        _mapper = mapper;
        _unitWork = unitWork;
        _countryRepository = _unitWork.GetRepository<Country>(); // Obtener el repositorio generico para 'Country'
    }

    public async Task<CountryDTO> Add(CountryDTO countryDTO)
    {
        try
        {
            var country = _mapper.Map<Country>(countryDTO);
            await _countryRepository.AddAsync(country);
            await _unitWork.Save();

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
            var countryDb = await _countryRepository.GetAsync(id);

            if (!countryDb.IsSuccesfuly || countryDb.Result == null)
                throw new TaskCanceledException("The record does not exist");

            await _countryRepository.DeleteAsync(id);
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
            var response = await _countryRepository.GetAsync(id);

            if (!response.IsSuccesfuly || response.Result == null)
            {
                throw new TaskCanceledException($"No countries records match with the id: {id}");
            }

            return _mapper.Map<CountryDTO>(response.Result);
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
            var list = await _countryRepository.GetAsync();
            return _mapper.Map<IEnumerable<CountryDTO>>(list.Result);
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
            var countryDb = await _countryRepository.GetAsync(countryDTO.Id);

            if (!countryDb.IsSuccesfuly || countryDb.Result == null)
                throw new TaskCanceledException("Record not found or not exist, please check out");

            var country = countryDb.Result;
            country.Name = countryDTO.Name;

            await _countryRepository.UpdateAsync(country);
            await _unitWork.Save();
        }
        catch (Exception)
        {
            throw;
        }
    }
}