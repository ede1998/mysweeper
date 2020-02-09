using System.Linq;
using System.Collections.Generic;
using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public abstract class AbstractRevealCommand : AbstractCommand
    {
        public AbstractRevealCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
            this.RevealedCoordinates = new List<Coordinate>();
        }

        protected List<Coordinate> RevealedCoordinates { get; }

        public override void UndoExecution()
        {
            foreach (var revealedCoordinate in this.RevealedCoordinates)
            {
                this.Minefield.GetValue(revealedCoordinate).IsRevealed = false;
            }
        }

        protected void RevealNonExplosiveNeighbourhoods()
        {
            var minefieldModified = true;
            while (minefieldModified)
            {
                minefieldModified = false;
                var fieldsInNonExplosiveNeighbourhood = this.Minefield.Where(x => x.AdjacentMines == 0 && x.IsRevealed);
                var nonExplosiveFields = fieldsInNonExplosiveNeighbourhood
                                            .SelectMany(x => this.Minefield.GetNeighbours(x.Coordinate))
                                            .Where(x => !x.IsRevealed);

                foreach (var field in nonExplosiveFields)
                {
                    field.IsRevealed = true;
                    minefieldModified = true;
                    this.RevealedCoordinates.Add(field.Coordinate);
                }
            }
        }

    }
}
