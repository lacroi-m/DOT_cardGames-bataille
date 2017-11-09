using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using NetworkCommsDotNet;

namespace ClientApp
{
    class Program
    {









        public static void ActionReaction(byte[] message)
        {
            string[] IRCreceive = new string[]
            {
                "000: YOU ARE CONNECTED!",
                "001: Waiting for other players to connect.",
                "002: STARTING GAME - Dealing Cards.",
                "003: Play a card.",
                "004: Card received.",
                "005: Sending your wins.",
                "006: Waiting for opponent to play a card.",
                "007: You loose the round.",
                "008: Sending your wins.",
                "009: YOU LOOSE THE GAME.",
                "010: YOU WIN THE GAME.",
                "011: SCORE [NBR] - [NBR].",
                "666: FATAL ERROR."
            };
            string[] IRCsend = new string[]
            {
                "101: Message received.",
                "102: Deck received.",
                "103: Sending card.",
                "104: Wins received. Putting them at the bottom of my deck.",
                "105: GTG."
            };
        }

        static void Main(string[] args)
        {
            //Request server IP and port number
            Console.WriteLine(
                "Please enter the server IP and port in the format 192.168.0.1:10000 and press return:");
            string serverInfo = Console.ReadLine();

            //Parse the necessary information out of the provided string
            string serverIP = serverInfo.Split(':').First();
            int serverPort = int.Parse(serverInfo.Split(':').Last());

            //Keep a loopcounter
            int loopCounter = 1;
            while (true)
            {
                //Write some information to the console window
                string messageToSend = "This is message #" + loopCounter;
                Console.WriteLine("Sending message to server saying '" + messageToSend + "'");

                //Send the message in a single line
                NetworkComms.SendObject("Message", serverIP, serverPort, messageToSend);

                //Check if user wants to go around the loop
                Console.WriteLine("\nPress q to quit or any other key to send another message.");
                if (Console.ReadKey(true).Key == ConsoleKey.Q) break;
                else loopCounter++;
            }

            //We have used comms so we make sure to call shutdown
            NetworkComms.Shutdown();
        }
    }
}
