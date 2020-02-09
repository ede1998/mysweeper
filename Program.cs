using MySweeper.InputOutput.Terminal;

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
                game.Execute(input);
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
