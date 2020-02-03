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
            io.Initialize(game.Minefield);

            while (!game.GameLost && !game.GameWon)
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

            if (game.GameWon)
            {
                io.TerminalPrinter.PrintGameWon();
            }
            else if (game.GameLost)
            {
                game.RevealAll();
                io.TerminalPrinter.PrintGameLost();
            }
        }
    }
}
