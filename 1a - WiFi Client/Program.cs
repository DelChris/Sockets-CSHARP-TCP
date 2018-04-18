using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1a___WiFi_Client
{
    class Program
    {
        private static Socket _socket;
        private static String input;

        static void Main(string[] args)
        {
            Console.WriteLine("------------------ WiFi Client v0.1 ------------------");
            Console.Write("Choisissez l'ip où vous connecter : ");
            String address = Console.ReadLine();
            Console.Write("Choisissez le port d'écoute : ");
            int port = int.Parse(Console.ReadLine());
            ClientTCP(IPAddress.Parse(address), port);
        }

        private static void ClientTCP(IPAddress address, int port)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEndPoint = new IPEndPoint(address, port);

            _socket.Connect(ipEndPoint);
            var thread = new Thread(Listen);
            thread.Start();
        }

        private static void Listen()
        {
            while (true)
            {
                Console.WriteLine("Message à envoyer :");
                input = Console.ReadLine();
                _socket.Send(Encoding.UTF8.GetBytes(input));
                Console.WriteLine("Message envoyé.");
            }
        }
    }
}
