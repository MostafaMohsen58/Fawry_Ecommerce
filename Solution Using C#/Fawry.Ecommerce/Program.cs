using Fawry.Ecommerce.Service;
using System.Text;

namespace Fawry.Ecommerce
{
    internal class Program
    {
        public static void checkout(Customer customer)
        {
            if (customer.Cart.IsEmpty())
                throw new InvalidOperationException("Cart is empty. Please add items to the cart before checking out.");

            double totalPrice = customer.Cart.GetTotalPrice();

            if (totalPrice > customer.Balance)
                throw new InvalidOperationException("Insufficient balance to complete the purchase.");

            foreach (var item in customer.Cart.GetItems())
            {
                if (item.Product.Quantity < item.Quantity)
                    throw new InvalidOperationException($"Insufficient stock for {item.Product.Name}. Available: {item.Product.Quantity}, Requested: {item.Quantity}");
                if (item.Product.IsExpired())
                    throw new InvalidOperationException($"Cannot purchase expired product: {item.Product.Name}");

                item.Product.DecreaseQuantity(item.Quantity);
            }

            customer.Balance -= totalPrice;

            List<(IShippable shippableItems, int count)> shippableProductWithCount = new();
            foreach (var item in customer.Cart.GetItems())
            {
                if (item.Product is IShippable shippableProduct)
                {
                    shippableProductWithCount.Add((shippableProduct, item.Quantity));
                }
            }

            ShippingService.Shipping(shippableProductWithCount);

            Console.WriteLine(CheckoutSummary(customer));
        }
        private static string CheckoutSummary(Customer customer)
        {
            StringBuilder summary = new StringBuilder();

            summary.AppendLine("** Checkout receipt **");
            foreach (var item in customer.Cart.GetItems())
            {
                summary.AppendLine(item.ToString());
            }
            summary.AppendLine("\n==================\n");

            summary.AppendLine("** Checkout Summary **");
            summary.AppendLine($"Subtotal: ${customer.Cart.GetSubTotal():F2}");
            summary.AppendLine($"Shipping Cost: ${customer.Cart.GetShippingCost():F2}");
            summary.AppendLine($"Total Price: ${customer.Cart.GetTotalPrice():F2}");
            summary.AppendLine($"Checkout successful! Remaining balance: {customer.Balance:C}");

            return summary.ToString();
        }
        static void Main(string[] args)
        {
            
            var cheese = new ExpirableProduct("Cheese", 5.0, 10, DateTime.Now.AddDays(2));
            var tv = new NonExpirableProduct("TV", 400.0, 3);
            var scratchCard = new NonExpirableProduct("Mobile Card", 2.0, 4);

            var shippableCheese = new ShipDecorator(cheese, 2);// 2 => wieght in gram
            var shippableTv = new ShipDecorator(tv, 5000); // 5000 g


            #region Successful case
            Console.WriteLine("** Successful Checkout Test **\n");
            var customer = new Customer(1000);

            try
            {
                customer.AddToCart(shippableCheese, 2);
                customer.AddToCart(shippableTv, 1);
                customer.AddToCart(scratchCard, 3);
                checkout(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            #endregion


            #region Expired product
            Console.WriteLine("\n\n** Expired Product Test **\n");
            var Testcheese = new ExpirableProduct("Cheese", 5.0, 10, DateTime.Now.AddDays(-5));
            var shippableCheeseTest = new ShipDecorator(Testcheese, 2);

            var customer2 = new Customer(1000);

            try
            {
                customer2.AddToCart(shippableCheeseTest, 2);
                customer2.AddToCart(tv, 1);
                customer2.AddToCart(scratchCard, 3);
                checkout(customer2);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            #endregion


            #region Empty Cart
            Console.WriteLine("\n\n** Empty Cart Test **\n");
            var customer3 = new Customer(1000);

            try
            {
                checkout(customer3);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            #endregion


            #region Insufficient stock
            Console.WriteLine("\n\n** Insufficient Stock Test **\n");
            var customer4 = new Customer(1000);
            try
            {
                customer4.AddToCart(shippableCheese, 11);
                customer4.AddToCart(tv, 1);
                customer4.AddToCart(scratchCard, 3);
                checkout(customer4);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            #endregion


            #region Insufficient balance
            Console.WriteLine("\n\n** Insufficient Balance Test **\n");
            var customer5 = new Customer(300);
            try
            {
                customer.AddToCart(shippableCheese, 2);
                customer.AddToCart(tv, 1);
                customer.AddToCart(scratchCard, 3);
                checkout(customer);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR: " + ex.Message);
            }
            #endregion
        }
    }
}
