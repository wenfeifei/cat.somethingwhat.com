using Cat.M.Public.Models.ModelBinder.QueryModels;
using Cat.M.Public.Models.Table;
using Cat.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Public.Services
{
    /// <summary>
    /// 小程序支付订单 服务类
    /// </summary>
    public class WechatPayOrderService : AppBaseService<Wechat_PayOrder>
    {
        public Wechat_PayOrder GetByOutTradeNo(string OutTradeNo)
        {
            return base.GetSingle(w => w.OutTradeNo == OutTradeNo);
        }

        public int Add(Enums.Wechat.AppKey AppKey, string Appid, string Openid, string MchId, string OutTradeNo, string Body, string RequestData, int TotalFee, bool IsPaySuccessed, string PayResult, string prepay_id)
        {
            Wechat_PayOrder model = new Wechat_PayOrder();
            model.Id = StringHelper.GetUUID().ToString();
            model.Create_Time = DateTime.Now;

            model.FromKey = AppKey.ToString();
            model.Appid = Appid;
            model.Openid = Openid;
            model.MchId = MchId;
            model.OutTradeNo = OutTradeNo;
            model.Body = Body;
            model.RequestData = RequestData;
            model.TotalFee = TotalFee;
            model.IsPaySuccessed = IsPaySuccessed;
            model.PayResult = PayResult;
            model.PrepayId = prepay_id;

            return base.Add(model);
        }

        public int Update(Wechat_PayOrder entity)
        {
            entity.Update_Time = entity.Update_Time ?? DateTime.Now;
            return base.Update(entity);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="requestParams"></param>
        /// <returns></returns>
        public Models.Response.PageResult GetByPage(QueryPager_Wechat_PayOrder requestParams)
        {
            //查询表达式
            var exp = Cat.M.Public.Services.Helper.ExpressionHelper.GetExpressionByQueryPager<QueryPager_Wechat_PayOrder, Wechat_PayOrder>(requestParams);
            //排序
            IList<OrderBy> listOrderBy = new List<OrderBy>();
            if (!string.IsNullOrEmpty(requestParams.sorter))
                listOrderBy.Add(new OrderBy() { Order = requestParams.order, Sort = requestParams.sort });

            return base.GetByPage(requestParams.pn, requestParams.ps, exp, listOrderBy);
        }
    }
}
