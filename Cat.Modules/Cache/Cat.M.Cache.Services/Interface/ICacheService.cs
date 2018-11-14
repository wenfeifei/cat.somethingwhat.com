using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Cache.Services
{
    public interface ICacheService
    {
        /// <summary>
        /// 判断key是否存在
        /// </summary>
        bool Exists(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetCache(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetCache<T>(string key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="expiresIn"></param>
        /// <returns></returns>
        bool SetCache(string key, object value, DateTime expiresIn);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">缓存Key</param>
        /// <returns></returns>
        void Remove(string key);
    }
}
