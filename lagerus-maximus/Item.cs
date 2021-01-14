using System;
using System.Collections.Generic;
using System.Text;

namespace lagerus_maximus
{
    public class Item
    {
        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Category { get; set; }

        public int ItemNumber { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public Item()
        {

        }

        public Item(Item item)
        {
            Name = item.Name;
            Quantity = item.Quantity;
            Category= item.Category;
            ItemNumber = item.ItemNumber;

        }


    }
}
