using MySweeper.InputOutput.Commands;
using System.Linq;

namespace MySweeper.Solver
{
    public class BasicFieldRevealerSolver : ISolver
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
            var revealableFields = fieldsWithHiddenNeighbours.Where(AllBombsMarked).ToList();

            foreach (var field in revealableFields)
            {
                var command = this.CommandFactory.CreateRevealAdjacentFieldsCommand(field.Field.Coordinate);
                g.Execute(command);
            }

            return revealableFields.Any() ? SolveResult.Rerun : SolveResult.Done;
        }

        private static bool AllBombsMarked(FieldWithNeighbourhood field)
        {
            return field.RemainingMines == 0;
        }
    }
}
