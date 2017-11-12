using System;
using System.Collections.Generic;
using ConsoleApp1;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;

namespace ClientApp
{
    class Client
    {
        private static Deck MyDeck = new Deck(false);
        static string serverIP = "127.0.0.1";
        static int serverPort = 10101;

        static void Main(string[] args)
        {
            MyDeck = new Deck(false);
            NetworkComms.SendObject("First Connection", serverIP, serverPort, "");
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("002", PrintIncomingMessage);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("004", ReceiveWins);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("005", ReceiveCard);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("666", ServerShutDown);
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("003", PlayACard);
            while (true)
            {
            }
        }

        private static void PrintIncomingMessage(PacketHeader packetheader, Connection connection, string incomingobject)
        {
            Console.WriteLine(incomingobject);
        }

        private static void PlayACard(PacketHeader papckeHeader, Connection connection, string empty)
        {
            Console.WriteLine("\nPress any key to Play a card.");
            Console.ReadKey(true);
            Console.Write("You have :" + MyDeck.CardsInDeck() + "cards.");
            Console.WriteLine("Youve played: " + MyDeck.GetDeck()[0].GetValue() + "\nwith color " + MyDeck.GetDeck()[0].GetColor());
            string jsonCard = JsonConvert.SerializeObject(MyDeck.GetDeck()[0]);
            NetworkComms.SendObject("103", serverIP, serverPort, jsonCard);
            MyDeck.GetDeck().RemoveAt(0);
        }

        private static void ReceiveWins(PacketHeader papckeHeader, Connection connection, string jsonCards)
        {
            List<Card> deserializedCard = JsonConvert.DeserializeObject<List<Card>>(jsonCards);
            for (int i = 0; i < deserializedCard.Count; i++)
                MyDeck.AddCard(deserializedCard[i]);
        }
        private static void ReceiveCard(PacketHeader papckeHeader, Connection connection, string jsonCard)
        {
            Card deserializedCard = JsonConvert.DeserializeObject<Card>(jsonCard);
            Console.WriteLine("Receiving Card :\n value = " + deserializedCard.GetValue() +"\n color  = " + deserializedCard.GetColor());
            MyDeck.AddCard(deserializedCard);
        }

        private static void ServerShutDown(PacketHeader packetheader, Connection connection, string incomingobject)
        {
            Console.WriteLine("Server shutting down\nThis client will close in 3 seconds");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            while (true)
            {
                if (watch.ElapsedMilliseconds > 3000)
                    break;
            }
            watch.Stop();
            Environment.Exit(0);
        }
    }
}
