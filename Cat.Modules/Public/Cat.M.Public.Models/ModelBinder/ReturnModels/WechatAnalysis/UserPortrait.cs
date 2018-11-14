using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 小程序新增或活跃用户的画像分布数据
    /// </summary>
    public class UserPortrait
    {
        /// <summary>
        /// 时间范围，如："20170611-20170617"
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 新用户画像
        /// </summary>
        public visit_uv_new_model visit_uv_new { get; set; }
        /// <summary>
        /// 活跃用户画像
        /// </summary>
        public visit_uv_new_model visit_uv { get; set; }

        public class KeyValueModel
        {
            /// <summary>
            /// 属性值id
            /// </summary>
            public int id { get; set; }
            /// <summary>
            /// 属性值名称，与id对应。如属性为 province 时，返回的属性值名称包括「广东」等。
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 该场景访问uv
            /// </summary>
            public int value { get; set; }
        }

        public class visit_uv_new_model
        {
            /// <summary>
            /// 省份
            /// </summary>
            public List<KeyValueModel> province { get; set; }
            /// <summary>
            /// 城市
            /// </summary>
            public List<KeyValueModel> city { get; set; }
            /// <summary>
            /// 性别
            /// </summary>
            public List<KeyValueModel> genders { get; set; }
            /// <summary>
            /// 移动终端平台
            /// </summary>
            public List<KeyValueModel> platforms { get; set; }
            /// <summary>
            /// 机型
            /// </summary>
            public List<KeyValueModel> devices { get; set; }
            /// <summary>
            /// 年龄段
            /// </summary>
            public List<KeyValueModel> ages { get; set; }
        }

        public string query_data { get; set; }
    }
}
