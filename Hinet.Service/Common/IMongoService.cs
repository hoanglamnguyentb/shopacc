using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDBCore
{
    public interface IMongoService<T>
    {
        bool CheckExist(Expression<Func<T, bool>> expression);

        T Create(T entity);

        IEnumerable<T> Create(IEnumerable<T> entities);

        T Update(T entity);

        IEnumerable<T> Update(IEnumerable<T> entities);

        void Update<TValue>(Expression<Func<T, bool>> expression, Expression<Func<T, TValue>> feild, TValue value);

        void Update(Expression<Func<T, bool>> expression, string feild, object value);

        T CreateOrUpdate(T entity);

        IEnumerable<T> CreateOrUpdate(IEnumerable<T> entities);

        bool Delete(Guid id);

        IQueryable<T> GetQueryable();
    }
}