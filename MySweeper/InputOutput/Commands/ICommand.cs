using MySweeper.Basic;

namespace MySweeper.InputOutput.Commands
{
    public interface ICommand
    {
        Coordinate Coordinate { get; }
        Minefield Minefield { get; }

        void Execute();

        void UndoExecution();
    }
}