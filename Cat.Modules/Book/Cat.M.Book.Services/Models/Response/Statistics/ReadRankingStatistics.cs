using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Models.Response.Statistics
{
    /// <summary>
    /// 阅读排行榜
    /// </summary>
    public class ReadRankingStatistics
    {
        public string Openid { get; set; }
        public string NickName { get; set; }
        public int total { get; set; }
    }
}
