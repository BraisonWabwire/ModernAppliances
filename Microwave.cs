// The `Microwave` class extends the `Appliance` base class and represents a specific type of appliance.
// It adds additional attributes specific to microwaves such as `Capacity` and `RoomType`.

namespace ModernAppliances.Models
{
    public class Microwave : Appliance
    {
        // Additional properties specific to Microwave appliances
        public double Capacity { get; }
        public string RoomType { get; }

        // Constructor to initialize a Microwave object with specified attributes, including base class attributes
        public Microwave(string itemNumber, string brand, int quantity, int wattage, string color, double price, double capacity, string roomType)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Capacity = capacity;
            RoomType = roomType;
        }

        // Override of ToString method to provide a string representation of the Microwave object
        public override string ToString()
        {
            // Format: "ItemNumber;Brand;Quantity;Wattage;Color;Price;Capacity;RoomType"
            return base.ToString() + $";{Capacity};{RoomType}";
        }
    }
}
