using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Cache.Services
{
    public class CacheService : ICacheService
    {
        Enum.CacheSelector cacheSelector = Enum.CacheSelector.MSCache;
        ICacheService service;
        public CacheService()
        {
            InitService();
        }

        private void InitService()
        {
            cacheSelector = (Enum.CacheSelector)System.Enum.Parse(typeof(Enum.CacheSelector), Cat.Foundation.ConfigManager.CacheSettings.CacheModule);
            //应用哪种缓存
            switch (cacheSelector)
            {
                case Enum.CacheSelector.MSCache:
                    service = new MSCache.MemoryCacheService();
                    break;
                case Enum.CacheSelector.Redis:
                    service = new Redis.RedisService();
                    break;

                default:
                    throw new Exception("找不到缓存服务类，请检查配置");
            }
        }

        /// <summary>
        /// 多久后过期
        /// </summary>
        public enum Expires
        {
            一小时,
            一天,
            七天,
            一个月
        }

        /// <summary>
        /// 缓存key是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Exists(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("key is null");
            return service.Exists(key);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetCache(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("key is null");
            return service.GetCache(key);
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetCache<T>(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("key is null");
            return service.GetCache<T>(key);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool SetCache(string key, object value, Expires expires = Expires.一小时)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("key is null");
            DateTime expiresIn;
            switch (expires)
            {
                case Expires.一小时:
                    expiresIn = DateTime.Now.AddHours(1);
                    break;
                case Expires.一天:
                    expiresIn = DateTime.Now.AddDays(1);
                    break;
                case Expires.七天:
                    expiresIn = DateTime.Now.AddDays(7);
                    break;
                case Expires.一个月:
                    expiresIn = DateTime.Now.AddMonths(1);
                    break;
                default:
                    expiresIn = DateTime.Now.AddDays(1);
                    break;
            }
            return SetCache(key, value, expiresIn);
        }

        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        public bool SetCache(string key, object value, DateTime expiresIn)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("key is null");
            return service.SetCache(key, value, expiresIn);
        }

        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        public void Remove(string key)
        {
            if (string.IsNullOrEmpty(key)) throw new Exception("key is null");
            service.Remove(key);
        }
    }
}
