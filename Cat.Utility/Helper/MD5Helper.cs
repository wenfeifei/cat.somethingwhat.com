using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Cat.Utility
{
    public class MD5Helper
    {
        /// <summary>
        /// 对字符串进行MD5加密
        /// 32位，大写
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string MD5(string text)
        {
            try
            {
                MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(MD5.ComputeHash(Encoding.GetEncoding(Encoding.UTF8.BodyName).GetBytes(text))).Replace("-", "");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
