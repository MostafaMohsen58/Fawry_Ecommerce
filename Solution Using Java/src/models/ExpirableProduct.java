package models;

import java.time.LocalDate;

public class ExpirableProduct extends  Product{
    private LocalDate expirationDate;
    public ExpirableProduct(String name, double price,int quantity,LocalDate expirationDate)
    {
        super(name,price,quantity);
        this.expirationDate = expirationDate;
    }
    public LocalDate getExpirationDate() {
        return expirationDate;
    }
    public void setExpirationDate(LocalDate expirationDate) {
        this.expirationDate = expirationDate;
    }

    @Override
    public boolean isExpired() {
        return LocalDate.now().isAfter(expirationDate);
    }

    @Override
    public String toString() {
        return getName() + " - $"+ String.format("%.2f",getPrice()) + getQuantity() +" in stock - Expires on "+getExpirationDate();
    }
}
