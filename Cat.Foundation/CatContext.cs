using System;
using Microsoft.AspNetCore.Hosting;

namespace Cat.Foundation
{
    public static class CatContext
    {

        internal static IHostingEnvironment _HostingEnvironment;

        public static IHostingEnvironment HostingEnvironment
        {
            get
            {
                return _HostingEnvironment;
            }
        }


        internal static IServiceProvider _ServiceProvider;

        public static Microsoft.AspNetCore.Http.HttpContext HttpContext
        {
            get
            {
                object factory = _ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
                return context;
            }
        }
    }

    public static class MyExtensions
    {
        public static IServiceProvider UseMySvp(this IServiceProvider svp)
        {
            CatContext._ServiceProvider = svp;
            return svp;
        }
        public static IHostingEnvironment UseMyEnv(this IHostingEnvironment env)
        {
            CatContext._HostingEnvironment = env;
            return env;
        }
    }

}