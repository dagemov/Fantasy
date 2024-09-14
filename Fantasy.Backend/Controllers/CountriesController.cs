using AutoMapper;
using BusinessLogic.Interfaces;
using Data.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Models.DTOS;
using Models.Entities;
using System.Collections.Generic;
using System.Net;

namespace Fantasy.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : BaseApiController<Country, CountryDTO>
{
    public CountriesController(IUnitWork unitWork, IMapper mapper)
        : base(unitWork, mapper)
    {
    }
}