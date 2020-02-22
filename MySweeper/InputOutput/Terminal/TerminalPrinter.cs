using MySweeper.Basic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace MySweeper.InputOutput.Terminal
{
    public class TerminalPrinter : IPrinter
    {
        public Minefield Minefield { get; set; }

        public void PrintMinefield()
        {
            var cellFactory = new CellFactory { Minefield = this.Minefield };
            var printedMinefield = this.Minefield.ToDictionary(x => x.Coordinate, cellFactory.CreateCell);

            var lineByLineMinefield = ConvertMinefieldToString(printedMinefield, cellFactory);

            Console.WriteLine(lineByLineMinefield);
        }

        private string ConvertMinefieldToString(Dictionary<Coordinate, string> printedMinefield, CellFactory cellFactory)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < this.Minefield.Height; i++)
            {
                for (int k = 0; k < cellFactory.GetCellHeight(i); k++)
                {
                    for (var j = 0; j < this.Minefield.Width; j++)
                    {
                        var cell = printedMinefield[new Coordinate(j, i)];
                        var rowsOfCell = cell.Split(Environment.NewLine);

                        if (k < rowsOfCell.Length)
                        {
                            stringBuilder.Append(rowsOfCell[k]);
                        }
                    }
                    stringBuilder.Append(Environment.NewLine);
                }
            }

            return stringBuilder.ToString();
        }

        public void PrintGameLost()
        {
            Console.WriteLine("========================== Game over ==========================");
            this.PrintMinefield();
            Console.WriteLine("========================== Game over ==========================");
        }

        public void PrintGameWon()
        {
            this.PrintMinefield();
            Console.WriteLine("Congratulations. You won the game.");
        }
    }
}