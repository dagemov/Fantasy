﻿using System.Linq.Expressions;

namespace Data.Interfaces;

public interface IRepositoryGeneric<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(
              Expression<Func<T, bool>> filter = null!,
              Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!,
              string includeProperties = null!  //Include
          );

    Task<T> GetAsync(
             Expression<Func<T, bool>> filter = null!,
             string includeProperties = null!
        );

    Task Add(T entity);

    void Delete(T entity);
}