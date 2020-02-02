namespace MySweeper.InputOutput
{
    public interface IPrinter
    {
        void SetGame(Game g);

        void PrintMinefield();

        void PrintGameOver();
    }
}