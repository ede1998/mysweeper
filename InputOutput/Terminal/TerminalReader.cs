using System.Net.Mime;
using System.Text.RegularExpressions;
using MySweeper.Basic;
using System;

namespace MySweeper.InputOutput.Terminal
{
    public class TerminalReader : IReader
    {
        public Minefield Minefield { get; set; }

        public GameInput ReadGameInput()
        {
            while (true)
            {
                var flagsRemaining = this.Minefield.MineCount - this.Minefield.MarkerCount;
                Console.Write($"Action (h for help, {flagsRemaining} flags): ");
                var input = Console.ReadLine();
                var match = Regex.Match(input, @"^h$");
                if (match.Success)
                {
                    this.PrintHelp();
                    continue;
                }

                match = Regex.Match(input, @"^q$");
                if (match.Success)
                {
                    Environment.Exit(0);
                }

                match = Regex.Match(input, @"^([umra])\s*(\d+)\s*(\d+)$");
                if (match.Success)
                {
                    var x = int.Parse(match.Groups[2].Value);
                    var y = int.Parse(match.Groups[3].Value);
                    switch (match.Groups[1].Value)
                    {
                        case "r":
                            return new GameInput { Action = Action.RevealField, Coordinate = new Coordinate(x, y) };
                        case "a":
                            return new GameInput { Action = Action.RevealAdjacent, Coordinate = new Coordinate(x, y) };
                        case "m":
                            return new GameInput { Action = Action.MarkAsMine, Coordinate = new Coordinate(x, y) };
                        case "u":
                            return new GameInput { Action = Action.UnmarkMine, Coordinate = new Coordinate(x, y) };
                    }
                }
            }
        }

        private void PrintHelp()
        {
            Console.WriteLine("Help:");
            Console.WriteLine("h     - Print this help text.");
            Console.WriteLine("r x y - Reveal field on location (x,y).");
            Console.WriteLine("a x y - reveal all fields around location (x,y).");
            Console.WriteLine("m x y - Mark field on location (x,y) as a mine.");
            Console.WriteLine("u x y - Remove mine marker from location (x,y).");
            Console.WriteLine("q     - Quit the game.");
        }

        public InitializationInput ReadInitializationInput()
        {
            Console.WriteLine("MySweeper");
            var (width, height) = this.GetDimensions();
            var mineCount = this.GetMineCount();

            return new InitializationInput { Width = width, Height = height, MineCount = mineCount };
        }

        private Tuple<int, int> GetDimensions()
        {
            Match match;
            do
            {
                Console.WriteLine("How large should the mine field be?");
                Console.Write("Width x Height: ");
                var input = Console.ReadLine();

                match = Regex.Match(input, @"^(\d+)\s*[x,]*\s*(\d+)$");
            }
            while (!match.Success);

            var width = int.Parse(match.Groups[1].Value);
            var height = int.Parse(match.Groups[2].Value);

            return new Tuple<int, int>(width, height);
        }

        private int GetMineCount()
        {
            string input;
            int mineCount;
            do
            {
                Console.WriteLine("How many mines should there be?");
                Console.Write("Mine count: ");
                input = Console.ReadLine();
            }
            while (!int.TryParse(input, out mineCount));

            return mineCount;
        }
    }
}