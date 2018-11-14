using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cat.Web.Areas.Api.Models.Param
{
    public class List_Input<T> where T : class
    {
        public List<T> list { get; set; }
    }
}
