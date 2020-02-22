using MySweeper.Basic;
using MySweeper.InputOutput.Commands;
using System.Collections.Generic;
using System.Linq;
using log4net;

namespace MySweeper.Solver
{
    public class BasicMineMarkerSolver : ISolver
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(BasicMineMarkerSolver));
        private CommandFactory CommandFactory { get; set; }

        public void SetCommandFactory(CommandFactory factory)
        {
            this.CommandFactory = factory;
        }

        public SolveResult Solve(Game g)
        {
            Logger.DebugFormat("Solving game [{0}] with solver [{1}].", g, nameof(BasicMineMarkerSolver));
            var revealedFields = g.Minefield.Where(x => x.IsRevealed).Select(x => new FieldWithNeighbourhood(x, g.Minefield));
            var fieldsWithHiddenNeighbours = revealedFields.Where(HasHiddenNeighbours);
            var fieldsWithOnlyMinesAsHiddenNeighbours = fieldsWithHiddenNeighbours.Where(AllNeighboursLeftAreMines);
            var mines = fieldsWithOnlyMinesAsHiddenNeighbours.SelectMany(HiddenNeighbours).Distinct();

            foreach (var mine in mines)
            {
                var command = this.CommandFactory.CreateMarkAsMineCommand(mine.Coordinate);
                g.Execute(command);
            }

            return SolveResult.Done;
        }

        private static bool AllNeighboursLeftAreMines(FieldWithNeighbourhood f) => f.Neighbours.Count(x => !x.IsRevealed) == f.RemainingMines;
        private static IEnumerable<Field> HiddenNeighbours(FieldWithNeighbourhood f) => f.Neighbours.Where(x => !x.IsRevealed);
        private static bool HasHiddenNeighbours(FieldWithNeighbourhood f) => f.Neighbours.Any(x => !x.IsRevealed);
    }
}
