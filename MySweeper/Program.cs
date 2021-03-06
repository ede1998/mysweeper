﻿using MySweeper.InputOutput.Commands;
using MySweeper.InputOutput.Terminal;
using MySweeper.Solver;
using log4net;

namespace MySweeper
{
    class Program
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(Program));
        static void Main(string[] args)
        {
            Logger.Debug("MySweeper started");
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
                    new SolverSequence(true)
                    {
                        new BasicMineMarkerSolver(),
                        new BasicFieldRevealerSolver()
                    }
                };
                solvers.SetCommandFactory(new CommandFactory(game.Minefield));
                solvers.Solve(game);
            }

            while (!game.IsFinished)
            {
                io.TerminalPrinter.PrintMinefield();
                var input = io.TerminalReader.ReadGameInput();

                if (input == null)
                {
                    break;
                }

                game.Execute(input);
            }

            if (game.IsWon)
            {
                io.TerminalPrinter.PrintGameWon();
            }
            else if (game.IsLost)
            {
                game.RevealAll();
                io.TerminalPrinter.PrintGameLost();
            }
        }
    }
}
