package models;

public class Customer {
    private double balance;
    private final Cart cart;

    public Customer(double balance) {
        this.balance = balance;
        this.cart = new Cart();
    }
    public double getBalance() {
        return balance;
    }
    public void setBalance(double balance) {
        this.balance = balance;
    }
    public Cart getCart() {
        return cart;
    }
    public void addToCart(Product product,int quantity) {
        cart.addItem(product,quantity);
    }
}
