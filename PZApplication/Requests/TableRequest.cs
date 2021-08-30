using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Requests
{
    public class TableRequest
    {
        [Required(ErrorMessage = "This is a required range")]
        [MaxLength(20, ErrorMessage = "Name too long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [Range(1,10,ErrorMessage ="Not in value range")]
        public int IdHall { get; set; }
    }
}
