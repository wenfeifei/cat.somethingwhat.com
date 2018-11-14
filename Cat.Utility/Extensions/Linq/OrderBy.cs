using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Utility
{
    public class OrderBy
    {
        /// <summary>
        /// 排序字段，如：Sort_Num
        /// </summary>
        public string Sort { get; set; }
        /// <summary>
        /// 排序方式，如：desc
        /// </summary>
        public string Order { get; set; }
    }
}
