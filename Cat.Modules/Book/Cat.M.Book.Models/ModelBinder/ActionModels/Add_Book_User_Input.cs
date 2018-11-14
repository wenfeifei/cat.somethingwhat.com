using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Models.ModelBinder.ActionModels
{
    /// <summary>
    /// 用于“新增”的参数实体
    /// </summary>
    public class Add_Book_User_Input
    {
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
    }
}
