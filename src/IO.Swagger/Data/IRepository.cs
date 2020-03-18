using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;

namespace IO.Swagger.Data
{
    public interface IRepository<T> : IDisposable where T: EntityBase
    {
        T GetById(Guid id);

        Guid Create(T entity);

        void Delete(T entity);

        void Update(T entity);
        
        IEnumerable<T> Query(Expression<Func<T, bool>> filter);
    }
}