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
            return new MarkAsMineCommand(this.Minefield, coordinate);
        }

        public ICommand CreateRevealAdjacentFieldCommand(Coordinate coordinate)
        {
            return new RevealAdjacentFieldCommand(this.Minefield, coordinate);
        }

        public ICommand CreateRevealFieldCommand(Coordinate coordinate)
        {
            return new RevealFieldCommand(this.Minefield, coordinate);
        }

        public ICommand CreateUnmarkMineCommand(Coordinate coordinate)
        {
            return new UnmarkMineCommand(this.Minefield, coordinate);
        }
    }
}