using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    internal abstract class Product
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public Product(string name, double price, int quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }
        public abstract bool IsExpired();
        public void DecreaseQuantity(int quantity)
        {
            if(quantity > Quantity)
                throw new ArgumentException("There is no sufficient quantity");
            Quantity -= quantity;
        }
        
    }
}
