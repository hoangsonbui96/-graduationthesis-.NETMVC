using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Models
{
    public class ShoppingCart
    {
        public OrderHeader OrderHeader { get; set; }

        public List<Product> Products { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }
    }
}
