using MySweeper.Basic;
using log4net;

namespace MySweeper.InputOutput.Commands
{
    public class RevealFieldCommand : AbstractRevealCommand
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(RevealFieldCommand));
        public RevealFieldCommand(Minefield minefield, Coordinate coordinate)
        : base(minefield, coordinate)
        {
        }

        public override void Execute()
        {
            Logger.DebugFormat("Revealing field [{0}}.", this.Coordinate);
            var field = this.Minefield.GetValue(this.Coordinate);
            field.IsRevealed = true;
            this.RevealedCoordinates.Add(this.Coordinate);
            this.RevealNonExplosiveNeighbourhoods();
        }
    }
}