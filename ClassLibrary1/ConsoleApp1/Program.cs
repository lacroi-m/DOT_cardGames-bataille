using System;
using System.Collections.Generic;
using System.Linq;
using ConsoleApp1;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;


namespace ServerApplication
{
    class Server
    {
        private static int nbrofclientsconnected = 0;
        private static List<int> clientports = new List<int>();
        private static bool launchGame;

        static void Main(string[] args)
        {
            Connection.StartListening(ConnectionType.TCP,
                new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 10101));
            NetworkComms.AppendGlobalIncomingPacketHandler<String>("First Connection", FirstConnect);
            Console.WriteLine("Press any key to close server.");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    ShutDownClients();
                    break;
                }
                if (nbrofclientsconnected % 2 == 0 && nbrofclientsconnected != 0 && launchGame == false)
                {
                    Client fClient = new Client();
                    fClient.Port = clientports[nbrofclientsconnected - 2];
                    Client sClient = new Client();
                    sClient.Port = clientports[nbrofclientsconnected - 1];
                    Game g1 = new Game(fClient, sClient);
                    g1.Launch();
                    Console.WriteLine("A Game has been launched");
                    launchGame = true;
                    if (Console.KeyAvailable)
                    {
                        ShutDownClients();
                        break;
                    }
                }
            }
            Environment.Exit(0);
        }

        private static void ShutDownClients()
        {
            if (nbrofclientsconnected != 0)
                for (int i = 0; i < nbrofclientsconnected; i++)
                {
                    try
                    {
                        NetworkComms.SendObject("666", "127.0.0.1", clientports[i], "Server shutting down");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            NetworkComms.Shutdown();
            Environment.Exit(0);
        }

        private static void FirstConnect(PacketHeader header, Connection connection, string empty)
        {
            string port = connection.ToString();
            port = port.Split(':').Last();
            port = port.Split(' ').First();
            clientports.Add(int.Parse(port));
            try
            {
                NetworkComms.SendObject("002", "127.0.0.1", clientports[nbrofclientsconnected],
                    $"002: Waiting for other players to connect.");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            nbrofclientsconnected++;
            launchGame = false;
        }
    }
}