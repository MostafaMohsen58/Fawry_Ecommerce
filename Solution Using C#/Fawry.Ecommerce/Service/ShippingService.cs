using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce.Service
{
    public static class ShippingService
    {
        public static void Shipping(List<(IShippable t,int count)> items)
        {
            //var totalWeight = products.Sum(p => p.GetWeight());
            //Console.WriteLine("** Shipping the following products **");
            //foreach (var product in products)
            //{
            //    Console.WriteLine(product);
            //}
            //Console.WriteLine($"Total package weight {totalWeight} kg\n\n================\n");
            double totalWeight = 0;
            Console.WriteLine("** Shipment Notice **");
            foreach (var (item, quantity) in items)
            {
                double itemWeight = item.GetWeight() * quantity;
                Console.WriteLine($"{quantity}x {item.GetName()} {itemWeight}g");
                totalWeight += itemWeight;
            }

            if (totalWeight < 1000)
                Console.WriteLine($"Total Package Weight {totalWeight} g\n");
            else
                Console.WriteLine($"Total Package Weight {totalWeight / 1000} kg\n");

        }
    }
}
