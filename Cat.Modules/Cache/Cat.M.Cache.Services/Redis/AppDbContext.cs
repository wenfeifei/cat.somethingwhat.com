using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Cache.Services.Redis
{
    public class AppDbContext
    {
        private static ConnectionMultiplexer instance;
        private static readonly object locker = new object();

        /// <summary>
        /// 单例模式获取redis连接实例
        /// </summary>
        public static ConnectionMultiplexer Instance
        {
            get
            {
                lock (locker)
                {

                    if (instance == null)
                    {
                        if (instance == null)
                            instance = ConnectionMultiplexer.Connect(Cat.Foundation.ConfigManager.ConnectionStrings.RedisConnectionStrings);
                    }
                }
                return instance;
            }
        }
    }
}
