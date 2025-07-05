package models;

public class ShipDecorator extends Product implements IShippable{
    private final Product product;
    private final double weight;
    public ShipDecorator(Product product,double weight) {
        super(product.getName(),product.getPrice(),product.getQuantity());
        this.product = product;
        this.weight = weight;
    }

    @Override
    public boolean isExpired() {
        return product.isExpired();
    }
    @Override
    public String getName(){
        return product.getName();
    }
    @Override
    public double getWeight() {
        return weight;
    }
}
