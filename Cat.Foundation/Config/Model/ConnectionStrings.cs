using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation.Config.Model
{
    /// <summary>
    /// ConnectionStrings，数据库连接相关项
    /// </summary>
    public class ConnectionStrings
    {
        /// <summary>
        /// 主数据库
        /// </summary>
        public TempModel CatCoreAppDB { get; set; }
        public class TempModel
        {
            public string DbProvider { get; set; }
            public string ConnectionStrings { get; set; }
        }

        /// <summary>
        /// Redis数据库
        /// </summary>
        public string RedisConnectionStrings { get; set; }

        /// <summary>
        /// Mongo数据库（用作日志数据库）
        /// </summary>
        public MongoModel MongoDB { get; set; }
        public class MongoModel
        {
            public string DBName { get; set; }
            public string ConnectionStrings { get; set; }
        }

    }
}