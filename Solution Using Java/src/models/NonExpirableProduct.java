package models;

public class NonExpirableProduct extends Product {
    public NonExpirableProduct(String name,double price, int quantity) {
        super(name,price,quantity);
    }
    @Override
    public boolean isExpired() {
        return false;
    }
}
