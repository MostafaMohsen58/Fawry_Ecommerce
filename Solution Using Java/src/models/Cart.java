package models;

import java.util.ArrayList;
import java.util.List;

public class Cart {
    private final List<Item> items;
    public Cart () {
        this.items = new ArrayList<>();
    }
    public List<Item> getItems() {
        return items;
    }
    public  void addItem(Product product,int quantity){
        Item item=null;
        //check if product is already in cart
        for(Item i:items){
            if(i.getProduct().getName().equals(product.getName())){
                item=i;
                break;
            }
        }
        if(item != null){
            item.setQuantity(item.getQuantity()+quantity);
        }
        else {
            items.add(new Item(product, quantity));
        }
    }

    public boolean isEmpty() {
        return items.isEmpty();
    }

    //Get total price of all items in cart without shipping cost
    public double getSubTotal() {
        double subtotal=0;
        for(Item i:items){
            subtotal+=i.getTotalPrice();
        }
        return subtotal;
    }
    public double getShippingCost() {
        double sum=0;
        for(Item i:items){
            if(i.getProduct() instanceof IShippable){
                double weight=((IShippable)i.getProduct()).getWeight();
                sum+= weight > 1000 ? (weight/1000) *10 : 5; //I assume shipping cost 10 per Kilogram and 5 for weight less than 1KG
            }
        }
        return sum;
    }
    public double getTotalPrice() {
        return getSubTotal()+getShippingCost();
    }
}
