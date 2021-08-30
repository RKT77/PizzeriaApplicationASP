using PizzeriaApplication.DTO;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMVC.Models
{
    public class OrderUpdate
    {
        public Status Status { get; set; }
        public OrderRequest OrderRequest { get; set; }
        public TableChangeRequest TableChangeRequest { get; set; }
        public ItemSubtractRequest ItemSubtractRequest { get; set; }
        public OrderDTO orderDTO { get; set; } 
        public IEnumerable<TableDTO> Tables { get; set; }
        public IEnumerable<ItemDTO> Items { get; set; }
        public IEnumerable<ItemDTO> ItemsAll { get; set; }
    }
}
