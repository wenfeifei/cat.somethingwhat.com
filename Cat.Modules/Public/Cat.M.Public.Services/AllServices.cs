using Cat.M.Public.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Services
{
    public class AllServices
    {
        /// <summary>
        /// 微信帮助类
        /// </summary>
        public static WechatHelper WechatHelper => new WechatHelper();
        /// <summary>
        /// 小程序支付订单 服务类
        /// </summary>
        public static WechatPayOrderService WechatPayOrderService => new WechatPayOrderService();
        /// <summary>
        /// 微信小程序配置 服务类
        /// </summary>
        public static WechatAppConfigService WechatAppConfigService => new WechatAppConfigService();
        /// <summary>
        /// 后台账户 服务类
        /// </summary>
        public static SysAccountService SysAccountService => new SysAccountService();
        /// <summary>
        /// 后台账户登录日志 服务类
        /// </summary>
        public static SysLoginLogService SysLoginLogService => new SysLoginLogService();
        /// <summary>
        /// 接口白名单表 服务类
        /// </summary>
        public static SysInterfaceWhiteListService SysInterfaceWhiteListService => new SysInterfaceWhiteListService();
        /// <summary>
        /// 小程序相关接口 服务类
        /// </summary>
        public static MiniProgramService MiniProgramService => new MiniProgramService();
    }
}
