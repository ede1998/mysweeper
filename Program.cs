using System;

namespace mysweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var game = new Game();
            game.Initialize(10,10,10);
            Console.WriteLine("[{0}]", string.Join(',', game.Minefield));
        }
    }
}
