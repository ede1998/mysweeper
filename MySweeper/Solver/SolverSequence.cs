using MySweeper.InputOutput.Commands;
using System.Collections.Generic;
using log4net;

namespace MySweeper.Solver
{
    public class SolverSequence : List<ISolver>, ISolver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(SolverSequence));
        private bool IsRepeating { get; }
        public SolverSequence(bool isRepeating = false)
        {
            this.IsRepeating = isRepeating;
        }

        public void SetCommandFactory(CommandFactory factory)
        {
            foreach (var solver in this)
            {
                solver.SetCommandFactory(factory);
            }
        }

        public SolveResult Solve(Game g)
        {
            SolveResult result;
            do
            {
                result = this.RunAllSolvers(g);
                Logger.DebugFormat("Finished running all solvers with result [{0}].", result);
            }
            while (this.IsRepeating && result != SolveResult.NothingToDo && !g.IsFinished);
            return result;
        }

        private SolveResult RunAllSolvers(Game g)
        {
            var result = SolveResult.NothingToDo;
            foreach (var solver in this)
            {
                var singleResult = RunSolverUntilDone(g, solver);
                if (singleResult != SolveResult.NothingToDo)
                {
                    result = singleResult;
                }
                if (g.IsFinished)
                {
                    Logger.Info("Game is finished. Stopping solving.");
                    return SolveResult.Done;
                }
            }
            return result;
        }

        private static SolveResult RunSolverUntilDone(Game g, ISolver solver)
        {
            var result = SolveResult.Rerun;
            while (result == SolveResult.Rerun)
            {
                result = solver.Solve(g);
                if (g.IsFinished)
                    return SolveResult.Done;
            }
            Logger.DebugFormat("Finished running solver [{0}] with result [{1}].", solver.GetType().Name, result);
            return result;
        }
    }
}