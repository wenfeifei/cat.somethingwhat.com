using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    public class AllServices
    {
        /// <summary>
        /// 微信支付相关 服务类
        /// </summary>
        public static WechatPayService WechatPayService => new WechatPayService();
        /// <summary>
        /// 书本相关搜索 服务类
        /// </summary>
        public static BookSearchService BookSearchService => new BookSearchService();
        /// <summary>
        /// 书本阅读记录 服务类
        /// </summary>
        public static BookReadRecordService BookReadRecordService => new BookReadRecordService();
        /// <summary>
        /// 书本章节阅读记录 服务类
        /// </summary>
        public static BookChapterReadRecordService BookChapterReadRecordService => new BookChapterReadRecordService();
        /// <summary>
        /// book用户 服务类
        /// </summary>
        public static BookUserService BookUserService => new BookUserService();
        /// <summary>
        /// 用户偏好设置 服务类
        /// </summary>
        public static BookUserPreferenceService BookUserPreferenceService => new BookUserPreferenceService();
        /// <summary>
        /// 用户消费记录表 服务类
        /// </summary>
        public static BookUserConsumeService BookUserConsumeService => new BookUserConsumeService();
        /// <summary>
        /// 喵币充值记录 服务类
        /// </summary>
        public static BookUserRechargeService BookUserRechargeService => new BookUserRechargeService();
        /// <summary>
        /// 系统信息表 服务类
        /// </summary>
        public static BookUserMessageService BookUserMessageService => new BookUserMessageService();
        /// <summary>
        /// 用户阅读系统信息记录表 服务类
        /// </summary>
        public static BookUserMessageReadRecordService BookUserMessageReadRecordService => new BookUserMessageReadRecordService();
        /// <summary>
        /// 报表统计 服务类
        /// </summary>
        public static StatisticsService StatisticsService => new StatisticsService();
    }
}
