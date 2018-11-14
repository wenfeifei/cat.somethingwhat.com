using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Cat.M.Book.Models.Table
{
    /// <summary>
    /// 用户偏好表
    /// </summary>
    public partial class Book_User_Preference : BaseEntity
    {
        [NotMapped]
        public new string Sort_Num { get; set; }

        /// <summary>
        /// Openid
        /// </summary>
        public string Openid { get; set; }
        /// <summary>
        /// 字体大小
        /// </summary>
        public int FontSize { get; set; }
        /// <summary>
        /// 字体颜色
        /// </summary>
        public string FontColor { get; set; }
        /// <summary>
        /// 背景色
        /// </summary>
        public string BackgroundColor { get; set; }
        /// <summary>
        /// 字体
        /// </summary>
        public string FontFamily { get; set; }
        /// <summary>
        /// 屏幕亮度（0~10），0最暗，10最亮
        /// </summary>
        public int ScreenBrightness { get; set; }
        /// <summary>
        /// 是否屏幕常亮
        /// </summary>
        public bool? KeepScreenOn { get; set; }
    }
}
