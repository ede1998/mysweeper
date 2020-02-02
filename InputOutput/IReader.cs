using MySweeper.Basic;

namespace MySweeper.InputOutput
{
    public interface IReader
    {
        GameInput ReadGameInput();
        InitializationInput ReadInitializationInput();
    }
}