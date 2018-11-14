using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 用户小程序访问分布数据
    /// </summary>
    public class VisitDistributionMulti
    {
        /// <summary>
        /// 日期，格式为 yyyymmdd
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// access_source_session_cnt
        /// </summary>
        public List<KeyValueModel> access_source_visit_pv { get; set; }
        /// <summary>
        /// access_source_visit_uv
        /// </summary>
        public List<KeyValueModel> access_source_visit_uv { get; set; }
        /// <summary>
        /// access_staytime_info
        /// </summary>
        public List<KeyValueModel> access_staytime_info { get; set; }
        /// <summary>
        /// access_depth_info
        /// </summary>
        public List<KeyValueModel> access_depth_info { get; set; }


        public class KeyValueModel
        {
            public int key { get; set; }
            public int value { get; set; }
            public string name { get; set; }
        }
    }
}
