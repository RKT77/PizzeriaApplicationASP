using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Requests
{
    public class TableChangeRequest
    {
        [Required(ErrorMessage = "This is a required field")]
        public int IdTable { get; set; }
    }
}
