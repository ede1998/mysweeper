using MySweeper.InputOutput.Commands;
using System.Linq;
using log4net;

namespace MySweeper.Solver
{
    public class BasicFieldRevealerSolver : ISolver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BasicFieldRevealerSolver));
        private CommandFactory CommandFactory { get; set; }

        public void SetCommandFactory(CommandFactory factory)
        {
            this.CommandFactory = factory;
        }

        public SolveResult Solve(Game g)
        {
            Logger.DebugFormat("Solving game [{0}] with solver [{1}].", g, nameof(BasicFieldRevealerSolver));

            var revealedFields = g.Minefield.Where(x => x.IsRevealed).Select(x => new FieldWithNeighbourhood(x, g.Minefield));
            var fieldsWithHiddenNeighbours = revealedFields.Where(x => x.Neighbours.Any(n => !n.IsRevealed));
            var revealableFields = fieldsWithHiddenNeighbours.Where(AllBombsMarked).ToList();

            foreach (var field in revealableFields)
            {
                var command = this.CommandFactory.CreateRevealAdjacentFieldsCommand(field.Field.Coordinate);
                g.Execute(command);
            }

            return revealableFields.Any() ? SolveResult.Rerun : SolveResult.NothingToDo;
        }

        private static bool AllBombsMarked(FieldWithNeighbourhood field)
        {
            return field.RemainingMines == 0;
        }
    }
}
