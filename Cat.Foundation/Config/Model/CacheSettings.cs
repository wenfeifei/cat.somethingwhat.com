using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation.Config.Model
{
    /// <summary>
    /// CacheSettings，缓存配置相关项
    /// </summary>
    public class CacheSettings
    {
        /// <summary>
        /// 应用哪种缓存，这里填写“MSCache”或“Redis”
        /// </summary>
        public string CacheModule { get; set; }
    }
}