using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Products.Contract
{
    public class Product
    {
        public string StoreName { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<Item> Items { get; set; }
        public decimal Price { get; set; }
    }

    public class Item
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
    }

}
