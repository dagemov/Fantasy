using AutoMapper;
using Models.DTOS;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilyties
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Country, CountryDTO>();

            //Inverse Maps
            CreateMap<CountryDTO, Country>();
        }
    }
}