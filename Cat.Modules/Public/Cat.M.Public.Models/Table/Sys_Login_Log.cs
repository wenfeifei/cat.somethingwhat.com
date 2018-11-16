using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Public.Models.Table
{
    /// <summary>
    /// 登录日志表
    /// </summary>
    public partial class Sys_Login_Log : BaseEntity
    {
        [NotMapped]
        public string Sort_Num { get; set; }
        [NotMapped]
        public new DateTime? Update_Time { get; set; }


        /// <summary>
        /// 用户id
        /// </summary>
        public string User_Id { get; set; }
        /// <summary>
        /// 客户端ip
        /// </summary>
        public string Client_IP { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 地区
        /// </summary>
        public string District { get; set; }
        /// <summary>
        /// 为追踪日志（Trace）提供针对当前请求的唯一标识
        /// </summary>
        public string TraceIdentifier { get; set; }
        /// <summary>
        /// 是否登录成功
        /// </summary>
        [Column(TypeName = "bit")]
        public bool? IsSuccessed { get; set; }
    }
}
