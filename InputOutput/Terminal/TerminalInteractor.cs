using MySweeper.Basic;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;

namespace MySweeper.InputOutput.Terminal
{
    public class TerminalInteractor : IReader, IPrinter
    {
        private Game game;

        public void SetGame(Game g)
        {
            this.game = g;
        }

        public void Print()
        {
            var cellFactory = new CellFactory { Game = this.game };
            var printedMinefield = this.game.Minefield.ToDictionary(x => x.Coordinate, cellFactory.CreateCell);

            var lineByLineMinefield = ConvertMinefieldToString(printedMinefield, cellFactory);

            Console.WriteLine(lineByLineMinefield);
        }

        private string ConvertMinefieldToString(Dictionary<Coordinate, string> printedMinefield, CellFactory cellFactory)
        {
            var stringBuilder = new StringBuilder();

            for (var i = 0; i < this.game.Height; i++)
            {
                for (int k = 0; k < cellFactory.GetCellHeight(i); k++)
                {
                    for (var j = 0; j < this.game.Width; j++)
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

        public Coordinate Read()
        {
            throw new System.NotImplementedException();
        }
    }
}