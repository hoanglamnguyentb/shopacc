using Hinet.Model.MongoEntities;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MongoDBCore
{
    public class MongoProvider<T> where T : MongoEntity
    {
        private static readonly Type _type = typeof(T);
        private static readonly string _name = _type.Name;

        public static bool CheckExist(Expression<Func<T, bool>> expression)
        {
            var name = typeof(T).Name;

            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);
                var builder = Builders<T>.Filter;
                var query = builder.Where(expression);
                return collection.Find(query).Any();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T Create(T entity)
        {
            var name = typeof(T).Name;
            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);

                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();
                entity.CreateDate = DateTime.Now;
                collection.InsertOne(entity);
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> Create(IEnumerable<T> entities)
        {
            var name = typeof(T).Name;

            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);
                foreach (var entity in entities)
                {
                    if (entity.Id == Guid.Empty)
                        entity.Id = Guid.NewGuid();
                    entity.CreateDate = DateTime.Now;
                }
                collection.InsertMany(entities);
                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T Update(T entity)
        {
            var name = typeof(T).Name;

            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);

                entity.CreateDate = DateTime.Now;
                collection.ReplaceOne(x => x.Id == entity.Id, entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IEnumerable<T> Update(IEnumerable<T> entities)
        {
            var name = typeof(T).Name;

            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);
                foreach (var entity in entities)
                {
                    entity.CreateDate = DateTime.Now;
                    collection.ReplaceOne(x => x.Id == entity.Id, entity);
                }

                return entities;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update<TValue>(Expression<Func<T, bool>> expression, Expression<Func<T, TValue>> feild, TValue value)
        {
            var name = typeof(T).Name;

            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);
                var filter = Builders<T>.Filter.Where(expression);
                var update = Builders<T>.Update.Set(feild, value);
                collection.UpdateMany(filter, update);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void Update(Expression<Func<T, bool>> expression, string prop, object value)
        {
            var name = typeof(T).Name;
            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);

                var propInfo = typeof(T).GetProperty(prop);
                if (propInfo != null)
                {
                    value = Convert.ChangeType(value, propInfo.PropertyType);
                    var entites = collection.AsQueryable().Where(expression).ToArray();
                    foreach (var entity in entites)
                    {
                        propInfo.SetValue(entity, value);
                    }
                    Update(entites);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static T CreateOrUpdate(T entity)
        {
            var name = typeof(T).Name;
            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);

                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                var builder = Builders<T>.Filter;
                var query = builder.Where(x => x.Id == entity.Id);
                if (collection.Find(query).Any())
                {
                    entity.UpdateDate = DateTime.Now;
                    collection.ReplaceOne(query, entity);
                }
                else
                {
                    entity.CreateDate = DateTime.Now;
                    collection.InsertOne(entity);
                }
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IEnumerable<T> CreateOrUpdate(IEnumerable<T> entities)
        {
            var name = typeof(T).Name;
            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);

                var builder = Builders<T>.Filter;
                foreach (var entity in entities)
                {
                    if (entity.Id == Guid.Empty)
                        entity.Id = Guid.NewGuid();

                    var query = builder.Where(x => x.Id == entity.Id);
                    if (collection.Find(query).Any())
                    {
                        entity.UpdateDate = DateTime.Now;
                        collection.ReplaceOne(query, entity);
                    }
                    else
                    {
                        entity.CreateDate = DateTime.Now;
                        collection.InsertOne(entity);
                    }
                }
                return entities;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static bool Delete(Guid id)
        {
            var name = typeof(T).Name;

            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);
                collection.DeleteOne(x => x.Id == id);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static IQueryable<T> GetQueryable()
        {
            var name = typeof(T).Name;

            try
            {
                var collection = MongoContext.Instance.Database.GetCollection<T>(_name);
                return collection.AsQueryable();
            }
            catch (Exception)
            {
            }
            return Enumerable.Empty<T>().AsQueryable();
        }
    }
}