using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Searches
{
    public class TableSearch
    {
        [Range(1,50,ErrorMessage = "Not in the range")]
        public int? IdPizzeriaHall { get; set; }
        public bool? IsFree { get; set; }
    }
}
