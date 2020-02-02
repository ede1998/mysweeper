using System;
using System.Linq;
using System.Text;

public class TerminalInteractor : IReader, IPrinter
{
    private static readonly string InnerCell = $"|---{Environment.NewLine}| N ";

    private static readonly string TopCell = $"____{Environment.NewLine}| N ";

    private static readonly string BottomCell = $"|---{Environment.NewLine}| N {Environment.NewLine}|___";
    private static readonly string RightCell = $"|---|{Environment.NewLine}| N |";
    private static readonly string TopRightCell = $"_____{Environment.NewLine}| N |{Environment.NewLine}|---|";
    private static readonly string BottomRightCell = $"|---|{Environment.NewLine}| N |{Environment.NewLine}|___|";
    
    private Game game;

    public void SetGame(Game g)
    {
        this.game = g;
    }

    public void Print()
    {
        var stringBuilder = new StringBuilder();
        var printedMinefield = this.game.Minefield.Keys.ToDictionary(x => x, CreateCell);

        for (var i = 0; i < this.game.Height; i++)
        {
            for (int k = 0; k < GetColumnHeight(i); k++)
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
        Console.WriteLine(stringBuilder.ToString());
    }

    private int GetColumnHeight(int row)
    {
        if (row == this.game.Height - 1)
        {
            return 3;
        }
        
        return 2;
    }

    private string CreateCell(Coordinate c)
    {
        if (c.Y == 0)
        {
            if(c.X == this.game.Width - 1)
            {
                return TopRightCell;
            }

            return TopCell;
        }

        if (c.Y == this.game.Height - 1)
        {
            if (c.X == this.game.Width - 1)
            {
                return BottomRightCell;
            }

            return BottomCell;
        }

        if (c.X == this.game.Width - 1)
        {
            return RightCell;
        }
        

        return InnerCell;
    }

    public Coordinate Read()
    {
        throw new System.NotImplementedException();
    }
}