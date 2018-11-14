using Cat.M.Book.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    /// <summary>
    /// 用户阅读系统信息记录表 服务类
    /// </summary>
    public class BookUserMessageReadRecordService : AppBaseService<Book_User_Message_ReadRecord>
    {
        /// <summary>
        /// 新增用户阅读系统信息记录
        /// </summary>
        /// <param name="Book_User_Message_Id"></param>
        /// <param name="Openid"></param>
        /// <returns></returns>
        public int Add(List<string> Book_User_Message_Ids, string Openid)
        {
            List<Book_User_Message_ReadRecord> entities = new List<Book_User_Message_ReadRecord>();
            foreach (var Book_User_Message_Id in Book_User_Message_Ids)
            {
                entities.Add(new Book_User_Message_ReadRecord()
                {
                    Book_User_Message_Id = Book_User_Message_Id,
                    Openid = Openid,
                    Create_Time = DateTime.Now
                });
            }

            return base.Add(entities);
        }
    }
}
