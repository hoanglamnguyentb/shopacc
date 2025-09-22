using Hinet.Model;
using log4net;
using MongoDB.Driver;
using System;
using System.Linq;
using ILog = log4net.ILog;

namespace Hinet.Web.Core
{
    public class CacheMongoProvider
    {
        public static void SaveCache<T>(T model)
        {
            var typeObj = typeof(T);
            ILog log = LogManager.GetLogger(typeObj);
            try
            {
                MongoDBContext mongoDBContext = new MongoDBContext();
                var collect = mongoDBContext.Database.GetCollection<T>(typeObj.Name);

                if (model != null)
                {
                    var builder = Builders<T>.Filter;
                    var query = builder.Empty;
                    var lstData = collect.Find(query).ToList();
                    if (lstData != null && lstData.Any())
                    {
                        collect.DeleteMany(query);
                    }
                }
                collect.InsertOne(model);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public static void ClearCache<T>()
        {
            var typeObj = typeof(T);
            ILog log = LogManager.GetLogger(typeObj);
            MongoDBContext mongoDBContext = new MongoDBContext();
            //Kiểm tra nếu tồn tại thì xóa

            try
            {
                var collect = mongoDBContext.Database.GetCollection<T>(typeObj.Name);
                var builder = Builders<T>.Filter;
                var query = builder.Empty;
                //query &= builder.And(builder.Eq(x => x.Id, idLog));

                collect.DeleteMany(query);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
        }

        public static T GetCache<T>()
        {
            var typeObj = typeof(T);
            ILog log = LogManager.GetLogger(typeObj);
            MongoDBContext mongoDBContext = new MongoDBContext();
            //Kiểm tra nếu tồn tại thì xóa

            try
            {
                var collect = mongoDBContext.Database.GetCollection<T>(typeObj.Name);
                var builder = Builders<T>.Filter;
                var query = builder.Empty;
                //query &= builder.And(builder.Eq(x => x.Id, idLog));

                var firstData = collect.Find(query).FirstOrDefault();
                return firstData;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            return default(T);
        }
    }
}