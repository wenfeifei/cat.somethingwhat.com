using System;
using System.IO;
using Cat.Foundation;
using log4net;
using log4net.Config;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Cat.Web
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(
                options =>
                {
                    //设置不使用默认的驼峰式命名
                    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
                    //设置输出json文本时的时间格式
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                }
                );

            //services.UseMyConfig();
            
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider svp)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "area",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


            env.UseMyEnv();
            svp.UseMySvp();



            /* 暂时用不上
            //配置静态文件路径
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(env.ContentRootPath + SettingsManager.SiteSettings.PhysicalFileProvider),
                RequestPath = new Microsoft.AspNetCore.Http.PathString(SettingsManager.SiteSettings.PhysicalFileProvider)
            });
            */

            //log4net 配置
            ILoggerRepository repository = LogManager.CreateRepository("RollingLogFileAppender");
            XmlConfigurator.Configure(repository, new FileInfo("Configs/log4net.config"));

        }
    }
}
