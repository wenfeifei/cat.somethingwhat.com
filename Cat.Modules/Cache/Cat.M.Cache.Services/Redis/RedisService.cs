using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Cache.Services.Redis
{
    public class RedisService : ICacheService
    {
        RedisHelper _RedisHelper;
        public RedisService()
        {
            _RedisHelper = new RedisHelper();
        }

        public bool Exists(string key)
        {
            return _RedisHelper.Exists(key);
        }

        public object GetCache(string key)
        {
            return _RedisHelper.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return _RedisHelper.Get<T>(key);
        }

        public bool SetCache(string key, object value, DateTime expiresIn)
        {
            _RedisHelper.SetCache(key, value, expiresIn);
            return true;
        }

        public void Remove(string key)
        {
            _RedisHelper.Remove(key);
        }

    }
}
