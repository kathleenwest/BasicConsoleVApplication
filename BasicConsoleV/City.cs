using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsoleV
{
    /// <summary>
    /// This class is the blueprint for a City object representing the city's location
    /// in terms of name, province, country, and location (Geolocation).
    /// </summary>
    class City
    {
        public string Name { get; set; }            // Name property for the city location
        public string Province { get; set; }        // Province property for the city location
        public string Country { get; set; }         // Country property for the city location
        public Geolocation Location { get; set; }   // Location object property for the city (coordinates)

        /// <summary>
        /// This constructor create as a City object with the specified parameters
        /// Name, Provience, Country, and Location object of the city to be created.
        /// </summary>
        /// <param name="name">Name of the city to be set to the property Name</param>
        /// <param name="province">Province of the city to be set to the property Provience</param>
        /// <param name="country">Country of the city to be set to the property Country</param>
        /// <param name="location">Object that represented the location in decimal coordinates of the city</param>
        public City(string name, string province, string country, Geolocation location)
        {
            Name = name;
            Province = province;
            Country = country;
            Location = location;
        } // end of method

        /// <summary>
        /// This constructor create as a City object with the specified parameters
        /// Name, Provience, Country, Latitude, and Longitude of the city to be created.
        /// </summary>
        /// <param name="name">Name of the city to be set to the property Name</param>
        /// <param name="province">Province of the city to be set to the property Provience</param>
        /// <param name="country">Country of the city to be set to the property Country</param>
        /// <param name="latitude">Latitude of the city to be set to the property Latitude</param>
        /// <param name="longitude">Longitude of the city to be set to the property Longitude</param>
        public City(string name, string province, string country, decimal latitude, decimal longitude)
        {
            Name = name;
            Province = province;
            Country = country;
            Location = new Geolocation(latitude, longitude); 
            
        } // end of method

        /// <summary>
        /// This instance method returns the great circle distance or arc length of two cities 
        /// on the globe with coordinate location objects. The method is called on the first city
        /// instance object and used the second city parameter object to determine the arc
        /// distance. The distance is returned in the unit of measured as specified by the
        /// second parameter to the method which is an enum for the units. 
        /// </summary>
        /// <param name="city">City type object representing the other city to calculate arc distance between</param>
        /// <param name="lengthType">Enum of Measurement type to return calculation</param>
        /// <returns></returns>
        public decimal Distance(City city, LengthTypes lengthType = LengthTypes.Miles)
        {
            if (city == null)
            {
                return 0.0M;
            }

            return Location.GreatCircleDistance(city.Location, lengthType);

        } // end of method

        /// <summary>
        /// Instance method that prints the formated properties for a city including:
        /// Name, Province, Country, and Coordinate Location
        /// </summary>
        public void Print()
        {
            Console.WriteLine($"{Name,-15} {Province,-10} {Country,-15} {Location.DMS()}");
        } // end of method

        /// <summary>
        /// Static method that prints a header for the listing of city data (formated)
        /// </summary>
        public static void PrintHeader()
        {
            Console.WriteLine($"{"City",-15} {"Province",-10} {"Country",-15} {"Coordinates"}");
            Console.WriteLine("===========================================================================");
        } // end of method 

    }// end of class
} // end of namespace
