using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Log.Models.ModelBinder.QueryModels
{
    /// <summary>
    /// 分页查询的条件实体
    /// </summary>
    public class QueryPager
    {
        public int pn { get; set; }
        public int ps { get; set; }
        public string sorter { get; set; }

        private string _sort;
        public string sort
        {
            get
            {
                if (string.IsNullOrEmpty(_sort) && !string.IsNullOrWhiteSpace(sorter))
                {
                    var idx = sorter.LastIndexOf("_");
                    _sort = sorter.Substring(0, idx);
                }
                return _sort;
            }
            set
            {
                _sort = value;
            }
        }
        //public string sort
        //{
        //    get { return _sort; }
        //    set
        //    {
        //        if (!string.IsNullOrWhiteSpace(sorter))
        //        {
        //            var idx = sorter.LastIndexOf("_");
        //            _sort = sorter.Substring(0, idx);
        //            order = sorter.Substring(idx + 1).Replace("end", string.Empty);
        //        }
        //    }
        //}

        public string _order { get; set; }
        public string order
        {
            get
            {
                if (string.IsNullOrEmpty(_order) && !string.IsNullOrWhiteSpace(sorter))
                {
                    var idx = sorter.LastIndexOf("_");
                    _order = sorter.Substring(idx + 1).Replace("end", string.Empty);
                }
                return _order;
            }
            set
            {
                _order = value;
            }
        }
    }
}
