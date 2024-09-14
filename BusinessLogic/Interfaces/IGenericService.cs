namespace BusinessLogic.Interfaces;

public interface IGenericService<TDto> where TDto : class
{
    Task<TDto> Add(TDto dto);

    Task<IEnumerable<TDto>> GetAll();

    Task<TDto> Get(int id);

    Task Update(TDto dto);

    Task Delete(int id);
}