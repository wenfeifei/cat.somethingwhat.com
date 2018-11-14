using Cat.Utility;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Cat.M.Book.Services
{
    public class SqlService
    {
        private AppDbContext db;
        public SqlService()
        {
            db = new AppDbContext();
        }

        public int ExecuteSqlCommand(RawSqlString sql, params object[] parameters)
        {
            return db.Database.ExecuteSqlCommand(sql, parameters);
        }

        public IList<T> SqlQuery<T>(string sql, params object[] parameters) where T : class
        {
            return SqlQuery<T>(new AppDbContext(), sql, parameters);
        }

        public IList<T> SqlQuery<T>(DbContext context, string sql, params object[] parameters) where T : class
        {
            var concurrencyDetector = context.Database.GetService<IConcurrencyDetector>();
            using (concurrencyDetector.EnterCriticalSection())
            {
                var rawSqlCommand = context.Database.GetService<IRawSqlCommandBuilder>().Build(sql, parameters);
                RelationalDataReader query = rawSqlCommand.RelationalCommand.ExecuteReader(context.Database.GetService<IRelationalConnection>(), parameterValues: rawSqlCommand.ParameterValues);
                var list = ListHelper.DataReaderToList<T>(query.DbDataReader);
                query.Dispose();
                return list;
            }
        }

    }
}
