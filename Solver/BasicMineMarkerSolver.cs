using MySweeper.InputOutput.Commands;
using System.Linq;

namespace MySweeper.Solver
{
    public class BasicMineMarkerSolver : ISolver
    {
        private CommandFactory CommandFactory { get; set; }

        public void SetCommandFactory(CommandFactory factory)
        {
            this.CommandFactory = factory;
        }

        public SolveResult Solve(Game g)
        {
            var revealedFields = g.Minefield.Where(x => x.IsRevealed).Select(x => new FieldWithNeighbourhood(x, g.Minefield));
            var fieldsWithHiddenNeighbours = revealedFields.Where(x => x.Neighbours.Any(n => !n.IsRevealed));
            var mines = fieldsWithHiddenNeighbours.Where(RemainingFieldsAreMines);

            foreach (var mine in mines)
            {
                var command = this.CommandFactory.CreateMarkAsMineCommand(mine.Field.Coordinate);
                g.Execute(command);
            }

            return mines.Any() ? SolveResult.Rerun : SolveResult.Done;
        }

        private static bool RemainingFieldsAreMines(FieldWithNeighbourhood field)
        {
            var hidden = field.Neighbours.Count(x => !x.IsRevealed);

            return hidden == field.RemainingMines;
        }
    }
}
