// The `Vacuum` class extends the `Appliance` base class and represents a specific type of appliance.
// It adds additional attributes specific to vacuums such as `Grade` and `BatteryVoltage`.

namespace ModernAppliances.Models
{
    public class Vacuum : Appliance
    {
        // Additional properties specific to Vacuum appliances
        public string Grade { get; set; }
        public int BatteryVoltage { get; set; }

        // Constructor to initialize a Vacuum object with specified attributes, including base class attributes
        public Vacuum(string itemNumber, string brand, int quantity, int wattage, string color, double price, string grade, int batteryVoltage)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            // Validate and set the battery voltage based on valid options
            if (batteryVoltage != 18 && batteryVoltage != 24)
            {
                throw new ArgumentException($"Invalid battery voltage for vacuum: {batteryVoltage}. Must be 18V or 24V.");
            }
            
            Grade = grade;
            BatteryVoltage = batteryVoltage;
        }

        // Override of ToString method to provide a string representation of the Vacuum object
        public override string ToString()
        {
            // Format: "ItemNumber;Brand;Quantity;Wattage;Color;Price;Grade;BatteryVoltage"
            return base.ToString() + $";{Grade};{BatteryVoltage}";
        }
    }
}
