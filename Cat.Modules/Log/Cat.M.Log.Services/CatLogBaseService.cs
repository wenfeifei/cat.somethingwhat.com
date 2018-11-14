using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Cat.M.Log.Services
{
    public class CatLogBaseService<T> : AppBaseService<T> where T : Cat.M.Log.Models.BaseEntity
    {
        public CatLogBaseService() : base(Cat.Foundation.ConfigManager.ConnectionStrings.MongoDB.DBName)
        {

        }
    }    

}
