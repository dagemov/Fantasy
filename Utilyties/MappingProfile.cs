﻿using AutoMapper;
using Models.DTOS;
using Models.Entities;

namespace Utilyties;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Country, CountryDTO>()
           .ForMember(dest => dest.TeamsCount, opt => opt.MapFrom(src => src.Teams!.Count()))
           .ForMember(dest => dest.Teams, opt => opt.MapFrom(src => src.Teams));
        CreateMap<Team, TeamDTO>()
            .ForMember(dest => dest.CountryName, opt => opt.MapFrom(src => src.Country!.Name));
        //Inverse Maps
        CreateMap<CountryDTO, Country>();
        CreateMap<TeamDTO, Team>();
    }
}