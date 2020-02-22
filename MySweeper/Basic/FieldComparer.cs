using System.Collections.Generic;

namespace MySweeper.Basic
{
    public class FieldComparer : IEqualityComparer<Field>
    {
        public bool Equals(Field x, Field y)
        {
            if (x == null ^ y == null)
            {
                return false;
            }

            return x?.Coordinate == y?.Coordinate;
        }


        public int GetHashCode(Field f)
        {
            if (f == null)
            {
                return 0;
            }

            return f.Coordinate.GetHashCode();
        }
    }
}