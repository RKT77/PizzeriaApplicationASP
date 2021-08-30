using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public string Table { get; set; }
        public int IdTable { get; set; }
        public string FullName { get; set; }
        public double TotalPrice { get; set; }
        public List<OrderItemsDTO> Items { get; set; }
    }
    public enum Status { paid, cancelled, active}
}
