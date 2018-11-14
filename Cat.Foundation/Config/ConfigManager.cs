using Cat.Enums;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation
{
    /// <summary>
    /// 配置文件管理
    /// </summary>
    public static class ConfigManager
    {
        /// <summary>
        /// 配置文件根目录
        /// </summary>
        public static string ConfigPath
        {
            get
            {
                return "/Configs";
            }
        }

        /// <summary>
        /// 当前应用的配置文件目录
        /// </summary>
        public static DevelopmentCatalog CurDevelopmentCatalog
        {
            get
            {
                string development = Appsettings.AspNetCore.Environment;
                DevelopmentCatalog developmentcatalog = (DevelopmentCatalog)Enum.Parse(typeof(DevelopmentCatalog), development.ToUpper());
                return developmentcatalog;
            }
        }

        /// <summary>
        /// 当前应用的配置文件目录名称
        /// </summary>
        public static string CurDevelopmentCatalogName
        {
            get
            {
                return CurDevelopmentCatalog.ToString();
            }
        }


        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="filename">文件名称</param>
        /// <returns></returns>
        private static IConfigurationRoot GetConfigurationRoot(string filename)
        {
            return GetConfigurationRootByPath(CatContext.HostingEnvironment.ContentRootPath + ConfigPath + "/" + CurDevelopmentCatalogName + "/" + filename);
        }

        /// <summary>
        /// 获取配置文件信息
        /// </summary>
        /// <param name="path">文件相对路径</param>
        /// <returns></returns>
        private static IConfigurationRoot GetConfigurationRootByPath(string path)
        {
            string full_path = path;
            if (path.StartsWith('/'))
                full_path = CatContext.HostingEnvironment.ContentRootPath + full_path;
            IConfigurationRoot configurationRoot = new ConfigurationBuilder().AddJsonFile(full_path, false, true).Build();
            return configurationRoot;
        }

        #region 自定义配置文件

        /// <summary>
        /// Appsettings，全局配置相关项
        /// </summary>
        public static Config.Model.Appsettings Appsettings
        {
            get
            {
                var configuration = GetConfigurationRootByPath(ConfigPath + "/appsettings.json");
                return configuration.Get<Config.Model.Appsettings>();
            }
        }

        /// <summary>
        /// ConnectionStrings，数据库连接相关项
        /// </summary>
        public static Config.Model.ConnectionStrings ConnectionStrings
        {
            get
            {
                var configuration = GetConfigurationRoot("ConnectionStrings.json");
                return configuration.Get<Config.Model.ConnectionStrings>();
            }
        }

        /// <summary>
        /// CacheSettings，缓存相关项
        /// </summary>
        public static Config.Model.CacheSettings CacheSettings
        {
            get
            {
                var configuration = GetConfigurationRoot("CacheSettings.json");
                return configuration.Get<Config.Model.CacheSettings>();
            }
        }

        /// <summary>
        /// BookSettings，缓存相关项
        /// </summary>
        public static Config.Model.BookSettings BookSettings
        {
            get
            {
                var configuration = GetConfigurationRoot("BookSettings.json");
                return configuration.Get<Config.Model.BookSettings>();
            }
        }

        /// <summary>
        /// CatSettings，缓存相关项
        /// </summary>
        public static Config.Model.CatSettings CatSettings
        {
            get
            {
                var configuration = GetConfigurationRoot("CatSettings.json");
                return configuration.Get<Config.Model.CatSettings>();
            }
        }

        /// <summary>
        /// UploadSettings，文件上传相关项
        /// </summary>
        public static Config.Model.UploadSettings UploadSettings
        {
            get
            {
                var configuration = GetConfigurationRoot("UploadSettings.json");
                return configuration.Get<Config.Model.UploadSettings>();
            }
        }

        #endregion
    }
}