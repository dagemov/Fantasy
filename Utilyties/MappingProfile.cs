using AutoMapper;
using Models.DTOS;
using Models.Entities;

namespace Utilyties;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDTO>();

        //Inverse Maps
        CreateMap<CountryDTO, Country>();
    }
}