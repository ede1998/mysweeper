using System.Linq;
using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public class RevealAdjacentFieldCommand : AbstractRevealCommand
    {
        public RevealAdjacentFieldCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
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