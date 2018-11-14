using System;
using System.Collections.Generic;
using System.Text;

namespace Cat.M.Book.Services.Models.Response.Statistics
{
    public class DateStatistics
    {
        public int y { get; set; }
        public int m { get; set; } = 1;
        public int d { get; set; } = 1;
        public int total { get; set; }
    }
}
