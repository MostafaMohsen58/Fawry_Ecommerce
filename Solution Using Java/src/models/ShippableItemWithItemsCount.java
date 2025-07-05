package models;

public class ShippableItemWithItemsCount {
    private final IShippable item;
    private final int count;
    public ShippableItemWithItemsCount(IShippable item, int count) {
        this.item = item;
        this.count = count;
    }
    public IShippable getItem() {
        return item;
    }
    public int getCount() {
        return count;
    }
}
