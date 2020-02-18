using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public class CommandFactory
    {
        public Minefield Minefield { get; }
        public CommandFactory(Minefield minefield)
        {
            this.Minefield = minefield;
        }

        public ICommand CreateMarkAsMineCommand(Coordinate coordinate)
        {
            if (this.CoordinateOutOfBounds(coordinate)) return null;
            return new MarkAsMineCommand(this.Minefield, coordinate);
        }

        public ICommand CreateRevealAdjacentFieldCommand(Coordinate coordinate)
        {
            if (this.CoordinateOutOfBounds(coordinate)) return null;
            return new RevealAdjacentFieldCommand(this.Minefield, coordinate);
        }

        public ICommand CreateRevealFieldCommand(Coordinate coordinate)
        {
            if (this.CoordinateOutOfBounds(coordinate)) return null;
            return new RevealFieldCommand(this.Minefield, coordinate);
        }

        public ICommand CreateUnmarkMineCommand(Coordinate coordinate)
        {
            if (this.CoordinateOutOfBounds(coordinate)) return null;
            return new UnmarkMineCommand(this.Minefield, coordinate);
        }

        private bool CoordinateOutOfBounds(Coordinate coordinate)
        {
            return coordinate.X < 0 || coordinate.X >= this.Minefield.Width || coordinate.Y < 0 || coordinate.Y >= this.Minefield.Height;
        }
    }
}