using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cat.Foundation;
using Cat.Utility;
using Microsoft.AspNetCore.Mvc;

using AllLogServices = Cat.M.Log.Services.AllServices;

namespace Cat.Web.Areas.Web.Controllers
{
    public class HomeController : BaseController
    {
        [Route("")]
        public IActionResult Index()
        {
            //try
            //{
            //    int a = 0;
            //    int b = 10 / a;
            //}
            //catch (Exception ex)
            //{
            //    AllLogServices.LogService.Debug(ex.Message, ex);
            //}

            return View();
        }
    }
}