using System.Runtime.CompilerServices;
public struct Coordinate
{
    public int X { get; }
    public int Y { get; }

    public Coordinate(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public override string ToString()
    {
        return $"X: [{this.X}], Y: [{this.Y}]";
    }

    public override bool Equals(object obj)
    {
        var other = obj as Coordinate?;
        if (other == null)
        {
            return false;
        }

        return this.X == other.Value.X && this.Y == other.Value.Y;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator==(Coordinate c1, Coordinate c2)
    {
        return c1.X == c2.X && c1.Y == c2.Y;
    }

    public static bool operator!=(Coordinate c1, Coordinate c2)
    {
        return !(c1 == c2);
    }
}