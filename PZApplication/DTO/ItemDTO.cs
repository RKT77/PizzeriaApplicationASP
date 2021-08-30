using System;
using System.Collections.Generic;
using System.Text;

namespace PizzeriaApplication.DTO
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
    }
}
