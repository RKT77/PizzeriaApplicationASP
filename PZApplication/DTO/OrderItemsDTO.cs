using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.DTO
{
    public class OrderItemsDTO
    {
        public string ItemName { get; set; }
        public int ItemNumber { get; set; }
        public double TotalItemsPrice { get; set; }
    }
}
