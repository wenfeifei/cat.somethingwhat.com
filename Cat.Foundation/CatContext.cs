using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

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

        /*
        internal static IServiceProvider _ServiceProvider;
        public static Microsoft.AspNetCore.Http.HttpContext HttpContext
        {
            get
            {
                //object factory = _ServiceProvider.GetService(typeof(Microsoft.AspNetCore.Http.IHttpContextAccessor));
                //Microsoft.AspNetCore.Http.HttpContext context = ((Microsoft.AspNetCore.Http.HttpContextAccessor)factory).HttpContext;
                //return context;
            }
        }
        */

        internal static IApplicationBuilder _IApplicationBuilder;
        public static Microsoft.AspNetCore.Http.HttpContext HttpContext
        {
            get
            {
                var httpContextAccessor = _IApplicationBuilder.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
                return httpContextAccessor.HttpContext;

            }
        }
    }

    public static class MyExtensions
    {
        /*
        public static IServiceProvider UseMySvp(this IServiceProvider svp)
        {
            CatContext._ServiceProvider = svp;
            return svp;
        }
        */
        public static IApplicationBuilder UseMyApp(this IApplicationBuilder app)
        {
            CatContext._IApplicationBuilder = app;
            return app;
        }
        public static IHostingEnvironment UseMyEnv(this IHostingEnvironment env)
        {
            CatContext._HostingEnvironment = env;
            return env;
        }
    }

}