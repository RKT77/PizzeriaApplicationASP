using PizzeriaApplication.DTO;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMVC.Models
{
    public class CreateOrderModel
    {
        public OrderRequest OrderRequest { get; set; }
        public IEnumerable<ItemDTO> Items { get; set; }
        public IEnumerable<TableDTO> Tables { get; set; }
        public IEnumerable<AttendantDTO> Attendants { get; set; }
    }
}
