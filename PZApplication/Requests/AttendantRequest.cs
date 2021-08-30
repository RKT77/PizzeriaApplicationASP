using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Requests
{
    public class AttendantRequest
    {
        [Required(ErrorMessage ="This is a required field")]
        [MaxLength(20,ErrorMessage ="Name too long")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(20, ErrorMessage = "Name too long")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [Range(1,10,ErrorMessage ="Must be in range from 1 to 10")]
        public int IdRole { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(70, ErrorMessage = "Name too long")]
        public string Email { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public string Password { get; set; }
    }
}
