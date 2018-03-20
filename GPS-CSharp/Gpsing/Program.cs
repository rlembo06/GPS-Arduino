using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Device.Location;

namespace Gpsing
{
    class Program
    {
        private StringBuilder buffer = new StringBuilder();
        private static List<Coordinate> coordinates = new List<Coordinate>();
        private static CalculateGPS calculate = new CalculateGPS();

        public static void Main(string[] args)
        {
            SerialPort mySerialPort = new SerialPort("COM10");

            mySerialPort.BaudRate = 9600;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.One;
            mySerialPort.DataBits = 8;
            mySerialPort.Handshake = Handshake.None;
            mySerialPort.RtsEnable = true;

            mySerialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

            mySerialPort.Open();

            Console.WriteLine("Press any key to continue...");
            Console.WriteLine();
            Console.ReadKey();
            mySerialPort.Close();
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;

            string trame = sp.ReadLine();
            string[] splitted = trame.Split(',');

            double latitude = Double.Parse(splitted[2].Replace('.', ','));
            double longitude = Double.Parse(splitted[4].Replace('.', ','));

            Coordinate coordinate = new Coordinate(longitude, latitude);
            coordinates.Add(coordinate);

            coordinate.GetPosition();

            var lastDistance = calculate.distance(coordinate, coordinate, 'M');
            var lastVitesse = calculate.vitesse(coordinates);

            Console.WriteLine("- Vitesse : " + lastVitesse + " M/s");
            Console.WriteLine("- Distance : " + lastDistance + " M");
            Console.WriteLine("\n");

            Thread.Sleep(200);
            
        }
    }
}

