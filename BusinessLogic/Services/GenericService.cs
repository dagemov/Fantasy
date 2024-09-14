using AutoMapper;
using BusinessLogic.Interfaces;
using Data.Interfaces;

namespace BusinessLogic.Services;

public class GenericService<TEntity, TDto> : IGenericService<TDto>
  where TEntity : class
  where TDto : class
{
    private readonly IUnitWork _unitWork;
    private readonly IRepositoryGeneric<TEntity> _repository;
    private readonly IMapper _mapper;

    public GenericService(IUnitWork unitWork, IMapper mapper)
    {
        _unitWork = unitWork;
        _repository = _unitWork.GetRepository<TEntity>();
        _mapper = mapper;
    }

    public virtual async Task<TDto> Add(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        var result = await _repository.AddAsync(entity);
        await _unitWork.Save();
        return _mapper.Map<TDto>(result.Result);
    }

    public virtual async Task Delete(int id)
    {
        await _repository.DeleteAsync(id);
        await _unitWork.Save();
    }

    public virtual async Task<TDto> Get(int id)
    {
        var result = await _repository.GetAsync(id);
        return _mapper.Map<TDto>(result.Result);
    }

    public virtual async Task<IEnumerable<TDto>> GetAll()
    {
        var result = await _repository.GetAsync();
        return _mapper.Map<IEnumerable<TDto>>(result.Result);
    }

    public virtual async Task Update(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);
        await _repository.UpdateAsync(entity);
        await _unitWork.Save();
    }
}