using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicConsoleV
{
    /// <summary>
    /// This struct holds value information for the Geolocation of a city
    /// which has longitude and latitude coordinates. Some methods allow
    /// return of gelocation analysis between two struct objects such as
    /// arc distance and a formated string of the coordinates
    /// </summary>
    public struct Geolocation
    {
        public readonly decimal Longitude;  // Instance field member for the longitude value
        public readonly decimal Latitude;   // Instance field member for the latitude value

        /// <summary>
        /// This constructor creates a new Geolocation object with the specified
        /// latitude and longitude instance fields. It validates that the numbers
        /// are valid and within range before setting the fields.
        /// </summary>
        /// <param name="latitude">Decimal value for the latitude value</param>
        /// <param name="longitude">Decimal value for the longitude</param>
        public Geolocation(decimal latitude, decimal longitude)
        {
            if((latitude >= -90) && (latitude <= 90) && (longitude >= -180) && (longitude <= 180) )
            {
                Longitude = longitude;
                Latitude = latitude;
            }
            else
            {
                throw new ArgumentOutOfRangeException("The longitude or latitude was out of range.");
            }
        } // end of method

        /// <summary>
        /// This is copy constructor that creates a new Geolocation object by 
        /// copying the latitude and longitude information from the input
        /// specified Geolocation object.
        /// </summary>
        /// <param name="other">Input Geolocation to copy instance field values from</param>
        public Geolocation(Geolocation other)
        {
            Longitude = other.Longitude;    // Sets the longitude instance field to the copy of input other
            Latitude = other.Latitude;      // Sets the latitude instance field to the copy of input other
        } // end of method

        /// <summary>
        /// This instance method calculates the degrees, minutes, and seconds of the 
        /// longitude and latitude coordinate of the Geolocation object and returns 
        /// the results is a nicely formated string with direction indication
        /// </summary>
        /// <returns>Coordinate Formated String of Latitude and Longitude in pretty coordinate style</returns>
        public string DMS()
        {
            // Latitude in Degrees
            decimal latitude_degrees = decimal.Truncate(Latitude);
            // Latitude in Minutes
            decimal latitude_minutes = decimal.Truncate(60*(Latitude - latitude_degrees));
            // Latitude in Seconds
            decimal latitude_seconds = decimal.Round(60*(60*(Latitude - latitude_degrees) - latitude_minutes));
            // Longitude in Degrees
            decimal longitude_degrees = decimal.Truncate(Longitude);
            // Longitude in Minutes
            decimal longitude_minutes = decimal.Truncate(60*(Longitude - longitude_degrees));
            // Longitude in Seconds
            decimal longitude_seconds = decimal.Round(60 *(60 *(Longitude - longitude_degrees) - longitude_minutes));

            // Latitude formated string
            string latitude = $"{Math.Abs(latitude_degrees),3}\u00B0 {Math.Abs(latitude_minutes),2}' {Math.Abs(latitude_seconds),2}\" {(latitude_seconds > 0 ? 'N' : 'S'),1} ";
            // Longitude formated string
            string longitude = $"{Math.Abs(longitude_degrees),3}\u00B0 {Math.Abs(longitude_minutes),2}' {Math.Abs(longitude_seconds),2}\" {(longitude_seconds > 0 ? 'E' : 'W'),1} ";

            // Returns Coordinate Formated String
            return latitude + longitude;

        } // end of method

        /// <summary>
        /// This instance method calculates the Great Circle Distance or arclength
        /// between two Geolocation points (city locations) and returns the length
        /// in the prefered unit of measurements as specified by the input LengthTypes
        /// or default to meters.
        /// </summary>
        /// <param name="other">The second Geolocation object (city location)</param>
        /// <param name="lengthType">Unit of Measure (enum LengthTypes), Default: Meters</param>
        /// <returns>Arclength (decimal) between two locations</returns>
        public decimal GreatCircleDistance(Geolocation other, LengthTypes lengthType = LengthTypes.Meters)
        {
           
            double lat1 = ToRadians((double) Latitude);                     // Location object 1 latitude in radians
            double lat2 = ToRadians((double) other.Latitude);               // Location object 2 latitude in radians
            double lng1 = ToRadians((double) Longitude);                    // Location object 1 longitude in radians
            double lng2 = ToRadians((double) other.Longitude);              // Location object 2 longitude in radians
            double latDiff = lat1 - lat2;                                   // Latitude difference between locations 1 and 2
            double lngDiff = lng1 - lng2;                                   // Longitude difference between locations 1 and 2
            double sin2Lat = Math.Pow(Math.Sin(latDiff / 2),2);             // Wizard Math
            double sin2Lng = Math.Pow(Math.Sin(lngDiff / 2),2);             // Wizard Math
            double a = sin2Lat + Math.Cos(lat1) * Math.Cos(lat2) * sin2Lng; // More Wizard Math
            double arcLength = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));    // How the Wizard died of math           
            double arcLength_meters = 6371000 * arcLength;                  // ArchLength in Meters

            // Return arcLength based on Unit of Measurement choice
            switch (lengthType)
            {
                case LengthTypes.Meters:
                    return (decimal) arcLength_meters;
                case LengthTypes.Kilometers:
                    return (decimal) (arcLength_meters * 0.001);
                case LengthTypes.Feet:
                    return (decimal) (arcLength_meters * 3.2808399);
                case LengthTypes.Miles:
                    return (decimal) (arcLength_meters * 0.00062137119);
            }

            // Default return of zero
            return 0.0M;

        } // end of GreatCircleDistance method

        /// <summary>
        /// This static method converts an input double number value from degrees to radians
        /// and returns that result as a double.
        /// </summary>
        /// <param name="degreeVal">Input number (double) that represents a value in degrees</param>
        /// <returns>double converted value in radians</returns>
        private static double ToRadians(double degreeVal)
        {
            return (degreeVal * (Math.PI / 180.0));
        } // end of method

    } // end of class
} // end of namespace
