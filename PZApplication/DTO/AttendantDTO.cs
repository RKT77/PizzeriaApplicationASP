using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.DTO
{
    public class AttendantDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
