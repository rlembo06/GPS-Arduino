using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

//TUTO : https://www.geodatasource.com/developers/c-sharp
namespace Gpsing
{
    class CalculateGPS
    {
        public CalculateGPS () {}

        public double distance(Coordinate c1, Coordinate c2, char unit)
        {
            double theta = c1.Longitude - c2.Longitude;
            double dist = Math.Sin(deg2rad(c1.Latitude)) * Math.Sin(deg2rad(c2.Latitude)) + Math.Cos(deg2rad(c1.Latitude)) * Math.Cos(deg2rad(c2.Latitude)) * Math.Cos(deg2rad(theta));
            dist = Math.Acos(dist);
            dist = rad2deg(dist);
            dist = dist * 60 * 1.1515;
            if (unit == 'K')
            {
                dist = dist * 1.609344;
            }
            else if (unit == 'N')
            {
                dist = dist * 0.8684;
            }
            return (dist);
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }


        public double vitesse(List<Coordinate> coordinates)
        {
            int nbCoordinates = coordinates.Count - 1;
            int i = nbCoordinates;

            if (nbCoordinates > 1)
            {
                int last = nbCoordinates;
                double lastDistance = distance(coordinates[nbCoordinates], coordinates[nbCoordinates - 1], 'M');
                int during = coordinates[nbCoordinates].Date.Second - coordinates[nbCoordinates - 1].Date.Second;
                return lastDistance / during;
            }

            return 0;
        }
    }
}