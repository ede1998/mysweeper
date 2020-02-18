using MySweeper.InputOutput.Commands;

namespace MySweeper.Solver
{
    public interface ISolver
    {
        SolveResult Solve(Game g);
        void SetCommandFactory(CommandFactory factory);
    }
}