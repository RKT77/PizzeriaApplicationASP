using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.DTO
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(30, ErrorMessage = "Max length is 30")]
        [MinLength(3, ErrorMessage = "Min length is 3")]
        public string Name { get; set; }
    }
}
