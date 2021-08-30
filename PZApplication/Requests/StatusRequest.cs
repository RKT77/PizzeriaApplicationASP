using PizzeriaApplication.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Requests
{
    public class StatusRequest
    {
        [Required(ErrorMessage = "This is a required field")]
        public Status status { get; set; }
    }
}
