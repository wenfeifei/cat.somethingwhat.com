using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Utility;
using Cat.Web.Support.Filter;
using Microsoft.AspNetCore.Mvc;

namespace Cat.Web.Areas.Book.Controllers
{
    [Area("Book")]
    [CatErrorFilter]
    [BookAuthorizeFilterAttribute]
    public class BaseController : Controller
    {
        public string Openid
        {
            get
            {
                string token = Cat.Foundation.CatContext.HttpContext.Request.Headers["cat-token"];
                token = AesHelper.AesDecrypt(token);
                var auth = Serializer.JsonDeserialize<Cat.M.Book.Models.ModelBinder.ReturnModels.BookAuth>(token);
                return auth.Openid;
            }
        }
    }
}