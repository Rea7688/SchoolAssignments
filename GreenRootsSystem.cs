using System;
using System.Collections.Generic;


namespace GreenRootsSystem
{
       public class Produce
    {
        public string Name { get; set; }     
        public double WeightKg { get; set; }  
        public string Category { get; set; }  
    }

   
    public class Depot
    {

        private Dictionary<string, List<Produce>> sections;

        
        private const int Capacity = 4;


        public Depot()
        {
            sections = new Dictionary<string, List<Produce>>
            {
                { "Grains", new List< Produce > () },
                { "Vegetables", new List< Produce > () },
                { "Fruits", new List< Produce > () }
            };
            
        }

        public bool AddProduce(int sectionNo, Produce produce)
        {
            // Convert number to section name string
            string sectionName = GetSectionName(sectionNo);

            // Check if section has reached maximum capacity (4 items)
            if (sections[sectionName].Count >= Capacity)
            {
                Console.WriteLine("Section is full. Cannot add more produce.");
                return false; 
            }

            // Add produce to the section's list
            sections[sectionName].Add(produce);
            return true; 
        }

        // Displays all sections and their contents
        public void DisplayInventory()
        {
            Console.WriteLine("\n--- Depot Inventory ---");
            int sectionNo = 0;  // Track section number for display (0, 1, 2)

            foreach (var section in sections)
            {
               
                Console.WriteLine($"Section {sectionNo}:");

                // Check if this section has any produce
                if (section.Value.Count == 0)
                {
                    Console.WriteLine("  (Empty)");
                }
                else
                {
                    // Display each produce item in this section
                    foreach (var produce in section.Value)
                    {
                        Console.WriteLine($"  - Name: {produce.Name}, Weight: {produce.WeightKg}kg, Category: {produce.Category}");
                    }
                }
                sectionNo++; 
            }
        }

        private string GetSectionName(int sectionNo)
        {
            if (sectionNo == 0) return "Grains";
            if (sectionNo == 1) return "Vegetables";
            return "Fruits";  // Default for section 2
        }
    }


    class Program
    {
       
        static void Main(string[] args)
        {
          
            Depot depot = new Depot();

            while (true)
            {
                Console.WriteLine("\nGreenRoots Produce Management System");
                Console.WriteLine("1. Add Produce");
                Console.WriteLine("2. Display Produce");
                Console.WriteLine("3. Exit");
                Console.Write("Enter your choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduce(depot);  
                        break;
                    case "2":
                        depot.DisplayInventory();  
                        break;
                    case "3":
                        Console.WriteLine("Exiting system... Goodbye!");
                        return;  
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }


        static void AddProduce(Depot depot)
        {
            Console.Write("Enter produce name: ");
            string name = Console.ReadLine();

            // Get weight with validation (must be positive number)
            Console.Write("Enter weight in kg: ");
            double weightKg = GetValidDouble();

            Console.Write("Enter category (e.g. Grain, Vegetable, Fruit): ");
            string category = Console.ReadLine();

            // Get section number with validation (must be 0, 1, or 2)
            Console.Write("Enter section number (0-2): ");
            int sectionNo = GetValidInt();

            // Create new Produce object using object initializer
            Produce produce = new Produce
            {
                Name = name,
                WeightKg = weightKg,
                Category = category
            };

 
            if (depot.AddProduce(sectionNo, produce))
            {
                Console.WriteLine($"Produce '{name}' added to Section {sectionNo} successfully.");
            }
            else
            {
                Console.WriteLine("Failed to add produce. Please try again.");
            }
        }

        static double GetValidDouble()
        {
            double value;

            while (!double.TryParse(Console.ReadLine(), out value) || value <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number.");
                Console.Write("Enter weight in kg: ");
            }
            return value;  // Return the valid value to the caller
        }

        // Validates that user enters a valid section number (0-2)
        static int GetValidInt()
        {
            int value;
            while (!int.TryParse(Console.ReadLine(), out value) || value < 0 || value > 2)
            {
                Console.WriteLine("Invalid input. Please enter a number between 0 and 2.");
                Console.Write("Enter section number (0-2): ");
            }
            return value;
        }
    }
}
