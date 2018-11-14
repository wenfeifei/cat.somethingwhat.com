using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 用户访问小程序日留存
    /// </summary>
    public class DailyRetain
    {
        /// <summary>
        /// 日期，格式为 yyyymmdd
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 新增用户留存
        /// </summary>
        public List<KeyValueModel> visit_uv_new { get; set; }
        /// <summary>
        /// 活跃用户留存
        /// </summary>
        public List<KeyValueModel> visit_uv { get; set; }

        public class KeyValueModel
        {
            public int key { get; set; }
            public int value { get; set; }
        }
    }
}
