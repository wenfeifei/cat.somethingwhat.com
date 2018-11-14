using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 用户访问小程序数据日趋势
    /// </summary>
    public class DailyVisitTrend
    {
        public List<TempModel> list { get; set; }
        public class TempModel
        {
            /// <summary>
            /// 日期，格式为 yyyymmdd
            /// </summary>
            public string ref_date { get; set; }
            /// <summary>
            /// 打开次数
            /// </summary>
            public int session_cnt { get; set; }
            /// <summary>
            /// 访问次数
            /// </summary>
            public int visit_pv { get; set; }
            /// <summary>
            /// 访问人数
            /// </summary>
            public int visit_uv { get; set; }
            /// <summary>
            /// 新用户数
            /// </summary>
            public int visit_uv_new { get; set; }
            /// <summary>
            /// 人均停留时长 (浮点型，单位：秒)
            /// </summary>
            public float stay_time_uv { get; set; }
            /// <summary>
            /// 次均停留时长 (浮点型，单位：秒)
            /// </summary>
            public float stay_time_session { get; set; }
            /// <summary>
            /// 平均访问深度 (浮点型)
            /// </summary>
            public float visit_depth { get; set; }
        }
    }
}
