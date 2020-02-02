
public class Field
{
    public Coordinate Coordinate { get; }
    public bool HasMine { get; }
    public bool BombMarked { get; set; }
    public bool IsRevealed {
        get => this.isRevealed;
        set {
            if (!this.isRevealed)
            {
                this.isRevealed = true;
            }
        }
    }

    public int AdjacentMines { get; set; }

    public bool MineExploded => isRevealed && HasMine;
    
    private bool isRevealed;

    public Field(bool hasMine, Coordinate coordinate)
    {
        this.HasMine = hasMine;
        this.Coordinate = coordinate;
        this.BombMarked = false;
    }

    public override string ToString()
    {
        return $"Coordinate: [{this.Coordinate}], HasMine: [{this.HasMine}]";
    }
}