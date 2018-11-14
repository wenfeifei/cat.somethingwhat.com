using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 用户小程序访问分布数据
    /// </summary>
    public class VisitDistribution
    {
        /// <summary>
        /// 日期，格式为 yyyymmdd
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<item_list_model> list { get; set; }


        public class KeyValueModel
        {
            public int key { get; set; }
            public int value { get; set; }
        }

        public class item_list_model
        {
            /// <summary>
            /// 分布类型
            /// access_source_session_cnt、access_staytime_info、access_depth_info
            /// </summary>
            public string index { get; set; }
            /// <summary>
            /// 分布数据列表
            /// </summary>
            public List<KeyValueModel> item_list { get; set; }
        }
    }
}
