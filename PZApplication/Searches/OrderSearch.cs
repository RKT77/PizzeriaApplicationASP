using PizzeriaApplication.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Searches
{
    public class OrderSearch
    {
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        [Range(1, 100, ErrorMessage = "Value out of range")]
        public int? IdTable { get; set; }
        [Range(1, 100, ErrorMessage = "Value out of range")]
        public int? IdAttendant { get; set; }
        public Status? Status { get; set; } 
    }
}
