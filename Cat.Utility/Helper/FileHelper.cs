using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Cat.Utility
{
    /// <summary>
    /// 文件帮助类
    /// </summary>
    public class FileHelper
    {
        /*
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                return Cat.Foundation.CatContext.HttpContext;
            }
        }
        */
        /// <summary>
        /// 目录分隔符
        /// </summary>
        private static string DirectorySeparatorChar => Path.DirectorySeparatorChar.ToString();
        /// <summary>
        /// 包含应用程序内容文件的目录的绝对路径
        /// </summary>
        private static string ContentRootPath => Cat.Foundation.CatContext.HostingEnvironment.ContentRootPath;
        /// <summary>
        /// 是否是绝对路径
        /// windows下判断 路径是否包含 ":"
        /// Mac OS、Linux下判断 路径是否包含 "\"
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static bool IsAbsolute(string path)
        {
            return Path.VolumeSeparatorChar == ':' ? path.IndexOf(Path.VolumeSeparatorChar) > 0 : path.IndexOf('\\') > 0;
        }
        /// <summary>
        /// 获取文件绝对路径
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string MapPath(string path)
        {
            return IsAbsolute(path) ? path : Path.Combine(ContentRootPath, path.TrimStart('~', '/').Replace("/", DirectorySeparatorChar));
        }
        /// <summary>
        /// 检测指定路径是否存在
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        /// <returns></returns>
        private static bool IsExist(string path, bool isDirectory)
        {
            return isDirectory ? Directory.Exists(IsAbsolute(path) ? path : MapPath(path)) : File.Exists(IsAbsolute(path) ? path : MapPath(path));
        }
        /// <summary>
        /// 文件是否存在
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        /// <returns></returns>
        public static bool IsExist(string path)
        {
            return File.Exists(IsAbsolute(path) ? path : MapPath(path));
        }
        /// <summary>
        /// 检测目录是否为空
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        private static bool IsEmptyDirectory(string path)
        {
            return Directory.GetFiles(IsAbsolute(path) ? path : MapPath(path)).Length <= 0 && Directory.GetDirectories(IsAbsolute(path) ? path : MapPath(path)).Length <= 0;
        }
        /// <summary>
        /// 创建文件并写入内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public static void CreateFiles(string path, Exception ex, Encoding encoding = null)
        {
            string content = ex.Message + "\r\n" + ex.StackTrace;
            CreateFiles(path, content, encoding);
        }
        /// <summary>
        /// 创建文件并写入内容
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        /// <param name="encoding"></param>
        public static void CreateFiles(string path, string content = "", Encoding encoding = null)
        {
            var filePath = IsAbsolute(path) ? path : MapPath(path);
            var dirPath = Path.GetDirectoryName(filePath);

            FileInfo file = new FileInfo(filePath);
            //文件夹不存在则自动创建
            Directory.CreateDirectory(dirPath);
            //创建文件
            FileStream fs = file.Create();
            Encoding encode = encoding ?? Encoding.Default;
            //获得字节数组
            byte[] data = encode.GetBytes(content);
            //开始写入
            fs.Write(data, 0, data.Length);
            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
            fs.Dispose();
        }
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="filePath">文件名</param>
        /// <param name="content">文件内容</param>
        public static void WriteFile(string filePath, string content, Encoding encoding = null)
        {
            try
            {
                var fs = new FileStream(filePath, FileMode.Create);
                Encoding encode = encoding ?? Encoding.Default;
                //获得字节数组
                byte[] data = encode.GetBytes(content);
                //开始写入
                fs.Write(data, 0, data.Length);
                //清空缓冲区、关闭流
                fs.Flush();
                fs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*
        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path"></param>
        public static void CreateFolder(string path)
        {
            CreateFiles(path, true);
        }
        /// <summary>
        /// 创建目录或文件
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是目录</param>
        private static void CreateFiles(string path, bool isDirectory)
        {
            try
            {
                if (!IsExist(path, isDirectory))
                {
                    if (isDirectory)
                        Directory.CreateDirectory(IsAbsolute(path) ? path : MapPath(path));
                    else
                    {
                        FileInfo file = new FileInfo(IsAbsolute(path) ? path : MapPath(path));
                        FileStream fs = file.Create();
                        fs.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */
    }
}
