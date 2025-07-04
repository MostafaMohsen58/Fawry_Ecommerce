using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    internal class Item
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public Item(Product product,int quantity)
        {
            Product = product;
            Quantity = quantity;
        }
        public double GetTotalPrice()
        {
            return Product.Price * Quantity;
        }

        public override string ToString()
        {
            return $" {Quantity}x  {Product.Name}  {GetTotalPrice():C}";
        }
    }
}
