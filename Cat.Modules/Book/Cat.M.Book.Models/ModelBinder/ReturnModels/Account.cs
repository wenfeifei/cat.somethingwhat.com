using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Models.ModelBinder.ReturnModels
{
    /// <summary>
    /// 用户账户信息
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 总的喵币
        /// </summary>
        public int MM_Currency_Sum { get; set; }
        /// <summary>
        /// 已用的喵币
        /// </summary>
        public int MM_Currency_Used { get; set; }
        /// <summary>
        /// 当前剩余喵币
        /// </summary>
        public int MM_Currency_Current { get; set; }
        /// <summary>
        /// 购买的喵币
        /// </summary>
        public int MM_Currency_Buy { get; set; }
        /// <summary>
        /// 赠送的喵币
        /// </summary>
        public int MM_Currency_Give { get; set; }
        /// <summary>
        /// 元可以兑换多少喵币
        /// </summary>
        public int MM_Currency_Ratio { get; set; }
    }
}
