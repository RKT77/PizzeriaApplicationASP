using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Table:BaseEntity
    {
        public string Name { get; set; }
        public int IdPizzeriaHall { get; set; }
        public PizzeriaHall PizzeriaHall { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
