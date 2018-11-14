using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.Foundation.Config.Model
{
    /// <summary>
    /// UploadSettings，上传文件配置相关项
    /// </summary>
    public class UploadSettings
    {
        /// <summary>
        /// 图片上传配置
        /// </summary>
        public TempModel Image { get; set; }

        public class TempModel
        {
            /// <summary>
            /// 上传大小限制，单位B
            /// </summary>
            public int MaxSize { get; set; }
            /// <summary>
            /// 上传图片格式限制，如：.png .jpg ...
            /// </summary>
            public List<string> AllowFiles { get; set; }
            /// <summary>
            /// 上传保存路径,可以自定义保存路径和文件名格式
            /// </summary>
            public string PathFormat { get; set; }
        }
    }
}