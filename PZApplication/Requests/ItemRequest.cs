using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Requests
{
    public class ItemRequest
    {
        [Required(ErrorMessage = "This is a required field")]
        [MaxLength(20, ErrorMessage = "Name too long")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public int ItemTypeId { get; set; }
        [Required(ErrorMessage = "This is a required field")]
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
