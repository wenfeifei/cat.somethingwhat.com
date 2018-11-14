using System;
using System.Collections.Generic;
using System.Text;
using Cat.Utility;

namespace Cat.M.Cache.Services.MSCache
{
    public class MemoryCacheService : ICacheService
    {
        MemoryCacheHelper _MemoryCacheHelper;
        public MemoryCacheService()
        {
            _MemoryCacheHelper = new MemoryCacheHelper();
        }

        public bool Exists(string key)
        {
            return _MemoryCacheHelper.Exists(key);
        }

        public object GetCache(string key)
        {
            return _MemoryCacheHelper.Get(key);
        }

        public T GetCache<T>(string key)
        {
            return (T)_MemoryCacheHelper.Get(key);
            //return Serializer.JsonDeserialize<T>(data.ToString());
        }

        public bool SetCache(string key, object value, DateTime expiresIn)
        {
            return _MemoryCacheHelper.Set(key, value, expiresIn);
        }

        public void Remove(string key)
        {
            _MemoryCacheHelper.Remove(key);
        }

        //public bool SetCache(string key, string value, TimeSpan expiresIn, bool isSliding = false)
        //{
        //    return _MemoryCacheHelper.Set(key, value, expiresIn, isSliding);
        //}

        //public bool SetCache<T>(string key, T value, TimeSpan expiresIn, bool isSliding = false)
        //{
        //    return _MemoryCacheHelper.Set(key, value, expiresIn, isSliding);
        //}
        
    }
}
