using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    public class Game
    {
        private static Client _client1 { get; set; }
        private static Client _client2 { get; set; }
        [JsonProperty]
        private Deck _deck {get; set; }
        [JsonProperty]
        public static List<Card> f_table { get; set; }= new List<Card>();
        [JsonProperty]
        public static List<Card> s_table { get; set; }= new List<Card>();

        private static string ip = "127.0.0.1";

        public Game(Client f, Client s)
        {
            _deck = new Deck(true);
            _client1 = f;
            _client2 = s;
        }

        public void Launch()
        {
            Distribute();
            LoopGame();
        }

        public void LoopGame()
        {
            NetworkComms.AppendGlobalIncomingPacketHandler<string>("103", ReceiveCard);
            bool turn = false;
            int battle = 0;
            Console.WriteLine("Starting Game !");
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    break;
                }
                if (turn == false)
                {
                    NetworkComms.SendObject("003", ip, _client1.Port, "");
                    NetworkComms.SendObject("003", ip, _client2.Port, "");
                    turn = true;
                }
                if (f_table.Count > 0 && f_table.Count > 0)
                    {
                        try
                        {
                            if (f_table[battle].GetValue() > s_table[battle].GetValue())
                            {
                                NetworkComms.SendObject("002", ip, _client1.Port, "You win the round");
                                NetworkComms.SendObject("002", ip, _client2.Port, "You loose the round");
                                f_table.Add(s_table[battle]);
                                s_table.RemoveAt(0);
                                string jsonf_table = JsonConvert.SerializeObject(f_table);
                                NetworkComms.SendObject("004", ip, _client1.Port, jsonf_table);
                                f_table.RemoveAt(1);
                                f_table.RemoveAt(0);
                                _client1.Score++;
                                battle = 0;
                            }
                            else if (f_table[0].GetValue() < s_table[0].GetValue())
                            {
                                NetworkComms.SendObject("002", ip, _client2.Port, "You win the round");
                                NetworkComms.SendObject("002", ip, _client1.Port, "You loose the round");
                                s_table.Add(f_table[battle]);
                                f_table.RemoveAt(0);
                                string jsons_table = JsonConvert.SerializeObject(f_table);
                                NetworkComms.SendObject("004", ip, _client2.Port, jsons_table);
                                s_table.RemoveAt(1);
                                s_table.RemoveAt(0);
                                _client2.Score++;
                                battle = 0;
                            }
                            else
                            {
                                NetworkComms.SendObject("002", ip, _client1.Port, "Bataille !");
                                NetworkComms.SendObject("002", ip, _client2.Port, "Bataille !");
                                battle++;
                            }
                        turn = false;
                        }
                        catch (Exception e)
                        {
                            string noerr = e.ToString();
                            // I dont want the server to show that Im badly controling the situation
                            //                            Console.WriteLine(e);
                        }

                }
            }

        }


      private static void ReceiveCard(PacketHeader papcketHeader, Connection connection, string jsonCard)
        {
            string port = connection.ToString();
            port = port.Split(':').Last();
            port = port.Split(' ').First();
            if (jsonCard == "[]" || jsonCard.Equals(""))
                return;
            Console.WriteLine("Receiving a card from: " + port);
            Card deserializedCard = JsonConvert.DeserializeObject<Card>(jsonCard);
            if (_client1.Port == int.Parse(port))
            {
                Console.WriteLine("Client1 sent me a card\n value: " + deserializedCard.GetValue() + "\ncolor: " + deserializedCard.GetColor());
                f_table.Add(deserializedCard);
            }
            else if (_client2.Port == int.Parse(port))
            {
                Console.WriteLine("Client2 sent me a card\n value: " + deserializedCard.GetValue() + "\ncolor: " + deserializedCard.GetColor());
                s_table.Add(deserializedCard);
            }
            else
                NetworkComms.SendObject("666", "127.0.0.1", int.Parse(port),
                    "Youre talking to the wrong guy Jack ! Im shutting you down !");
            try
            {
                NetworkComms.SendObject("002", ip, int.Parse(port), "Waiting for other Player to play a card...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void GiveCard(Client client, Card card)
        {
            string jsonCard = JsonConvert.SerializeObject(card);
            NetworkComms.SendObject("005", "127.0.0.1", client.Port, jsonCard);
      //      Console.WriteLine("sending card with id : " + card._id + "\nvalue = " + card.GetValue() + "\n color = " + card.GetColor() + "\nJson is like this");
        }

        public void GiveCards(Client client, ArrayList cards)
        {
            var i = 0;
            Console.WriteLine("Distributing cards to: " + client.Port);
            while (i < cards.Count)
            {
                GiveCard(client, (Card)cards[i]);
                i = i + 1;
            }
        }

        public void Distribute()
        {
            var i = -1;
            Console.WriteLine("Distributing cards players");
            while (++i < _deck.GetDeck().Count)
            {
                if (i % 2 == 0)
                    GiveCard(_client1, _deck.GetDeck()[i]);
                else
                    GiveCard(_client2, _deck.GetDeck()[i]);
            }
        }

    }
}