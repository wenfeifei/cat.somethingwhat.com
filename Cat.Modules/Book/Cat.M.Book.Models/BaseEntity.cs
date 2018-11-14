using Cat.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Cat.M.Book.Models
{
    public class BaseEntity
    {
        /// <summary>
        /// 主键列
        /// </summary>
        [Key]
        public string Id { get; set; }
        /// <summary>
        /// 排序号
        /// </summary>
        public long? Sort_Num { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? Create_Time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? Update_Time { get; set; }

        public BaseEntity()
        {
            //Id = StringHelper.GuidTo16String(); //16位GUID
            //Create_Time = DateTime.Now;
            //Update_Time = DateTime.Now;
        }
    }
}
