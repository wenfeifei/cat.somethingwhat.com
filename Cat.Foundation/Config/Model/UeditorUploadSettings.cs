using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation.SettingsModel
{
    /// <summary>
    /// Ueditor上传文件配置相关项，以 ueditor_config.json 为准
    /// </summary>
    public class UeditorUploadSettings
    {
        #region 图片
        /// <summary>
        /// 上传图片大小限制，单位B，默认2MB
        /// </summary>
        public int ImageMaxSize { get; set; }
        /// <summary>
        /// 上传图片保存路径
        /// upload/images/{yyyy}{mm}{dd}/{time}{rand:6}
        /// </summary>
        public string ImagePathFormat { get; set; }
        /// <summary>
        /// 上传图片格式限制
        /// </summary>
        public List<string> ImageAllowFiles { get; set; }
        #endregion

        #region 文件
        /// <summary>
        /// 上传文件大小限制，单位B，默认50MB
        /// </summary>
        public int FileMaxSize { get; set; }
        /// <summary>
        /// 上传文件保存路径
        /// </summary>
        public string FilePathFormat { get; set; }
        /// <summary>
        /// 上传文件格式限制
        /// </summary>
        public List<string> FileAllowFiles { get; set; }
        #endregion

        #region 视频
        /// <summary>
        /// 上传视频大小限制，单位B，默认100MB
        /// </summary>
        public int videoMaxSize { get; set; }
        /// <summary>
        /// 上传视频保存路径
        /// </summary>
        public string videoPathFormat { get; set; }
        /// <summary>
        /// 上传视频格式限制
        /// </summary>
        public List<string> videoAllowFiles { get; set; }
        #endregion

    }
}