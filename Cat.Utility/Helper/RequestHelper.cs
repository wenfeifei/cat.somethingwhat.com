using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Utility
{
    public class RequestHelper
    {
        #region 获取指定http请求的参数信息（GET、POST）
        /// <summary>
        /// 获取指定http请求的参数信息（GET、POST）
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns>json格式字符串</returns>
        public static string GetParmsToJsonStr(Microsoft.AspNetCore.Http.HttpRequest httpRequest)
        {
            return GetParms(httpRequest).ToJson();
        }
        /// <summary>
        /// 获取指定http请求的参数信息（GET、POST）
        /// </summary>
        /// <param name="httpRequest"></param>
        /// <returns>json格式字符串</returns>
        public static List<Dictionary<string, string>> GetParms(Microsoft.AspNetCore.Http.HttpRequest httpRequest)
        {
            //List<KeyValue> kvList = new List<KeyValue>();
            List<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
            if (httpRequest.Method.ToUpper() == "POST")
            {
                foreach (var item in httpRequest.Form)
                {
                    //kvList.Add(new KeyValue()
                    //{
                    //    Key = item.Key,
                    //    Value = item.Value
                    //});
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic[item.Key] = item.Value;
                    list.Add(dic);
                }
            }
            else if (httpRequest.Method.ToUpper() == "GET")
            {
                foreach (var item in httpRequest.Query)
                {
                    //kvList.Add(new KeyValue()
                    //{
                    //    Key = item.Key,
                    //    Value = item.Value
                    //});
                    Dictionary<string, string> dic = new Dictionary<string, string>();
                    dic[item.Key] = item.Value;
                    list.Add(dic);
                }
            }
            //return kvList.ToJson();
            return list;
        }
        #endregion

        /// <summary>
        /// 键值对实体
        /// </summary>
        private class KeyValue
        {
            /// <summary>
            /// 键
            /// </summary>
            public string Key { get; set; }
            /// <summary>
            /// 值
            /// </summary>
            public string Value { get; set; }
        }
    }
}