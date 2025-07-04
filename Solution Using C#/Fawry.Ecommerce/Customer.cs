using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    internal class Customer
    {
        public double Balance { get; set; }
        public Cart Cart { get; }
        public Customer(double Balance)
        {
            this.Balance = Balance;
            Cart = new Cart();
        }
        public void AddToCart(Product product, int quantity)
        {
            Cart.AddItem(product, quantity);
        }
        
    }
}
