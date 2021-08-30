using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Requests
{
    public class ItemSubtractRequest
    {
        [Required(ErrorMessage = "This is a required field")]
        public int IdItem { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [Range(0,1,ErrorMessage ="Not a valid value")]
        public int DeleteAll { get; set; }
    }
}
