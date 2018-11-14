using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 访问页面。目前只提供按 page_visit_pv 排序的 top200。
    /// </summary>
    public class VisitPage
    {
        /// <summary>
        /// 日期，格式为 yyyymmdd
        /// </summary>
        public string ref_date { get; set; }
        /// <summary>
        /// 数据列表
        /// </summary>
        public List<TempObj> list { get; set; }


        public class TempObj
        {
            /// <summary>
            /// 页面路径
            /// </summary>
            public string page_path { get; set; }
            /// <summary>
            /// 访问次数
            /// </summary>
            public int page_visit_pv { get; set; }
            /// <summary>
            /// 访问人数
            /// </summary>
            public int page_visit_uv { get; set; }
            /// <summary>
            /// 次均停留时长
            /// </summary>
            public float page_staytime_pv { get; set; }
            /// <summary>
            /// 进入页次数
            /// </summary>
            public int entrypage_pv { get; set; }
            /// <summary>
            /// 退出页次数
            /// </summary>
            public int exitpage_pv { get; set; }
            /// <summary>
            /// 转发次数
            /// </summary>
            public int page_share_pv { get; set; }
            /// <summary>
            /// 转发人数
            /// </summary>
            public int page_share_uv { get; set; }
        }
    }
}
