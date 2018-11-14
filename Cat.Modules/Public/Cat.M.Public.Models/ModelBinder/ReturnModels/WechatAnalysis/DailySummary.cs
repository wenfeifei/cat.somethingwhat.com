using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 用户访问小程序数据概况
    /// </summary>
    public class DailySummary
    {
        public List<TempModel> list { get; set; }
        public class TempModel
        {
            /// <summary>
            /// 日期，格式为 yyyymmdd
            /// </summary>
            public string ref_date { get; set; }
            /// <summary>
            /// 累计用户数
            /// </summary>
            public int visit_total { get; set; }
            /// <summary>
            /// 转发次数
            /// </summary>
            public int share_pv { get; set; }
            /// <summary>
            /// 转发人数
            /// </summary>
            public int share_uv { get; set; }
        }
    }
}
