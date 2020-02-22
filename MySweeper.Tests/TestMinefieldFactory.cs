using System.IO.Enumeration;
using System.Linq;
using System;
using MySweeper.Basic;
using MySweeper.Helper;
using System.Collections.Generic;

namespace MySweeper.Tests
{
    class TestMinefieldFactory
    {
        public const char HiddenMine = 'X';
        public const char MarkedMine = 'M';
        public static Minefield CreateMinefield(string inputField)
        {
            var minefield = new Minefield();
            foreach (var (i, row) in SplitLines(inputField).Enumerate())
            {
                var skipped = 0;
                foreach (var (j, column) in row.Enumerate())
                {
                    var field = ParseCharacter(column, new Coordinate(j - skipped, i));

                    if (field != null)
                    {
                        minefield.Add(field);
                    }
                    else
                    {
                        ++skipped;
                    }
                }
            }
            return minefield;
        }

        private static IEnumerable<string> SplitLines(string field)
        {
            return field.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
        }

        private static Field ParseCharacter(char column, Coordinate coordinate)
        {
            Field field = null;
            switch (column)
            {
                case HiddenMine:
                    field = new Field(true, coordinate);
                    field.BombMarked = true;
                    break;
                case MarkedMine:
                    field = new Field(true, coordinate);
                    break;
                case var n when (char.IsDigit(n)):
                    field = new Field(false, coordinate);
                    field.AdjacentMines = n;
                    break;
            }
            return field;
        }
    }
}