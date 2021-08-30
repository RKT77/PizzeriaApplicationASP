using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.Responses
{
    public class PagedResponse<T>
    {
        public int TotalCount { get; set; }
        public int PagesCount { get; set; }
        public int CurrentPage { get; set; }
        public IEnumerable<T> Data { get; set; }
    }
}
