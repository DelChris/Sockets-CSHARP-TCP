using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _1b___WiFi_Serveur
{
    class Program
    {
        private static Socket _listenerSocket;

        static void Main(string[] args)
        {
            Console.WriteLine("------------------ WiFi Serveur v0.1 ------------------");
            Console.WriteLine("Choisissez le port d'écoute : ");
            int port = int.Parse(Console.ReadLine());
            ServerTCP(port);
        }

        private static void ServerTCP(int port)
        {
            _listenerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            var address = GetHostIPAdress();
            IPEndPoint ip = new IPEndPoint(address, port);
            _listenerSocket.Bind(ip);

            var listenThread = new Thread(ListenThread);
            listenThread.Start();
            Console.WriteLine($"Success... Listening TCP : {address}:{port}");
        }

        private static void ListenThread()
        {
            while (true)
            {
                _listenerSocket.Listen(0);
                //Clients.Add(new ClientManager(_listenerSocket.Accept()));
                var SocketReceive = _listenerSocket.Accept();
                Console.WriteLine("Client connecté.");
                var clientThread = new Thread(() => 
                {
                    Client(SocketReceive);
                });
                clientThread.Start();
            }
        }

        private static void Client(Socket socketReceive)
        {
            while (true)
            {
                var buffer = new byte[socketReceive.SendBufferSize];
                var readBytes = socketReceive.Receive(buffer);
                if (readBytes > 0)
                {
                    var msg = Encoding.UTF8.GetString(buffer, 0, readBytes);
                    Console.WriteLine($"{msg}");
                }
            }
        }

        private static IPAddress GetHostIPAdress()
        {
            return Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(_ => _.AddressFamily == AddressFamily.InterNetwork);
        }
    }
}
