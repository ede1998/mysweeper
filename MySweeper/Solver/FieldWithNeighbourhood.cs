using MySweeper.Basic;
using System.Collections.Generic;
using System.Linq;

namespace MySweeper.Solver
{
    public class FieldWithNeighbourhood
    {
        public Field Field { get; }

        public List<Field> Neighbours { get; }

        public int RemainingMines => this.Field.AdjacentMines - this.Neighbours.Count(x => x.BombMarked);

        public FieldWithNeighbourhood(Field field, Minefield minefield)
        {
            this.Field = field;
            this.Neighbours = minefield.GetNeighbours(field.Coordinate).ToList();
        }
    }
}
