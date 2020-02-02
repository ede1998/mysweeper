using MySweeper.InputOutput.Terminal;
using System;

namespace MySweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var game = new Game();
            game.Initialize(50,20,10);
            //Console.WriteLine("[{0}]", string.Join(',', game.Minefield));
            var terminalInteractor = new TerminalInteractor();
            terminalInteractor.SetGame(game);
            terminalInteractor.Print();
        }
    }
}
