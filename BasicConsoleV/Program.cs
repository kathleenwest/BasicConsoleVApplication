using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsoleV
{
    class Program
    {
        static List<City> cities = new List<City>();    // List holding City objects for the test data

        /// <summary>
        /// Main entry for the program of BasicConsoleV
        /// Kathleen West
        /// </summary>
        /// <param name="args">No arguments are processed</param>
        static void Main(string[] args)
        {
            MenuChoices choice = MenuChoices.CityDistances;   // Enum for user menu choice, Initialized for while loop logic      

            // User Prompt Loop and Main Program Loop
            do
            {
                // Change the ForeGround Color
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Please choose from the following menu options");

                // Print the MenuChoices from an Enum
                foreach (int item in Enum.GetValues(typeof(MenuChoices)))
                {
                    Console.WriteLine($"[{(int) item}] {(MenuChoices) item}");
                }

                // Prompt User for the Menu Choice
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Your choice: ");

                // Read Input for the Program Choice
                Console.ForegroundColor = ConsoleColor.Magenta;

                // Parse and Validate the User Data Entry before proceeding to the Menu calls
                if(Enum.TryParse(Console.ReadLine(), out choice))
                {
                    // Change the ForeGround Color
                    Console.ForegroundColor = ConsoleColor.White;

                    // Calls to Method based on user choice
                    switch (choice)
                    {
                        case MenuChoices.CityDistances:     // Choice for Calculating Arc Lengths between two cities
                            CityDistances();        
                            break;
                        case MenuChoices.DisplayCities:     // Choice for printing out the city data information
                            DisplayCities();
                            break;                          
                    } // end of switch

                    Console.WriteLine();
                } // end of if

            } while (choice != MenuChoices.Quit) ; // end of while

            // Change the ForeGround Color
            Console.ForegroundColor = ConsoleColor.White;

            // Pause the program for user to observe Console
            Console.Write("Press <Enter> to quit...");
            Console.ReadLine();

        } // end of Main method

        /// <summary>
        /// This constructor for the Program class adds sample city data for the
        /// cities list and adds each city data to the list for use in the program
        /// </summary>
        static Program()
        {
            cities.Add(new City("San Diego", "CA", "USA", 32.71513M, -117.1611M));
            cities.Add(new City("San Francisco", "CA", "USA", 37.78352M, -122.4169M));
            cities.Add(new City("Los Angeles", "CA", "USA", 34.04983M, -118.2498M));
            cities.Add(new City("New York", "NY", "USA", 40.71015M, -74.00658M));
            cities.Add(new City("London", "London", "England", 51.51786M, -0.102216M));
            cities.Add(new City("Sydney", "NSW", "Australia", -33.86767M, 151.2094M));
        } // end of method

        /// <summary>
        /// This static method prints to console all the current cities in the
        /// cities list, their information including name, province, country, coordinates, etc.
        /// and a very nice header for the list.
        /// </summary>
        public static void DisplayCities()
        {
            // Prints the Header for the City List
            City.PrintHeader();

            // Prints each city in its formated string per Print()
            foreach (City item in cities)
            {
                item.Print();
            } 

        } // end of method

        /// <summary>
        /// This static method provides a user choice menu of cities to select
        /// two cities to output the distance between them and the unit of
        /// measurement for the arc length distance.
        /// </summary>
        public static void CityDistances()
        {
            int city1;          // user first city input selection
            int city2;          // user second city input selection
            int unit_input;     // user choice for unit measurement as integer
            LengthTypes unit;   // parsed and validated user choice for unit measurement as enum
            
            // Change the ForeGround Color
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("City List:");

            // Print the list of cities for the user selection menu
            for (int i = 0; i < cities.Count; i++)
            {
                Console.WriteLine($"{i}. {cities.ElementAt(i).Name}");
            } // end of if

            // Prompt User for the First City Selection
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter first city number:");

            // Read Input for the First City
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Parse and Validate the First City Selection
            if (!(int.TryParse(Console.ReadLine().Trim(), out city1)) || ((city1 >= cities.Count) || (city1 < 0)))
            {
                    Console.WriteLine("You entered an invalid input. Exiting to main menu.");
                    return;
            } // end of if

            // Prompt User for the Second City Selection
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Enter second city number:");

            // Read Input for Second City
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Parse and Validate the Second City Selection
            if (!(int.TryParse(Console.ReadLine().Trim(), out city2)) || ((city2 >= cities.Count) || (city2 < 0)))
            {
                    Console.WriteLine("You entered an invalid input. Exiting to main menu.");
                    return;
            } // end of it

            // Change the ForeGround Color
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Please select a unit of measurement");

            // Print the LengthTypes for the Menu Selection
            foreach (int item in Enum.GetValues(typeof(LengthTypes)))
            {
                Console.WriteLine($"[{(int)item}] {(LengthTypes)item}");
            } // end of if

            // Read Input for LengthType
            Console.ForegroundColor = ConsoleColor.Magenta;

            // Parse and Validate the Unit Measurement
            if (!(int.TryParse(Console.ReadLine().Trim(), out unit_input)) || !(Enum.IsDefined(typeof(LengthTypes), unit_input)))
            {
                Console.WriteLine("You entered an invalid input. Exiting to main menu.");
                return;
            } // end of if

            // Set unit enumeration based on valid parsed user input
            unit = (LengthTypes)unit_input;

            // Prints the Distance Between Two Cities in a Pretty Formated String per the lab example
            Console.WriteLine($"The distance between {cities.ElementAt(city1).Name} " +
                $"and {cities.ElementAt(city2).Name} is " +
                $"{cities.ElementAt(city1).Location.GreatCircleDistance(cities.ElementAt(city2).Location, unit):00.0} " +
                $"{unit}");

        } // end of method CityDistances()

    } // end of class
} // end of namespace
