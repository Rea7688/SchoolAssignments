using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;


namespace AthleteManagement
{
    public class Athlete
    {
        private string athleteID;
        private string name;
        private List<int> scores;

        private static int totalAthletesReg;

        static Athlete()
        {
            totalAthletesReg = 0;
            Console.WriteLine("Athlete Management system intialised");
        }

        public Athlete(string id, string athleteName, List<int> athleteScores)
        {
            if (ValidateAthleteID(id) == false)
            {
                throw new ArgumentException("Invalid Athlete ID. Format (A12345)");
            }


            athleteID = id;
            name = athleteName;

            if (string.IsNullOrWhiteSpace(athleteName))
            {
                throw new ArgumentException("Athlete name cannot be empty.");
            }
            if (athleteScores == null)
            {
                scores = new List<int>();
            }
            else
            {
                scores = athleteScores;
            }

            totalAthletesReg = totalAthletesReg + 1;
        }

        public string AthleteID
        {
            get { return athleteID; }

        }
        public string Name
        {
            get { return name; }
        }


        public List<int> Scores
        {
            get { return scores; }
        }

        public static int TotalAthletesReg
        {
            get { return totalAthletesReg; }
        }

        public static bool ValidateAthleteID(string id)
        {
            if (id.Length != 6)
            {
                return false;
            }

            if (!id.StartsWith("A"))
            {
                return false;
            }

            for (int i = 1; i < id.Length; i++)
            {
                if (!char.IsDigit(id[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public double CalculateAvgScore()
        {
            if (scores.Count == 0)
            {
                return 0.0;
            }

            int total = 0;
            for (int i = 0; i < scores.Count; i = i + 1)
            {
                total = total + scores[i];
            }

            double avg = (double)total / scores.Count;
            return avg;
        }

        public string GetFormattedDetails()
        {
            string result = string.Format("ID: {0,-12} | Name: {1, -20}" +
                " | Avg Scorre: {2, 8:F2}", athleteID, name, CalculateAvgScore());

            return result;
        }


        ~Athlete()
        {
            Console.WriteLine("Athlete object " + athleteID + " (" + name + ") is being removed.");
        }
    }

        class Program
        {
            static List<Athlete> athletes = new List<Athlete>();

            static void Main(string[] args)
            {
                bool run = true;

                while (run == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("--- Athlete Management System Menu ---");
                    Console.WriteLine("1. Add Athlete");
                    Console.WriteLine("2. Remove Athlete");
                    Console.WriteLine("3. Display All Athletes");
                    Console.WriteLine("4. Search for Athlete");
                    Console.WriteLine("5. Exit");
                    Console.Write("Choose an option: ");

                    string choice = Console.ReadLine();
                    Console.WriteLine();


                    if (choice == "1")
                    {
                        AddAthlete();
                    }
                    else if (choice == "2")
                    {
                        RemoveAthlete();
                    }
                    else if (choice == "3")
                    {
                        DisplayAllAthletes();
                    }
                    else if (choice == "4")
                    {
                        SearchAthlete();
                    }
                    else if (choice == "5")
                    {
                        run = false;
                        Console.WriteLine("Thank you for using Athlete Management System. Goodbye!");
                    }
                    else
                    {
                        Console.WriteLine("Invalid option. Please choose 1-5.");
                    }
                }
            }


            static void AddAthlete()
            {
                try
                {
                    // Get ID from user
                    Console.Write("Enter Athlete ID (Format A12345): ");
                    string id = Console.ReadLine();

                    // Check if ID is valid
                    if (Athlete.ValidateAthleteID(id) == false)
                    {
                        throw new FormatException("Invalid ID format. Must be A followed by" +
                            " 5 digits.");
                    }

                    // Check if ID already exists
                    for (int i = 0; i < athletes.Count; i = i + 1)
                    {
                        if (athletes[i].AthleteID == id)
                        {
                            throw new InvalidOperationException("An athlete with this ID " +
                                "already exists.");
                        }
                    }

                    // Get name
                    Console.Write("Enter Name: ");
                    string name = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(name))
                    {
                        throw new ArgumentException("Name cannot be empty.");
                    }

                    // Get scores
                    Console.Write("How many scores? ");
                    string numInput = Console.ReadLine();
                    int numScores = Convert.ToInt32(numInput);

                    if (numScores < 0)
                    {
                        throw new FormatException("Number of scores cannot be negative.");
                    }

                    List<int> scores = new List<int>();

                    for (int i = 0; i < numScores; i = i + 1)
                    {
                        Console.Write("Enter score " + (i + 1) + ": ");
                        string scoreInput = Console.ReadLine();
                        int score = Convert.ToInt32(scoreInput);
                        scores.Add(score);
                    }

                    // Create the athlete and add to list
                    Athlete newAthlete = new Athlete(id, name, scores);
                    athletes.Add(newAthlete);

                    Console.WriteLine();
                    Console.WriteLine("Athlete added successfully.");
                }
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Input Error: " + ex.Message);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Validation Error: " + ex.Message);
                }
                catch (InvalidOperationException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Unexpected Error: " + ex.Message);
                }
            }

            static void RemoveAthlete()
            {
                try
                {
                    Console.Write("Enter Athlete ID to remove: ");
                    string id = Console.ReadLine();

                    // Find the athlete
                    Athlete toRemove = null;

                    for (int i = 0; i < athletes.Count; i = i + 1)
                    {
                        if (athletes[i].AthleteID == id)
                        {
                            toRemove = athletes[i];
                            break; // stop searching once found
                        }
                    }

                    // If athlete not found
                    if (toRemove == null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("Athlete with ID " + id + " not found.");
                        return;
                    }

                    // Remove atlete from list
                    athletes.Remove(toRemove);
                    Console.WriteLine();
                    Console.WriteLine("Athlete " + toRemove.Name + " (ID: " + id + ") " +
                        "removed successfully.");

                    GC.Collect();
                }
                catch (Exception ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Error removing athlete: " + ex.Message);
                }
            }

            static void DisplayAllAthletes()
            {
                Console.WriteLine("--- Athlete List ---");

                if (athletes.Count == 0)
                {
                    Console.WriteLine("No athletes registered.");
                    return;
                }

                // Loop through each athlete
                for (int i = 0; i < athletes.Count; i = i + 1)
                {
                    Console.WriteLine(athletes[i].GetFormattedDetails());
                }

                Console.WriteLine();
                Console.WriteLine("Total registered athletes (lifetime count): " + Athlete.TotalAthletesReg);
                Console.WriteLine("Currently active athletes: " + athletes.Count);
            }

            static void SearchAthlete()
            {
                try
                {
                    Console.Write("Enter Athlete ID to search: ");
                    string id = Console.ReadLine();

                    if (Athlete.ValidateAthleteID(id) == false)
                    {
                        throw new FormatException("Invalid ID format.");
                    }

                    // Search for athlete
                    Athlete found = null;

                    for (int i = 0; i < athletes.Count; i = i + 1)
                    {
                        if (athletes[i].AthleteID == id)
                        {
                            found = athletes[i];
                            break;
                        }
                    }

                    if (found != null)
                    {
                        Console.WriteLine();
                        Console.WriteLine("--- Athlete Found ---");
                        Console.WriteLine(found.GetFormattedDetails());

                        Console.Write("Scores: ");
                        if (found.Scores.Count == 0)
                        {
                            Console.WriteLine("No scores recorded");
                        }
                        else
                        {
                            for (int i = 0; i < found.Scores.Count; i = i + 1)
                            {
                                Console.Write(found.Scores[i]);
                                if (i < found.Scores.Count - 1)
                                {
                                    Console.Write(", ");
                                }
                            }
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("Athlete with ID " + id + " not found.");
                    }
                }
                catch (FormatException ex)
                {
                    Console.WriteLine();
                    Console.WriteLine("Search Error: " + ex.Message);
                }
            }
        }
    }