
public class Field
{
    public Coordinate Coordinate { get; }

    private bool HasMine { get; set; }

    public Field(bool hasMine, Coordinate coordinate)
    {
        this.HasMine = hasMine;
        this.Coordinate = coordinate;
    }

    public override string ToString()
    {
        return $"Coordinate: [{this.Coordinate}], HasMine: [{this.HasMine}]";
    }
}