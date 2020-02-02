
public class Field
{
    public Coordinate Coordinate { get; }

    public bool IsRevealed {
        get => this.isRevealed;
        set {
            if (!this.isRevealed)
            {
                this.isRevealed = true;
            }
        }
    }

    public bool MineExploded { get => isRevealed && hasMine; }
    
    private bool isRevealed;
    private bool hasMine;

    public Field(bool hasMine, Coordinate coordinate)
    {
        this.hasMine = hasMine;
        this.Coordinate = coordinate;
    }

    public override string ToString()
    {
        return $"Coordinate: [{this.Coordinate}], HasMine: [{this.hasMine}]";
    }
}