using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Cache.Services.Redis
{
    /// <summary>
    /// Redis 帮助类
    /// </summary>
    public class RedisHelper
    {
        ConnectionMultiplexer connectionMultiplexer;
        IDatabase database;
        JsonSerializerSettings jsonConfig = new JsonSerializerSettings() { ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore, NullValueHandling = NullValueHandling.Ignore };

        public RedisHelper()
        {
            connectionMultiplexer = AppDbContext.Instance;
            database = connectionMultiplexer.GetDatabase();
        }

        class CacheObject<T>
        {
            public T Value { get; set; }
        }

        public bool Exists(string key)
        {
            return database.KeyExists(key);
        }

        public object Get(string key)
        {
            return Get<object>(key);
        }

        public T Get<T>(string key)
        {
            //var cacheValue = Get(key);
            //return (T)cacheValue;
            T t = default(T);
            var cacheValue = database.StringGet(key);
            if (!cacheValue.IsNull)
            {
                var cacheObject = JsonConvert.DeserializeObject<CacheObject<T>>(cacheValue, jsonConfig);
                t = cacheObject.Value;
            }
            return t;
        }

        public bool SetCache(string key, object value, DateTime expiresIn)
        {
            var timeSpan = expiresIn - DateTime.Now;
            var cacheObject = new CacheObject<object>() { Value = value };
            var jsonStr = JsonConvert.SerializeObject(cacheObject, jsonConfig);
            return database.StringSet(key, jsonStr, timeSpan);
        }

        public void Remove(string key)
        {
            database.KeyDelete(key, CommandFlags.HighPriority);
        }

    }
}
