using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain
{
    public class Order:BaseEntity
    {
        public int IdTable { get; set; }
        public int IdAttendant { get; set; }
        public bool Active { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCancelled{ get; set; }
        public Attendant Attendant { get; set; }
        public Table Table { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
