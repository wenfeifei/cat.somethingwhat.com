using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Models.ModelBinder.ActionModels
{
    /// <summary>
    /// 用于“更新”的参数实体
    /// </summary>
    public class Update_Book_User_Message_Input
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 不填写则默认发送给所有人，填写则指定openid发送
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }
    }
}
