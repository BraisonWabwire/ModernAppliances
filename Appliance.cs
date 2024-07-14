// The `Appliance` class represents a base class for various types of appliances.
// It encapsulates common attributes such as item number, brand, quantity, wattage,
// color, and price that are shared among different appliance types.

using System;

namespace ModernAppliances.Models
{
    public abstract class Appliance
    {
        // Properties representing attributes of the appliance
        public string ItemNumber { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
        public int Wattage { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }

        // Constructor to initialize the appliance object with specified attributes
        public Appliance(string itemNumber, string brand, int quantity, int wattage, string color, double price)
        {
            ItemNumber = itemNumber;
            Brand = brand;
            Quantity = quantity;
            Wattage = wattage;
            Color = color;
            Price = price;
        }

        // Override of ToString method to provide a string representation of the appliance
        public override string ToString()
        {
            // Format: "ItemNumber;Brand;Quantity;Wattage;Color;Price"
            return $"{ItemNumber};{Brand};{Quantity};{Wattage};{Color};{Price}";
        }
    }
}
