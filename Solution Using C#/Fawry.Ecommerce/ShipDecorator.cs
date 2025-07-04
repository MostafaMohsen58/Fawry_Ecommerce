using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    internal class ShipDecorator:Product,IShippable
    {
        private readonly Product _product;
        private readonly double _weight;
        public ShipDecorator(Product product,double weight) : base(product.Name, product.Price, product.Quantity)
        {
            _product = product;
            _weight = weight;
        }
        public override bool IsExpired()
        {
            return _product.IsExpired();
        }
        public string GetName()
        {
            return _product.Name;
        }

        public double GetWeight()
        {
            return  _weight;
        }
    }
}
