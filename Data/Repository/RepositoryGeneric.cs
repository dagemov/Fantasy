using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repository;

public class RepositoryGeneric<T> : IRepositoryGeneric<T> where T : class
{
    private readonly DataContext _context;
    private DbSet<T> _dbSet;

    public RepositoryGeneric(DataContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task Add(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            foreach (var ip in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(ip);
            }
        }
        if (orderBy != null)
        {
            return await orderBy(query).ToListAsync();
        }
        return await query.ToListAsync();
    }

    public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null)
    {
        IQueryable<T> query = _dbSet;
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (includeProperties != null)
        {
            foreach (var ip in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(ip);
            }
        }

        return await query.FirstOrDefaultAsync();
    }
}