using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Services.Constants
{
    /// <summary>
    /// 场景值（数据分析）
    /// </summary>
    public class SceneByAnalysis
    {
        /// <summary>
        /// 根据场景值获取对应场景名称
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetName(int key)
        {
            string name = "其他";

            switch (key)
            {
                case 1: name = "小程序历史列表"; break;
                case 2: name = "搜索"; break;
                case 3: name = "会话"; break;
                case 4: name = "扫一扫二维码"; break;
                case 5: name = "公众号主页"; break;
                case 6: name = "聊天顶部"; break;
                case 7: name = "系统桌面"; break;
                case 8: name = "小程序主页"; break;
                case 9: name = "附近的小程序"; break;
                case 10: name = "其他"; break;
                case 11: name = "模板消息"; break;
                //case 12: name = ""; break;
                case 13: name = "公众号菜单"; break;
                case 14: name = "APP分享"; break;
                case 15: name = "支付完成页"; break;
                case 16: name = "长按识别二维码"; break;
                case 17: name = "相册选取二维码"; break;
                case 18: name = "公众号文章"; break;
                case 19: name = "钱包"; break;
                case 20: name = "卡包"; break;
                case 21: name = "小程序内卡券"; break;
                case 22: name = "其他小程序"; break;
                case 23: name = "其他小程序返回"; break;
                case 24: name = "卡券适用门店列表"; break;
                case 25: name = "搜索框快捷入口"; break;
                case 26: name = "小程序客服消息"; break;
                case 27: name = "公众号下发"; break;
                //case 28: name = ""; break;
                case 29: name = "任务栏-最近使用"; break;
                case 30: name = "长按小程序菜单圆点"; break;
                case 31: name = "连wifi成功页"; break;
                case 32: name = "城市服务"; break;
                case 33: name = "微信广告"; break;
                case 34: name = "其他移动应用"; break;
                case 35: name = "发现入口-我的小程序"; break; //发现入口-我的小程序（基础库2.2.4版本起1103场景值废弃，不影响此处统计结果）
                case 36: name = "任务栏-我的小程序"; break; //任务栏-我的小程序（基础库2.2.4版本起1104场景值废弃，不影响此处统计结果）
                default: name = "其他"; break;
            }

            return name;
        }

        /// <summary>
        /// 根据场景值获取对应场景名称（访问时长）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetNameByStaytime(int key)
        {
            string name = "其他";

            switch (key)
            {
                case 1: name = "0-2s"; break;
                case 2: name = "3-5s"; break;
                case 3: name = "6-10s"; break;
                case 4: name = "11-20s"; break;
                case 5: name = "20-30s"; break;
                case 6: name = "30-50s"; break;
                case 7: name = "50-100s"; break;
                case 8: name = "> 100s"; break;
            }

            return name;
        }

        /// <summary>
        /// 根据场景值获取对应场景名称（访问深度）
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetNameByDepth(int key)
        {
            string name = "其他";

            switch (key)
            {
                case 1: name = "1页"; break;
                case 2: name = "2页"; break;
                case 3: name = "3页"; break;
                case 4: name = "4页"; break;
                case 5: name = "5页"; break;
                case 6: name = "6-10页"; break;
                case 7: name = ">10页"; break;
            }

            return name;
        }
    }
}
