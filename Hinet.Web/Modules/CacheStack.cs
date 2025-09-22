using ServiceStack.Redis;

namespace Hinet.Web.Modules
{
    public class CacheStack
    {
        private readonly RedisEndpoint _redisEndpoint;
        private readonly string AppName = "sdi_bacgiang_cayxanhdothi";

        public CacheStack()
        {
            var host = "127.0.0.1";
            var port = 6379;
            _redisEndpoint = new RedisEndpoint(host, port);
        }

        public void SetStrings(string key, string value)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                key = $"{AppName}:{key}";
                redisClient.SetValue(key, value);
            }
        }

        public string GetStrings(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                key = $"{AppName}:{key}";
                return redisClient.GetValue(key);
            }
        }

        public bool IsKeyExists(string key)
        {
            using (var redisClient = new RedisClient(_redisEndpoint))
            {
                key = $"{AppName}:{key}";
                if (redisClient.ContainsKey(key))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}