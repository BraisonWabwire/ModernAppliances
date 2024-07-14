using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ModernAppliances.Models;

namespace ModernAppliances
{
    class Program
    {
        static void Main(string[] args)
        {
            var applianceManager = new ApplianceManager();
            applianceManager.LoadAppliances("appliances.txt");

            while (true)
            {
                Console.WriteLine("Welcome to Modern Appliances!");
                Console.WriteLine("How may we assist you?");
                Console.WriteLine("1 - Check out appliance");
                Console.WriteLine("2 - Find appliances by brand");
                Console.WriteLine("3 - Display appliances by type");
                Console.WriteLine("4 - Produce random appliance list");
                Console.WriteLine("5 - Save & exit");
                Console.Write("Enter option: ");
                var option = Console.ReadLine();

                switch (option)
                {
                    case "1":
                        CheckOutAppliance(applianceManager);
                        break;
                    case "2":
                        FindAppliancesByBrand(applianceManager);
                        break;
                    case "3":
                        DisplayAppliancesByType(applianceManager);
                        break;
                    case "4":
                        ProduceRandomApplianceList(applianceManager);
                        break;
                    case "5":
                        applianceManager.SaveAppliances("appliances.txt");
                        return;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        static void CheckOutAppliance(ApplianceManager applianceManager)
        {
            Console.Write("Enter the item number of an appliance: ");
            var itemNumber = Console.ReadLine();

            if (string.IsNullOrEmpty(itemNumber))
            {
                Console.WriteLine("Item number cannot be empty.");
                return;
            }

            var appliance = applianceManager.FindApplianceByItemNumber(itemNumber);

            if (appliance == null)
            {
                Console.WriteLine("No appliances found with that item number.");
                return;
            }

            if (appliance.Quantity > 0)
            {
                appliance.Quantity--;
                Console.WriteLine($"Appliance \"{appliance.ItemNumber}\" has been checked out.");
            }
            else
            {
                Console.WriteLine("The appliance is not available to be checked out.");
            }
        }

        static void FindAppliancesByBrand(ApplianceManager applianceManager)
        {
            Console.Write("Enter brand to search for: ");
            var brand = Console.ReadLine();

            if (string.IsNullOrEmpty(brand))
            {
                Console.WriteLine("Brand cannot be empty.");
                return;
            }

            var appliances = applianceManager.FindAppliancesByBrand(brand);

            if (appliances.Count == 0)
            {
                Console.WriteLine($"No appliances found with brand name {brand}.");
                return;
            }

            Console.WriteLine("Matching Appliances:");
            foreach (var appliance in appliances)
            {
                Console.WriteLine(appliance);
            }
        }

        static void DisplayAppliancesByType(ApplianceManager applianceManager)
        {
            Console.WriteLine("Appliance Types:");
            Console.WriteLine("1 - Refrigerators");
            Console.WriteLine("2 - Vacuums");
            Console.WriteLine("3 - Microwaves");
            Console.WriteLine("4 - Dishwashers");
            Console.Write("Enter type of appliance: ");
            var typeOption = Console.ReadLine();
            Type? applianceType = typeOption switch
            {
                "1" => typeof(Refrigerator),
                "2" => typeof(Vacuum),
                "3" => typeof(Microwave),
                "4" => typeof(Dishwasher),
                _ => null,
            };

            if (applianceType == null)
            {
                Console.WriteLine("Invalid type selected.");
                return;
            }

            if (applianceType == typeof(Refrigerator))
            {
                Console.Write("Enter number of doors: 2 (double door), 3 (three doors) or 4 (four doors): ");
                var numberOfDoors = Console.ReadLine();

                if (string.IsNullOrEmpty(numberOfDoors))
                {
                    Console.WriteLine("Number of doors cannot be empty.");
                    return;
                }

                var appliances = applianceManager.FindRefrigeratorsByNumberOfDoors(numberOfDoors);
                Console.WriteLine("Matching refrigerators:");
                foreach (var appliance in appliances)
                {
                    Console.WriteLine(appliance);
                }
            }
            else if (applianceType == typeof(Vacuum))
            {
                Console.Write("Enter battery voltage value. 18 V (low) or 24 V (high): ");
                var batteryVoltage = Console.ReadLine();

                if (string.IsNullOrEmpty(batteryVoltage))
                {
                    Console.WriteLine("Battery voltage cannot be empty.");
                    return;
                }

                var appliances = applianceManager.FindVacuumsByBatteryVoltage(batteryVoltage);
                Console.WriteLine("Matching vacuums:");
                foreach (var appliance in appliances)
                {
                    Console.WriteLine(appliance);
                }
            }
            else if (applianceType == typeof(Microwave))
            {
                Console.Write("Room where the microwave will be installed: K (kitchen) or W (work site): ");
                var roomType = Console.ReadLine();

                if (string.IsNullOrEmpty(roomType))
                {
                    Console.WriteLine("Room type cannot be empty.");
                    return;
                }

                var appliances = applianceManager.FindMicrowavesByRoomType(roomType);
                Console.WriteLine("Matching microwaves:");
                foreach (var appliance in appliances)
                {
                    Console.WriteLine(appliance);
                }
            }
            else if (applianceType == typeof(Dishwasher))
            {
                Console.Write("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet) or M (Moderate): ");
                var soundRating = Console.ReadLine();

                if (string.IsNullOrEmpty(soundRating))
                {
                    Console.WriteLine("Sound rating cannot be empty.");
                    return;
                }

                var appliances = applianceManager.FindDishwashersBySoundRating(soundRating);
                Console.WriteLine("Matching dishwashers:");
                foreach (var appliance in appliances)
                {
                    Console.WriteLine(appliance);
                }
            }
        }

        static void ProduceRandomApplianceList(ApplianceManager applianceManager)
        {
            Console.Write("Enter number of appliances: ");
            if (!int.TryParse(Console.ReadLine(), out var count) || count <= 0)
            {
                Console.WriteLine("Invalid number.");
                return;
            }

            var randomAppliances = applianceManager.GetRandomAppliances(count);
            Console.WriteLine("Random appliances:");
            foreach (var appliance in randomAppliances)
            {
                Console.WriteLine(appliance);
            }
        }
    }

    public class ApplianceManager
    {
        private List<Appliance> appliances;

        public ApplianceManager()
        {
            appliances = new List<Appliance>();
        }

        public void LoadAppliances(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var data = line.Split(';');
                if (data.Length < 7) continue;

                var itemNumber = data[0];
                var brand = data[1];

                if (!int.TryParse(data[2], out var quantity))
                {
                    Console.WriteLine($"Invalid quantity: {data[2]} for item: {itemNumber}");
                    continue;
                }

                if (!int.TryParse(data[3], out var wattage))
                {
                    Console.WriteLine($"Invalid wattage: {data[3]} for item: {itemNumber}");
                    continue;
                }

                var color = data[4];

                if (!double.TryParse(data[5], out var price))
                {
                    Console.WriteLine($"Invalid price: {data[5]} for item: {itemNumber}");
                    continue;
                }

                Appliance? appliance = null;

                // Determine appliance type based on item number prefix and specific attributes
                if (itemNumber.StartsWith("3"))
                {
                    // Microwave
                    if (data.Length < 8 || !double.TryParse(data[6], out var capacity) || string.IsNullOrEmpty(data[7]))
                    {
                        Console.WriteLine($"Invalid microwave data for item: {itemNumber}");
                        continue;
                    }

                    var roomType = data[7];
                    appliance = new Microwave(itemNumber, brand, quantity, wattage, color, price, capacity, roomType);
                }
                else if (itemNumber.StartsWith("2"))
                {
                    // Vacuum
                    if (data.Length < 8)
                    {
                        Console.WriteLine($"Invalid vacuum data for item: {itemNumber}");
                        continue;
                    }

                    var grade = data[6];
                    if (!int.TryParse(data[7], out var batteryVoltage))
                    {
                        Console.WriteLine($"Invalid battery voltage for vacuum: {itemNumber}");
                        continue;
                    }

                    appliance = new Vacuum(itemNumber, brand, quantity, wattage, color, price, grade, batteryVoltage);
                }
                else if (int.TryParse(data[6], out var numberOfDoors) && data.Length >= 9)
                {
                    // Refrigerator
                    if (!int.TryParse(data[7], out var height) || !int.TryParse(data[8], out var width))
                    {
                        Console.WriteLine($"Invalid dimensions for refrigerator: {itemNumber}");
                        continue;
                    }

                    appliance = new Refrigerator(itemNumber, brand, quantity, wattage, color, price, numberOfDoors, height, width);
                }
                else if (data[6] == "Clean with Steam" || data[6] == "Third Rack" || data[6] == "Finger Print Resistant")
                {
                    // Dishwasher
                    if (data.Length < 8)
                    {
                        Console.WriteLine($"Invalid dishwasher data for item: {itemNumber}");
                        continue;
                    }

                    var feature = data[6];
                    var soundRating = data[7];
                    appliance = new Dishwasher(itemNumber, brand, quantity, wattage, color, price, feature, soundRating);
                }
                else
                {
                    // Unknown appliance type
                    Console.WriteLine($"Unknown appliance type for item: {itemNumber}");
                    continue;
                }

                if (appliance != null)
                {
                    appliances.Add(appliance);
                }
            }
        }

        public void SaveAppliances(string filePath)
        {
            var lines = appliances.Select(a => a.ToString()).ToArray();
            File.WriteAllLines(filePath, lines);
        }

        public Appliance? FindApplianceByItemNumber(string itemNumber)
        {
            return appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);
        }

        public List<Appliance> FindAppliancesByBrand(string brand)
        {
            return appliances.Where(a => a.Brand.Equals(brand, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Refrigerator> FindRefrigeratorsByNumberOfDoors(string numberOfDoors)
        {
            return appliances.OfType<Refrigerator>().Where(r => r.NumberOfDoors.ToString() == numberOfDoors).ToList();
        }

        public List<Vacuum> FindVacuumsByBatteryVoltage(string batteryVoltage)
        {
            return appliances.OfType<Vacuum>().Where(v => v.BatteryVoltage.ToString() == batteryVoltage).ToList();
        }

        public List<Microwave> FindMicrowavesByRoomType(string roomType)
        {
            return appliances.OfType<Microwave>().Where(m => m.RoomType.Equals(roomType, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Dishwasher> FindDishwashersBySoundRating(string soundRating)
        {
            return appliances.OfType<Dishwasher>().Where(d => d.SoundRating.Equals(soundRating, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        public List<Appliance> GetRandomAppliances(int count)
        {
            return appliances.OrderBy(a => Guid.NewGuid()).Take(count).ToList();
        }
    }

    // Define Appliance subclasses: Refrigerator, Vacuum, Microwave, Dishwasher

    public abstract class Appliance
    {
        public string ItemNumber { get; }
        public string Brand { get; }
        public int Quantity { get; set; }
        public int Wattage { get; }
        public string Color { get; }
        public double Price { get; }

        public Appliance(string itemNumber, string brand, int quantity, int wattage, string color, double price)
        {
            ItemNumber = itemNumber;
            Brand = brand;
            Quantity = quantity;
            Wattage = wattage;
            Color = color;
            Price = price;
        }

        public override string ToString()
        {
            return $"Item Number: {ItemNumber}, Brand: {Brand}, Quantity: {Quantity}, Wattage: {Wattage}, Color: {Color}, Price: {Price:C}";
        }
    }

    public class Refrigerator : Appliance
    {
        public int NumberOfDoors { get; }
        public int Height { get; }
        public int Width { get; }

        public Refrigerator(string itemNumber, string brand, int quantity, int wattage, string color, double price, int numberOfDoors, int height, int width)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            NumberOfDoors = numberOfDoors;
            Height = height;
            Width = width;
        }

        public override string ToString()
        {
            return base.ToString() + $", Type: Refrigerator, Doors: {NumberOfDoors}, Height: {Height}, Width: {Width}";
        }
    }

    public class Vacuum : Appliance
    {
        public string Grade { get; }
        public int BatteryVoltage { get; }

        public Vacuum(string itemNumber, string brand, int quantity, int wattage, string color, double price, string grade, int batteryVoltage)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Grade = grade;
            BatteryVoltage = batteryVoltage;
        }

        public override string ToString()
        {
            return base.ToString() + $", Type: Vacuum, Grade: {Grade}, Battery Voltage: {BatteryVoltage} V";
        }
    }

    // The `Microwave` class extends the `Appliance` base class and represents a specific type of appliance.
    // It adds additional attributes specific to microwaves such as `Capacity` and `RoomType`.
    // The class constructor initializes these attributes along with inherited attributes from the base class.
    // The `ToString` method overrides the base implementation to provide a customized string representation
    // that includes the appliance type ("Microwave"), its capacity (`Capacity`), and its room type (`RoomType`).
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

        // Override of ToString method to provide a customized string representation of the Microwave object
        public override string ToString()
        {
            // Format: "ItemNumber;Brand;Quantity;Wattage;Color;Price, Type: Microwave, Capacity: {Capacity} cu.ft, Room Type: {RoomType}"
            return base.ToString() + $", Type: Microwave, Capacity: {Capacity} cu.ft, Room Type: {RoomType}";
        }
    }

    // The `Dishwasher` class extends the `Appliance` base class and represents a specific type of appliance.
    // It adds additional attributes specific to dishwashers such as `Feature` and `SoundRating`.
    // The class constructor initializes these attributes along with inherited attributes from the base class.
    // The `ToString` method overrides the base implementation to provide a customized string representation
    // that includes the appliance type ("Dishwasher"), its specific features (`Feature`), and its sound rating (`SoundRating`).
    public class Dishwasher : Appliance
    {
        // Additional properties specific to Dishwasher appliances
        public string Feature { get; }
        public string SoundRating { get; }

        // Constructor to initialize a Dishwasher object with specified attributes, including base class attributes
        public Dishwasher(string itemNumber, string brand, int quantity, int wattage, string color, double price, string feature, string soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Feature = feature;
            SoundRating = soundRating;
        }

        // Override of ToString method to provide a customized string representation of the Dishwasher object
        public override string ToString()
        {
            // Format: "ItemNumber;Brand;Quantity;Wattage;Color;Price, Type: Dishwasher, Feature: {Feature}, Sound Rating: {SoundRating}"
            return base.ToString() + $", Type: Dishwasher, Feature: {Feature}, Sound Rating: {SoundRating}";
        }
    }

}

