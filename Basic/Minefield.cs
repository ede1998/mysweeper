using System.Collections.Generic;
using System.Linq;

namespace MySweeper.Basic
{
    public class Minefield : HashSet<Field>
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Minefield()
        : base(new FieldComparer())
        {

        }

        public bool Contains(Coordinate item)
        {
            return this.Contains(new Field(false, item));
        }

        public Field GetValue(Coordinate item)
        {
            this.TryGetValue(new Field(false, item), out var result);
            return result;
        }

        public void Fill()
        {
            for (var i = 0; i < this.Width; i++)
            {
                for (var j = 0; j < this.Height; j++)
                {
                    var field = new Field(false, new Coordinate(i, j));
                    this.Add(field);
                }
            }
        }

        public void SetSize(int width, int height)
        {
            this.EnsureCapacity(width * height);
            this.Width = width;
            this.Height = height;
        }

        public IEnumerable<Field> GetNeighbours(Coordinate coordinate)
        {
            var x = coordinate.X;
            var y = coordinate.Y;

            var neighbours = new List<Coordinate>(8)
            {
               new Coordinate(x - 1, y),
               new Coordinate(x + 1, y),

               new Coordinate(x - 1, y - 1),
               new Coordinate(x,     y - 1),
               new Coordinate(x + 1, y - 1),

               new Coordinate(x - 1, y + 1),
               new Coordinate(x,     y + 1),
               new Coordinate(x + 1, y + 1),
            };

            return neighbours
                        .Where(x => this.Contains(coordinate))
                        .Select(this.GetValue)
                        .Where(x => x != null);
        }
    }
}