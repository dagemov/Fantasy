using AutoMapper;
using Data.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models.DTOS;
using System.Collections.Generic;
using System.Net;

namespace Fantasy.Backend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BaseApiController<TEntity, TDto> : ControllerBase
        where TEntity : class
        where TDto : class
{
    private readonly IUnitWork _unitWork;
    private readonly IRepositoryGeneric<TEntity> _repository;
    private readonly IMapper _mapper;

    public BaseApiController(IUnitWork unitWork, IMapper mapper)
    {
        _unitWork = unitWork;
        _repository = _unitWork.GetRepository<TEntity>();
        _mapper = mapper;
    }

    [HttpPost]
    public virtual async Task<IActionResult> Create(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        var result = await _repository.AddAsync(entity);
        try
        {
            await _unitWork.Save();
        }
        catch (Exception)
        {
            return BadRequest(result.Message);
        }

        ApiResponse<TDto> apiResponse = new()
        {
            IsSuccesfuly = true,
            StatusCode = HttpStatusCode.OK,
            Result = result.IsSuccesfuly ? _mapper.Map<TDto>(result.Result) : null
        };

        return Ok(apiResponse);
    }

    [HttpGet]
    public virtual async Task<IActionResult> GetAll()
    {
        var result = await _repository.GetAsync();

        ApiResponse<IEnumerable<TDto>> apiResponse = new()
        {
            IsSuccesfuly = true,
            StatusCode = HttpStatusCode.OK,
            Result = result.IsSuccesfuly ? _mapper.Map<IEnumerable<TDto>>(result.Result) : null
        };
        return Ok(apiResponse);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(int id)
    {
        var result = await _repository.GetAsync(id);

        ApiResponse<TDto> apiResponse = new()
        {
            IsSuccesfuly = true,
            StatusCode = System.Net.HttpStatusCode.OK,
            Result = _mapper.Map<TDto>(result.Result)
        };

        return Ok(apiResponse);
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(int id)
    {
        var row = await _repository.DeleteAsync(id);
        try
        {
            await _unitWork.Save();
        }
        catch (Exception)
        {
            return BadRequest(row.Message);
        }

        return NoContent();
    }

    [HttpPut]
    public virtual async Task<IActionResult> Update(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        var resutl = await _repository.UpdateAsync(entity);
        try
        {
            await _unitWork.Save();
        }
        catch (Exception)
        {
            return BadRequest(resutl.Message);
        }

        return Ok();
    }
}