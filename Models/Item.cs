using System.Collections.Generic;

namespace Domain
{
    public class Item:BaseEntity
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int IdItemType { get; set; }
        public ItemType ItemType { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public string Image { get; set; }
    }
}
