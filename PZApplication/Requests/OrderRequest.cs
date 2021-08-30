using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Requests
{
    public class OrderRequest
    {
        [Required(ErrorMessage = "This is a required field")]
        public int IdItem { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public int IdTable { get; set; }
        public int? IdAttendant { get; set; }
    }
}
