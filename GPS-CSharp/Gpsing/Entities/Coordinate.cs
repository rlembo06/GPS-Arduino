using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Gpsing
{
    class Coordinate
    {
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public DateTime Date { get; set; }

        public Coordinate(double longitude, double latitude)
        {
            Longitude = longitude;
            Latitude = latitude;
            Date =  DateTime.Now;
        }

        public void GetPosition()
        {
            Console.WriteLine("- Latitude : " + Latitude);
            Console.WriteLine("- Longitude : " + Longitude);
            System.Diagnostics.Process.Start("http://www.coordonnees-gps.fr/latitude-longitude/"+ Latitude + "/"+ Longitude + "/10/roadmap");
        }
    }
}