using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public abstract class AbstractCommand : ICommand
    {
        public Coordinate Coordinate { get; }
        public Minefield Minefield { get; }

        public AbstractCommand(Minefield minefield, Coordinate coordinate)
        {
            this.Coordinate = coordinate;
            this.Minefield = minefield;
        }

        public abstract void Execute();
        public abstract void UndoExecution();
    }
}