// The `Dishwasher` class extends the `Appliance` base class and represents a specific type of appliance.
// It adds additional attributes specific to dishwashers such as `Feature` and `SoundRating`.

namespace ModernAppliances.Models
{
    public class Dishwasher : Appliance
    {
        // Additional properties specific to Dishwasher appliances
        public string Feature { get; set; }
        public string SoundRating { get; set; }

        // Constructor to initialize a Dishwasher object with specified attributes, including base class attributes
        public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, double price, string feature, string soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Feature = feature;
            SoundRating = soundRating;
        }

        // Override of ToString method to provide a string representation of the Dishwasher object
        public override string ToString()
        {
            // Format: "ItemNumber;Brand;Quantity;Wattage;Color;Price;Feature;SoundRating"
            return base.ToString() + $";{Feature};{SoundRating}";
        }
    }
}
