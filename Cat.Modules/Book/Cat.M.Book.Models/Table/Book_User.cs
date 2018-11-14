using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    /// <summary>
    /// Book用户表
    /// </summary>
    public partial class Book_User : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }

        /// <summary>
        /// 微信小程序的appid
        /// </summary>
        public string Appid { get; set; }
        /// <summary>
        /// 用户id（mm开头+递增的用户总数）
        /// </summary>
        public string User_Id { get; set; }
        /// <summary>
        /// Openid
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// NickName
        /// </summary>
        public string NickName { get; set; }
        /// <summary>
        /// AvatarUrl
        /// </summary>
        public string AvatarUrl { get; set; }
        /// <summary>
        /// Gender
        /// </summary>
        public string Gender { get; set; }
        /// <summary>
        /// Country
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// Province
        /// </summary>
        public string Province { get; set; }
        /// <summary>
        /// City
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// Language
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Currency
        /// </summary>
        public int Currency { get; set; }
        /// <summary>
        /// 阅读时长（分钟）
        /// </summary>
        public int Read_Minute { get; set; }
    }
}
