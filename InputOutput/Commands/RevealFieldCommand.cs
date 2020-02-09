using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public class RevealFieldCommand : AbstractRevealCommand
    {
        public RevealFieldCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
            var field = this.Minefield.GetValue(this.Coordinate);
            field.IsRevealed = true;
            this.RevealedCoordinates.Add(this.Coordinate);
            this.RevealNonExplosiveNeighbourhoods();
        }
    }
}