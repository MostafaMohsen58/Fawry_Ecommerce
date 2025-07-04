using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawry.Ecommerce
{
    internal class ExpirableProduct:Product
    {
        public DateTime ExpirationDate { get; set; }
        public ExpirableProduct(string name, double price, int quantity, DateTime expirationDate)
            : base(name, price, quantity)
        {
            ExpirationDate = expirationDate;
        }
        public override bool IsExpired()
        {
            return DateTime.Now > ExpirationDate;
        }

        public override string ToString()
        {
            return $"{Name} - {Price:C} - {Quantity} in stock - Expires on {ExpirationDate.ToShortDateString()}";
        }
    }
}
