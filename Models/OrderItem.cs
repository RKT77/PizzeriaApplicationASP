using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class OrderItem
    {
        public int IdOrder { get; set; }
        public int IdItem{ get; set; }
        public int ItemsNumber { get; set; }
        public double ItemPrice { get; set; }
        public Order Order { get; set; }
        public Item Item { get; set; }
    }
}
