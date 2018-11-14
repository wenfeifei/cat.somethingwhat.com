using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.M.Public.Services.Constants;
using Cat.Utility;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Api.Controllers
{
    /// <summary>
    /// 文件上传控制器
    /// </summary>
    public class UploadController : BaseController
    {
        /// <summary>
        /// antd Upload 组件所需的数据响应格式
        /// </summary>
        public class UploadRes
        {
            public string uid { get; set; }
            public string name { get; set; }
            public string status { get; set; }
            public string url { get; set; }
            public string errmsg { get; set; }
        }

        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        //[ApiAuthorizeFilterAttribute(AuthorityIdentity = M.Public.Services.Constants.AuthorityIdentityEnum.Administrator)]
        public UploadRes Image()
        {
            var uploadRes = new UploadRes()
            {
                uid = StringHelper.GetUUID().ToString(),
                name = string.Empty,
                status = "success",
                url = string.Empty,
                errmsg = string.Empty
            };

            try
            {
                if (!base.IsAdministrator)
                {
                    //非管理员操作，需要验证上传文件的大小
                    int maxLength = 50 * 1024; //单位：B
                    foreach (var file in Request.Form.Files)
                    {
                        if (file.Length > maxLength)
                        {
                            throw new Exception($"非管理员上传，文件大小必须在 {maxLength / 1024}KB 以内");
                        }
                    }
                }

                uploadRes.url = M.Public.Services.UploadHelper.ImageHandler(Request.Form.Files);
                uploadRes.name = uploadRes.url.Substring(uploadRes.url.LastIndexOf("/") + 1);
            }
            catch (Exception ex)
            {
                uploadRes.status = "fail";
                uploadRes.errmsg = ex.Message;
            }

            return uploadRes;
        }
    }
}