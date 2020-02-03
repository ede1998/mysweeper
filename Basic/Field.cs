namespace MySweeper.Basic
{
    public class Field
    {
        public Coordinate Coordinate { get; }
        public bool HasMine { get; }
        public bool BombMarked
        {
            get => this.bombMarked;
            set
            {
                if (this.isRevealed)
                {
                    return;
                }
                this.bombMarked = value;
            }
        }
        public bool IsRevealed
        {
            get => this.isRevealed;
            set
            {
                if (!this.isRevealed)
                {
                    this.isRevealed = true;
                    this.BombMarked = false;
                }
            }
        }

        public int AdjacentMines { get; set; }

        public bool MineExploded => isRevealed && HasMine;

        private bool isRevealed;
        private bool bombMarked;

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
}