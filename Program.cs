using MySweeper.InputOutput.Terminal;
using MySweeper.InputOutput;

namespace MySweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var io = new Terminal();
            var init = io.TerminalReader.ReadInitializationInput();
            game.Initialize(init.MineCount, init.Width, init.Height);
            //Console.WriteLine("[{0}]", string.Join(',', game.Minefield));
            io.TerminalPrinter.SetGame(game);
            while (!game.GameLost)
            {
                io.TerminalPrinter.PrintMinefield();
                var input = io.TerminalReader.ReadGameInput();

                switch (input.Action)
                {
                    case Action.MarkAsMine:
                        game.MarkAsMine(input.Coordinate);
                    break;
                    case Action.UnmarkMine:
                        game.UnmarkMine(input.Coordinate);
                    break;
                    case Action.RevealAdjacent:
                        game.RevealAdjacentFields(input.Coordinate);
                    break;
                    case Action.RevealField:
                        game.RevealField(input.Coordinate);
                    break;
                }
            }

            game.RevealAll();
            io.TerminalPrinter.PrintGameOver();
        }
    }
}
