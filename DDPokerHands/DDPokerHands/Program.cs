using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DDPokerHands
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            string empty = string.Empty;
            string line;
            Model.Players player = new Model.Players();
            PokerHands pokerhands = new PokerHands();
            player.Player1 = new List<string>();
            player.Player2 = new List<string>();
            string filePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            string winner = string.Empty;
            string appPath = System.Environment.CurrentDirectory;
            StreamReader file = new StreamReader(appPath + "/poker-hands.txt");
            while ((line = file.ReadLine()) != null)
            {
                winner = pokerhands.ReadFile(line);
                if (winner == "player1")
                {
                    player.Player1.Add(line.ToString());
                }
                else if (winner == "player2")
                {
                    player.Player2.Add(line.ToString());
                }
                counter++;
            }

            System.Console.WriteLine("Player1 :" + line + " - " + player.Player1.Count().ToString());
            System.Console.WriteLine("Player2 :" + line + " - " + player.Player2.Count().ToString());
            Console.ReadLine();
        }
    }
}
