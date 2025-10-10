using Hinet.Model.MongoEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDBCore
{
    public class MongoService<T> : IMongoService<T> where T : MongoEntity
    {
        public virtual bool CheckExist(Expression<Func<T, bool>> expression)
        {
            return MongoProvider<T>.CheckExist(expression);
        }

        public virtual T Create(T entity)
        {
            return MongoProvider<T>.Create(entity);
        }

        public virtual IEnumerable<T> Create(IEnumerable<T> entities)
        {
            return MongoProvider<T>.Create(entities);
        }

        public virtual T Update(T entity)
        {
            return MongoProvider<T>.Update(entity);
        }

        public virtual IEnumerable<T> Update(IEnumerable<T> entities)
        {
            return MongoProvider<T>.Update(entities);
        }

        public virtual void Update<TValue>(Expression<Func<T, bool>> expression, Expression<Func<T, TValue>> feild, TValue value)
        {
            MongoProvider<T>.Update(expression, feild, value);
        }

        public virtual void Update(Expression<Func<T, bool>> expression, string prop, object value)
        {
            MongoProvider<T>.Update(expression, prop, value);
        }

        public virtual T CreateOrUpdate(T entity)
        {
            return MongoProvider<T>.CreateOrUpdate(entity);
        }

        public virtual IEnumerable<T> CreateOrUpdate(IEnumerable<T> entities)
        {
            return MongoProvider<T>.CreateOrUpdate(entities);
        }

        public virtual bool Delete(Guid id)
        {
            return MongoProvider<T>.Delete(id);
        }

        public virtual IQueryable<T> GetQueryable()
        {
            return MongoProvider<T>.GetQueryable();
        }
    }
}