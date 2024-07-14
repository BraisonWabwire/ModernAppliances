// The `Refrigerator` class extends the `Appliance` base class and represents a specific type of appliance.
// It adds additional attributes specific to refrigerators such as `NumberOfDoors`, `Height`, and `Width`.

namespace ModernAppliances.Models
{
    public class Refrigerator : Appliance
    {
        // Additional properties specific to Refrigerator appliances
        public int NumberOfDoors { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        // Constructor to initialize a Refrigerator object with specified attributes, including base class attributes
        public Refrigerator(string itemNumber, string brand, int quantity, int wattage, string color, double price, int numberOfDoors, int height, int width)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            NumberOfDoors = numberOfDoors;
            Height = height;
            Width = width;
        }

        // Override of ToString method to provide a string representation of the Refrigerator object
        public override string ToString()
        {
            // Format: "ItemNumber;Brand;Quantity;Wattage;Color;Price;NumberOfDoors;Height;Width"
            return base.ToString() + $";{NumberOfDoors};{Height};{Width}";
        }
    }
}
