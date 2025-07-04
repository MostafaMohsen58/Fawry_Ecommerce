using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    internal class Cart
    {
        List<Item> Items { get; }
        public Cart()
        {
            Items = new List<Item>();
        }
        public List<Item> GetItems()
        {
            return Items;
        }
        public void AddItem(Product product, int quantity)
        {

            var item = Items.FirstOrDefault(i => i.Product.Name == product.Name);

            //check if the product is already in the cart
            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                Items.Add(new Item(product, quantity));
            }
            //product.DecreaseQuantity(quantity);
        }
        public bool IsEmpty()
        {
            return Items.Count == 0;
        }
        public double GetSubTotal() //Get total price of all items in the cart with out shipping cost
        {
            return Items.Sum(i =>i.GetTotalPrice());
        }
        public double GetShippingCost()
        {
            
            return Items.Where(i => i.Product is IShippable)
                .Sum(i => ((IShippable)i.Product).GetWeight()>1000 ? ((IShippable)i.Product).GetWeight()/1000 * 10: 5 );
            //I Assume shipping cost 10EGP per Kilogram and 5EGP for weight less than 1KG
        }
        public double GetTotalPrice()
        {
            return GetSubTotal() + GetShippingCost();
        }
        public List<IShippable> GetShippableItems()
        {
            return Items.Where(i => i.Product is IShippable)
                        .Select(i => (IShippable)i.Product)
                        .ToList();
        }

    }
}
