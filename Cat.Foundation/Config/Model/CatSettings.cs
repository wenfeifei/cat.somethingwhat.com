using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation.Config.Model
{
    /// <summary>
    /// CatSettings，喵喵看书系统配置相关项
    /// </summary>
    public class CatSettings
    {
        /// <summary>
        /// 项目前缀，用作与前端做身份验证时的key（值需与前端保持一致）
        /// </summary>
        public string ProjectPrefix { get; set; }
        /// <summary>
        /// 用户每天可以登录失败的次数
        /// </summary>
        public int AllowLoginFailTimes { get; set; }
        /// <summary>
        /// 登录凭证的保存时间(天)，过期后需要重新登录
        /// </summary>
        public int LogonCredentialSaveDay { get; set; }
        /// <summary>
        /// 日志使用，填写：MongoDbService 或 Log4NetService
        /// </summary>
        public string LogService { get; set; }
    }
}