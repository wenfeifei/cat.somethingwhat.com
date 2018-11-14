using Cat.Enums;
using Cat.Foundation;
using Cat.M.Book.Models.Table;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(""); //需要引用 Microsoft.EntityFrameworkCore
            //optionsBuilder.UseMySQL("");     //需要引用 MySql.Data.EntityFrameworkCore

            #region 设置要链接的数据库
            //获取数据库类型、连接字符串
            string dbProvider_provider = ConfigManager.ConnectionStrings.CatCoreAppDB.DbProvider.ToUpper();
            string connStr = ConfigManager.ConnectionStrings.CatCoreAppDB.ConnectionStrings;
            switch ((DbProvider)Enum.Parse(typeof(DbProvider), dbProvider_provider))
            {
                case DbProvider.MYSQL:
                    optionsBuilder.UseMySQL(connStr);
                    break;
                case DbProvider.SQLSERVER:
                    optionsBuilder.UseSqlServer(connStr);
                    break;
                case DbProvider.ORACLE:
                    throw new Exception("当前未提供连接到Oracle的示例，需要自己实现");
                default:
                    throw new Exception("当前配置的数据库连接字符串必须指定provider");
            }
            #endregion

        }

        private DbSet<Book_Read_Record> Book_Read_Record { get; set; }
        private DbSet<Book_Chapter_Read_Record> Book_Chapter_Read_Record { get; set; }
        private DbSet<Book_User> Book_User { get; set; }
        private DbSet<Book_User_Preference> Book_User_Preference { get; set; }
        private DbSet<Book_User_Recharge> Book_User_Recharge { get; set; }
        private DbSet<Book_User_Consume> Book_User_Consume { get; set; }
        private DbSet<Book_User_Message> Book_User_Message { get; set; }
        private DbSet<Book_User_Message_ReadRecord> Book_User_Message_ReadRecord { get; set; }

    }
}
