using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Geekburger.Products.Contract
{
    public class ProductChanged
    {
        public ProductState State { get; set; }
        public Product Product { get; set; }
    }

    public enum ProductState
    {
        Deleted = 2,
        Modified = 3,
        Added = 4
    }

}
