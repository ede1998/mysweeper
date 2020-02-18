using System.Collections.Generic;
using MySweeper.InputOutput.Commands;

namespace MySweeper.Solver
{
    public class SolverSequence : List<ISolver>, ISolver
    {
        public void SetCommandFactory(CommandFactory factory)
        {
            foreach (var solver in this)
            {
                solver.SetCommandFactory(factory);
            }
        }

        public SolveResult Solve(Game g)
        {
            foreach (var solver in this)
            {
                SolveResult result;
                do
                {
                    result = solver.Solve(g);
                    if (g.GameLost || g.GameWon)
                        return SolveResult.Done;
                } while (result != SolveResult.Done);
            }
            return SolveResult.Done;
        }
    }
}