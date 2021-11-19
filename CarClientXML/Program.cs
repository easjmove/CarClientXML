using CarLibrary;
using System;
using System.IO;
using System.Net.Sockets;
using System.Xml.Serialization;

namespace CarClientXML
{
    class Program
    {
        static void Main(string[] args)
        {
            //Writes to the console what is running
            Console.WriteLine("Car Client");

            //Asks the user for the 3 properties
            Console.WriteLine("Type in Model:");
            string readModel = Console.ReadLine();
            Console.WriteLine("Type in Color:");
            string readColor = Console.ReadLine();
            Console.WriteLine("Type in Registration Number:");
            string readRegistrationNumber = Console.ReadLine();

            //Creates a new Car using the properties the user typed
            //Uses the default empty Constructor, but then initializes the properties
            Car newCar = new Car() { Model = readModel, Color = readColor, RegistrationNumber = readRegistrationNumber };

            //Establishes a connection to the server, in this instance on the localhost on port 10002
            TcpClient socket = new TcpClient("127.0.0.1", 10002);
            //Gets the stream object from the socket. The stream object is able to recieve and send data
            NetworkStream ns = socket.GetStream();

            //No reader is needed, as the server doesn't respond with anything

            //The StreamWriter is an easier way to write data to a Stream, it uses the NetworkStream
            StreamWriter writer = new StreamWriter(ns);

            //initializes a XML serializer and telling it to use the Class(type) Car
            XmlSerializer serializer = new XmlSerializer(typeof(Car));

            //As the serializer expects a Stream to write to, we can get that from the Console with the OpenStandardOutput method
            serializer.Serialize(Console.OpenStandardOutput(), newCar);

            //Uses the XMLSerializer to write the XML to the writer
            serializer.Serialize(writer.BaseStream, newCar);

            //Notice it doesn't use WriteLine, as the XML it is sending is in several lines
            
            //Always remember to flush
            writer.Flush();

            //Single use client, closes the connection afterwards
            socket.Close();
        }
    }
}
