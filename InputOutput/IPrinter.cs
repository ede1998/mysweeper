using MySweeper.Basic;

namespace MySweeper.InputOutput
{
    public interface IPrinter
    {
        Minefield Minefield { get; set; }

        void PrintMinefield();

        void PrintGameLost();

        void PrintGameWon();
    }
}