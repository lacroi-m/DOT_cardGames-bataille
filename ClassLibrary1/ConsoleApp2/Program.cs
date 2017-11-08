using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SynchronousSocketClient
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

    public static void StartClient()
    {
        // Data buffer for incoming data.  
        byte[] message = new byte[1024];

        // Connect to a remote device.  
        try
        {
            // Establish the remote endpoint for the socket.  
            // This example uses port 11000 on the local computer.  
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

            // Create a TCP/IP  socket.  
            Socket sender = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.  
            try
            {
                sender.Connect(remoteEP);

                Console.WriteLine("Socket connected to {0}",
                    sender.RemoteEndPoint.ToString());

                // Encode the data string into a byte array.  
                byte[] msg = Encoding.ASCII.GetBytes("This is a test<EOF>");

                // Send the data through the socket.  
                 int bytesSent = sender.Send(msg);

                // Receive the response from the remote device.  
                int bytesRec = sender.Receive(message);
                                                        //             ActionReaction(message);
                Console.WriteLine("Echoed test = {0}",
                    Encoding.ASCII.GetString(message, 0, bytesRec));

                // Release the socket.  
                sender.Shutdown(SocketShutdown.Both);
                for (int i = 1; i < 10; i++)
                    i--;
                sender.Close();

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        StartClient();
        return 0;
    }
}