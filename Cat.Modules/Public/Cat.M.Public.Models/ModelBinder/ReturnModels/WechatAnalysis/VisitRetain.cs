using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Models.ModelBinder.ReturnModels.WechatAnalysis
{
    /// <summary>
    /// 用户访问小程序留存
    /// </summary>
    public class VisitRetain
    {
        public string ref_date { get; set; }
        public List<int> visit_uv { get; set; }
        public List<int> visit_uv_new { get; set; }
    }
}
