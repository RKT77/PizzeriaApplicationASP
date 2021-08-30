using PizzeriaApplication.DTO;
using PizzeriaApplication.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PizzeriaMVC.Models
{
    public class CreateItemModel
    {
        public ItemRequest Item { get; set; }
        public IEnumerable<ItemTypeDTO> ItemTypes { get; set; }
    }
}
