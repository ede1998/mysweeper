using MySweeper.InputOutput.Commands;
using MySweeper.InputOutput.Terminal;
using MySweeper.Solver;

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

            if (init.UseSolver)
            {
                var solvers = new SolverSequence
                {
                    new StartSolver(),
                    new BasicMineMarkerSolver(),
                    new BasicFieldRevealerSolver(),
                    new BasicMineMarkerSolver(),
                    new BasicFieldRevealerSolver(),
                };
                solvers.SetCommandFactory(new CommandFactory(game.Minefield));
                solvers.Solve(game);
            }

            while (!game.GameLost && !game.GameWon)
            {
                io.TerminalPrinter.PrintMinefield();
                var input = io.TerminalReader.ReadGameInput();

                if (input == null)
                {
                    break;
                }

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
