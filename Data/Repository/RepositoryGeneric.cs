using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DTOS;
using System.Linq.Expressions;

namespace Data.Repository;

public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
{
    private readonly DataContext _context;
    private readonly DbSet<T> _entity;

    public RepositoryGeneric(DataContext context)
    {
        _context = context;
        _entity = context.Set<T>();
    }

    public virtual async Task<ApiResponse<T>> AddAsync(T entity)
    {
        _context.Add(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ApiResponse<T>
            {
                IsSuccesfuly = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    public virtual async Task<ApiResponse<T>> DeleteAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ApiResponse<T>
            {
                IsSuccesfuly = false,
                Message = "ERR001"
            };
        }

        try
        {
            _entity.Remove(row);
            await _context.SaveChangesAsync();
            return new ApiResponse<T>
            {
                IsSuccesfuly = true,
            };
        }
        catch
        {
            return new ApiResponse<T>
            {
                IsSuccesfuly = false,
                Message = "ERR002"
            };
        }
    }

    public virtual async Task<ApiResponse<T>> GetAsync(int id)
    {
        var row = await _entity.FindAsync(id);
        if (row == null)
        {
            return new ApiResponse<T>
            {
                IsSuccesfuly = false,
                Message = "ERR001"
            };
        }
        return new ApiResponse<T>
        {
            IsSuccesfuly = true,
            Result = row
        };
    }

    public virtual async Task<ApiResponse<IEnumerable<T>>> GetAsync()
    {
        return new ApiResponse<IEnumerable<T>>
        {
            IsSuccesfuly = true,
            Result = await _entity.ToListAsync()
        };
    }

    public virtual async Task<ApiResponse<T>> UpdateAsync(T entity)
    {
        _context.Update(entity);
        try
        {
            await _context.SaveChangesAsync();
            return new ApiResponse<T>
            {
                IsSuccesfuly = true,
                Result = entity
            };
        }
        catch (DbUpdateException)
        {
            return DbUpdateExceptionActionResponse();
        }
        catch (Exception exception)
        {
            return ExceptionActionResponse(exception);
        }
    }

    //Erros Catch
    private ApiResponse<T> ExceptionActionResponse(Exception exception)
    {
        return new ApiResponse<T>
        {
            IsSuccesfuly = false,
            Message = exception.Message
        };
    }

    private ApiResponse<T> DbUpdateExceptionActionResponse()
    {
        return new ApiResponse<T>
        {
            IsSuccesfuly = false,
            Message = "ERR003"
        };
    }
}