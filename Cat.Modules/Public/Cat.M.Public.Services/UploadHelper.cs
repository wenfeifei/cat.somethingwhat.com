using Cat.Foundation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cat.M.Public.Services
{
    /// <summary>
    /// 文件上传帮助类
    /// </summary>
    public class UploadHelper
    {
        private static bool CheckFileType(string filename, List<string> allowExtensions)
        {
            var fileExtension = Path.GetExtension(filename).ToLower();
            return allowExtensions.Select(x => x.ToLower()).Contains(fileExtension);
        }

        private static bool CheckFileSize(int size, int maxSize)
        {
            return size < maxSize;
        }

        /// <summary>
        /// 图片上传处理
        /// </summary>
        /// <param name="Files"></param>
        /// <returns></returns>
        public static string ImageHandler(IFormFileCollection Files)
        {
            var imageSettings = ConfigManager.UploadSettings.Image;

            var files = Files;
            var file = files[0];

            string savePath = Helper.PathFormatter.Format(file.FileName, imageSettings.PathFormat);
            string localPath = Path.Combine(Cat.Foundation.CatContext.HostingEnvironment.WebRootPath, savePath);

            string ex = Path.GetExtension(file.FileName);

            if (!CheckFileType(file.FileName, imageSettings.AllowFiles))
            {
                throw new Exception("不允许上传的图片格式");
            }
            if (!CheckFileSize((int)file.Length, imageSettings.MaxSize))
            {
                throw new Exception($"上传的图片超过大小[{imageSettings.MaxSize / 1024 / 1024}MB]");
            }

            string fileDir = localPath.Substring(0, localPath.LastIndexOf("/"));
            string fileName = localPath.Substring(localPath.LastIndexOf("/") + 1);

            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }
            //file.SaveAs(Path.Combine(localPath, filePathName));
            using (var stream = new FileStream(localPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return savePath;
        }
    }
}
