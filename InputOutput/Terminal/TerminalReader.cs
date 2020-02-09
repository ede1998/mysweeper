using MySweeper.Basic;
using MySweeper.InputOutput.Commands;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

namespace MySweeper.InputOutput.Terminal
{
    public class TerminalReader : IReader
    {
        private Minefield Minefield;
        private CommandFactory CommandFactory;
        private Dictionary<string, Func<Coordinate, ICommand>> StringToAction;

        public void Initialize(Minefield minefield, CommandFactory commandFactory)
        {
            this.Minefield = minefield;
            this.CommandFactory = commandFactory;
            this.StringToAction = new Dictionary<string, Func<Coordinate, ICommand>>
            {
                {"r", this.CommandFactory.CreateRevealFieldCommand},
                {"a", this.CommandFactory.CreateRevealAdjacentFieldCommand},
                {"m", this.CommandFactory.CreateMarkAsMineCommand},
                {"u", this.CommandFactory.CreateUnmarkMineCommand},
                {"h", PrintHelp},
                {"q", ExitApplication}
            };
        }

        public ICommand ReadGameInput()
        {
            ICommand command;
            do
            {
                var flagsRemaining = this.Minefield.MineCount - this.Minefield.MarkerCount;
                Console.Write($"Action (h for help, {flagsRemaining} flags): ");
                var input = Console.ReadLine();

                var commandString = ParseCommand(input);
                Console.WriteLine(commandString);

                var argument = ParseArguments(input) ?? new Coordinate(-1, -1);

                command = this.StringToAction[commandString](argument);
            }
            while(command == null);
            return command;
        }
        
        private static string ParseCommand(string input)
        {
            var match = Regex.Match(input, @"^([umrahq])\s*");
            return match.Success ? match.Groups[1].Value : string.Empty;
        }

        private static Coordinate? ParseArguments(string input)
        {
            var match = Regex.Match(input, @"\s+(\d+)\s+(\d+)$");
            if (match.Success)
            {
                var x = int.Parse(match.Groups[1].Value);
                var y = int.Parse(match.Groups[2].Value);
                var coordinate = new Coordinate(x, y);
                return coordinate;
            }
            return null;
        }

        private static ICommand ExitApplication(Coordinate _)
        {
            Environment.Exit(0);
            return null;
        }

        private static ICommand PrintHelp(Coordinate _)
        {
            Console.WriteLine("Help:");
            Console.WriteLine("h     - Print this help text.");
            Console.WriteLine("r x y - Reveal field on location (x,y).");
            Console.WriteLine("a x y - reveal all fields around location (x,y).");
            Console.WriteLine("m x y - Mark field on location (x,y) as a mine.");
            Console.WriteLine("u x y - Remove mine marker from location (x,y).");
            Console.WriteLine("q     - Quit the game.");
            return null;
        }

        #region Initialization

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

        #endregion Initialization
    }
}