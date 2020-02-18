using MySweeper.Basic;
using MySweeper.InputOutput.Commands;
using System;

namespace MySweeper.Solver
{
    public class StartSolver : ISolver
    {
        private CommandFactory CommandFactory { get; set; }

        public void SetCommandFactory(CommandFactory factory)
        {
            this.CommandFactory = factory;
        }

        public SolveResult Solve(Game g)
        {
            var rng = new Random();
            var x = rng.Next(0, g.Minefield.Width);
            var y = rng.Next(0, g.Minefield.Height);

            var command = this.CommandFactory.CreateRevealFieldCommand(new Coordinate(x,y));
            g.Execute(command);

            return SolveResult.Done;
        }
    }
}
