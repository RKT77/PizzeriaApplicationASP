using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class ItemType:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
