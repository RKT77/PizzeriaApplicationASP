using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Searches
{
    public class ItemSearch
    {
        [Range(1,100,ErrorMessage ="Value out of range")]
        public int? IdItemType { get; set; }
        [Range(1,1000,ErrorMessage = "Value out of range")]
        public int? IdOrder { get; set; }
        [StringLength(40,ErrorMessage ="Text too long")]
        public string Keyword { get; set; }
        public int PerPage { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
    }
}
