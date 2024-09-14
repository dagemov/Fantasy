using Models.DTOS;
using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IRepositoryGeneric<T> where T : class
{
    Task<ApiResponse<T>> GetAsync(int id);

    Task<ApiResponse<IEnumerable<T>>> GetAsync();

    Task<ApiResponse<T>> AddAsync(T entity);

    Task<ApiResponse<T>> DeleteAsync(int id);

    Task<ApiResponse<T>> UpdateAsync(T entity);
}