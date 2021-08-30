using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PizzeriaApplication.Searches
{
    public class AttendantSearch
    {
        [Range(1, 50, ErrorMessage = "Not in the range")]
        public int? IdRole { get; set; }
    }
}
