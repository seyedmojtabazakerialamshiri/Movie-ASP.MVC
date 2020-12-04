using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FilmApp.Repository
{
    /// <summary>
    /// Generic repository interface to bed used by all entities
    /// </summary>
    public interface IRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        IQueryable<T> Get();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        T SingleOrDefault(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}
