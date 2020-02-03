using MySweeper.Basic;

namespace MySweeper.InputOutput
{
    public interface IReader
    {
        Minefield Minefield { get; set; }
        GameInput ReadGameInput();
        InitializationInput ReadInitializationInput();
    }
}