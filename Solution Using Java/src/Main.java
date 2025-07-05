import Service.ShippingService;
import models.*;

import java.time.LocalDate;
import java.util.ArrayList;
import java.util.List;

//TIP To <b>Run</b> code, press <shortcut actionId="Run"/> or
// click the <icon src="AllIcons.Actions.Execute"/> icon in the gutter.
public class Main {
    public static void checkout(Customer customer) {
        if(customer.getCart().isEmpty())
            throw new IllegalArgumentException("Cart is empty. Please add items to the cart before checking out.");
        double totalPrice=customer.getCart().getTotalPrice();
        if(totalPrice > customer.getBalance())
            throw new IllegalArgumentException("Insufficient balance to complete the purchase.");

        for(Item item:customer.getCart().getItems()){
            Product product = item.getProduct();
            if(product.getQuantity() < item.getQuantity())
                throw new IllegalArgumentException("Insufficient stock for "+product.getName()+" Available: "+product.getQuantity()+" , Requested: "+item.getQuantity());
            if(product.isExpired())
                throw new IllegalArgumentException("Cannot purchase expired product: "+product.getName());

            product.DecreaseQuantity(item.getQuantity());
        }

        customer.setBalance(customer.getBalance() - totalPrice);
        List<ShippableItemWithItemsCount> shippableItemWithItemsCountList =new ArrayList<ShippableItemWithItemsCount>();
        for(Item item:customer.getCart().getItems()){
            if(item.getProduct() instanceof IShippable)
                shippableItemWithItemsCountList.add(new ShippableItemWithItemsCount((IShippable) item.getProduct(),item.getQuantity()));
        }
        ShippingService.shipping(shippableItemWithItemsCountList);
        System.out.println(checkoutSummary(customer));
    }
    private static String checkoutSummary(Customer customer) {
        StringBuilder summary = new StringBuilder();

        summary.append("** Checkout receipt **\n");
        for (Item item : customer.getCart().getItems()) {
            summary.append(item.toString()).append("\n");
        }
        summary.append("\n==================\n");

        summary.append("** Checkout Summary **\n");
        summary.append(String.format("Subtotal: $%.2f\n", customer.getCart().getSubTotal()));
        summary.append(String.format("Shipping Cost: $%.2f\n", customer.getCart().getShippingCost()));
        summary.append(String.format("Total Price: $%.2f\n", customer.getCart().getTotalPrice()));
        summary.append(String.format("Checkout successful! Remaining balance: $%.2f\n", customer.getBalance()));

        return summary.toString();
    }

    public static void main(String[] args) {
        Product cheese = new ExpirableProduct("Cheese", 5.0, 10,LocalDate.now().plusDays(2));
        Product tv = new NonExpirableProduct("TV", 400.0,3);
        Product scratchCard = new NonExpirableProduct("Mobile Card", 2.0,4);

        Product shippableCheese = new ShipDecorator(cheese, 2); // 2g
        Product shippableTv = new ShipDecorator(tv, 5000); // 5000g

        // Successful case
        System.out.println("** Successful Checkout Test **\n");
        Customer customer = new Customer(1000);

        try {
            customer.addToCart(shippableCheese, 2);
            customer.addToCart(shippableTv, 1);
            customer.addToCart(scratchCard, 3);
            checkout(customer);
        } catch (Exception ex) {
            System.out.println("ERROR: " + ex.getMessage());
        }

        // Expired product
        System.out.println("\n\n** Expired Product Test **\n");
        Product testCheese = new ExpirableProduct("Cheese", 5.0, 10,LocalDate.now().minusDays(5));
        Product testTv = new NonExpirableProduct("TV", 400.0,3);
        Product testScratchCard = new NonExpirableProduct("Mobile Card", 2.0,4);

        Product shippableCheeseTest = new ShipDecorator(testCheese, 2);

        Customer customer2 = new Customer(1100);

        try {
            customer2.addToCart(shippableCheeseTest, 2);
            customer2.addToCart(testTv, 1);
            customer2.addToCart(testScratchCard, 3);
            checkout(customer2);
        } catch (Exception ex) {
            System.out.println("ERROR: " + ex.getMessage());
        }

        // Empty Cart
        System.out.println("\n\n** Empty Cart Test **\n");
        Customer customer3 = new Customer(1000);

        try {
            checkout(customer3);
        } catch (Exception ex) {
            System.out.println("ERROR: " + ex.getMessage());
        }

         //Insufficient stock
         System.out.println("\n\n** Insufficient Stock Test **\n");
         Customer customer4 = new Customer(1000);
         try {
             customer4.addToCart(shippableCheese, 11);
             customer4.addToCart(tv, 1);
             customer4.addToCart(scratchCard, 3);
             checkout(customer4);
         } catch (Exception ex) {
             System.out.println("ERROR: " + ex.getMessage());
         }

         // Insufficient balance
         System.out.println("\n\n** Insufficient Balance Test **\n");
         Customer customer5 = new Customer(300);
         try {
             customer5.addToCart(shippableCheese, 2);
             customer5.addToCart(tv, 1);
             customer5.addToCart(scratchCard, 3);
             checkout(customer5);
         } catch (Exception ex) {
             System.out.println("ERROR: " + ex.getMessage());
         }
    }
}