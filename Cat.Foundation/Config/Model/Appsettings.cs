using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation.Config.Model
{
    /// <summary>
    /// Appsettings，全局配置相关项
    /// </summary>
    public class Appsettings
    {
        /// <summary>
        /// 项目配置
        /// </summary>
        public TempModel AspNetCore { get; set; }
        public class TempModel
        {
            /// <summary>
            /// 环境配置目录：develop、test、release ,分别对应开发环境、测试环境、生产环境
            /// </summary>
            public string Environment { get; set; }
        }
    }
}