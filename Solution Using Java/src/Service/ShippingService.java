package Service;

import models.IShippable;
import models.ShippableItemWithItemsCount;

import java.util.List;

public class ShippingService {
    public static void shipping(List<ShippableItemWithItemsCount> items){
        double totalWeight = 0;
        System.out.println("** Shipment Notice **");
        for(ShippableItemWithItemsCount item:items){
            IShippable  shippable = item.getItem();
            int count = item.getCount();
            double itemWeight = shippable.getWeight() * count;
            totalWeight += itemWeight;
            System.out.println(count+"x "+shippable.getName()+ " "+ itemWeight+"g");
        }

        if (totalWeight < 1000) {
            System.out.println("Total package weight "+ totalWeight +" g\n");
        }
        else{
            System.out.println("Total package weight "+ (totalWeight/1000) +" kg\n");
        }
    }
}
