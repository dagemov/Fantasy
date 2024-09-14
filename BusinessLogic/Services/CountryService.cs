using AutoMapper;
using BusinessLogic.Interfaces;
using Data.Interfaces;
using Models.DTOS;
using Models.Entities;

namespace BusinessLogic.Services;

public class CountryService : GenericService<Country, CountryDTO>, ICountryService
{
    public CountryService(IUnitWork unitWork, IMapper mapper)
        : base(unitWork, mapper)
    {
    }
}