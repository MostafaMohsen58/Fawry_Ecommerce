using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    internal class NonExpirableProduct:Product
    {
        public NonExpirableProduct(string name, double price, int quantity) : base(name, price, quantity)
        {
            
        }
        public override bool IsExpired()
        {
            return false;
        }
    }
}
