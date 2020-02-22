using MySweeper.Basic;
using System.Linq;
using log4net;

namespace MySweeper.InputOutput.Commands
{
    public class RevealAdjacentFieldsCommand : AbstractRevealCommand
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RevealAdjacentFieldsCommand));
        public RevealAdjacentFieldsCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
            Logger.DebugFormat("Revealing adjacent fields of [{0}].", this.Coordinate);
            var nonExplosiveNeighbours = this.Minefield.GetNeighbours(this.Coordinate).Where(x => !x.BombMarked).ToList();
            this.RevealedCoordinates.AddRange(nonExplosiveNeighbours.Select(x => x.Coordinate));
            foreach (var neighbour in nonExplosiveNeighbours)
            {
                neighbour.IsRevealed = true;
                this.RevealNonExplosiveNeighbourhoods();
            }
        }
    }
}