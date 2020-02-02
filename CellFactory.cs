using System.Collections.Generic;
using System;
using System.Linq;

public class CellFactory
{
    public Game Game { get; set; }

    public string CreateCell(Field f)
    {
        var location = this.DetermineLocation(f.Coordinate);

        var cellString = cellDictionary[location];

        if (f.IsRevealed)
        {
            return cellString;
        }

        if(false) // IsMarked
        {
            return cellString.Replace(Placeholder, Flag);
        }
        
        return cellString.Replace(Placeholder, Hidden);
    }

    public int GetCellHeight(int row)
    {
        var location = this.DetermineLocation(new Coordinate(0, row));

        return cellDictionary[location].Split(Environment.NewLine).Length;
    }

    private static char Flag => 'X';
    private static char Hidden => '?';
    private static char Placeholder => 'N';

    private Location DetermineLocation(Coordinate coordinate)
    {
        if (this.IsBottomRight(coordinate)) return Location.BottomRight;
        if (this.IsTopRight(coordinate)) return Location.TopRight;
        if (this.IsRight(coordinate)) return Location.Right;
        if (this.IsTop(coordinate)) return Location.Top;
        if (this.IsBottom(coordinate)) return Location.Bottom;
        if (this.IsInner(coordinate)) return Location.Inner;

        throw new ArgumentOutOfRangeException(nameof(coordinate), coordinate, "Coordinate is not in the minefield.");
    }

    private bool IsBottomRight(Coordinate c) => c.X == this.MaxX && c.Y == this.MaxY;
    private bool IsTopRight(Coordinate c) => c.X == this.MaxX && c.Y == 0;
    private bool IsRight(Coordinate c) => c.X == this.MaxX && !c.Y.AnyOf(0, this.MaxY);
    private bool IsTop(Coordinate c) => c.X != this.MaxX && c.Y == 0;
    private bool IsBottom(Coordinate c) => c.Y == this.MaxY && c.X != this.MaxX;
    private bool IsInner(Coordinate c) => 0 <= c.X && c.X < this.MaxX && 0 <= c.Y && c.Y < this.MaxY;

    private int MaxX => this.Game.Width - 1;
    private int MaxY => this.Game.Height - 1;

    private enum Location { Inner, Right, Top, Bottom, TopRight, BottomRight }

    private static readonly Dictionary<Location, string> cellDictionary = new Dictionary<Location, string>
    {
        {Location.Inner,       $"|---{Environment.NewLine}| N "},
        {Location.Top,         $"____{Environment.NewLine}| N "},
        {Location.Bottom,      $"|---{Environment.NewLine}| N {Environment.NewLine}|___"},
        {Location.Right,       $"|---|{Environment.NewLine}| N |"},
        {Location.TopRight,    $"_____{Environment.NewLine}| N |{Environment.NewLine}|---|"},
        {Location.BottomRight, $"|---|{Environment.NewLine}| N |{Environment.NewLine}|___|"}
    };
}