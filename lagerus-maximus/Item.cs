using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Media.Imaging;

namespace lagerus_maximus
{
    public class Item
    {
        //public BitmapImage Image { get; set; } 

        public string ImagePath { get; set; }

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
            ImagePath = item.ImagePath;

        }


    }
}
