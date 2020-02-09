using MySweeper.Basic;
using MySweeper.InputOutput.Commands;

namespace MySweeper.InputOutput
{
    public interface IReader
    {
        void Initialize(Minefield minefield, CommandFactory commandFactory);
        ICommand ReadGameInput();
        InitializationInput ReadInitializationInput();
    }
}