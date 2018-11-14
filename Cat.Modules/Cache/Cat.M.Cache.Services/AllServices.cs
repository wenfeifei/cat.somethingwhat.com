using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Cache.Services
{
    public class AllServices
    {
        /// <summary>
        /// 缓存 服务类
        /// </summary>
        public static CacheService CacheService => new CacheService();
    }
}
