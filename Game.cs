using MySweeper.Basic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MySweeper
{
    public class Game
    {
        public Minefield Minefield { get; }

        public int Width { get; private set; }
        public int Height { get; private set; }

        public bool GameLost { get => this.Minefield.Any(x => x.MineExploded); }

        public Game()
        {
            this.Minefield = new Minefield();
        }

        public void Initialize(int mineCount, int sizeX, int sizeY)
        {
            this.InitializeMinefield(mineCount, sizeX, sizeY);
            this.UpdateFields();
        }

        private void InitializeMinefield(int mineCount, int sizeX, int sizeY)
        {
            this.Minefield.EnsureCapacity(sizeX * sizeY);
            this.Width = sizeX;
            this.Height = sizeY;

            var mines = GenerateMineLocations(mineCount, sizeX, sizeY);
            this.Minefield.UnionWith(mines.Select(x => new Field(true, x)));

            for (var i = 0; i < sizeX; i++)
            {
                for (var j = 0; j < sizeY; j++)
                {
                    var field = new Field(false, new Coordinate(i, j));
                    this.Minefield.Add(field);
                }
            }

        }

        private List<Coordinate> GenerateMineLocations(int mineCount, int sizeX, int sizeY)
        {
            if (mineCount >= sizeX * sizeY)
            {
                throw new ArgumentOutOfRangeException(nameof(mineCount), mineCount, "mineCount must be smaller than number of fields.");
            }

            var mines = new HashSet<Coordinate>(mineCount);
            var rand = new Random();

            while (mines.Count < mineCount)
            {
                var x = rand.Next(0, sizeX);
                var y = rand.Next(0, sizeY);

                var coordinate = new Coordinate(x, y);

                mines.Add(coordinate);
            }

            return mines.ToList();
        }

        private void UpdateFields()
        {
            foreach (var field in this.Minefield)
            {
                var neighbourCoordinates = this.GetNeighbours(field.Coordinate);
                var neighbours = this.Minefield.Where(x => neighbourCoordinates.Contains(x.Coordinate));

                field.AdjacentMines = neighbours.Count(x => x.HasMine);
            }
        }

        private IEnumerable<Coordinate> GetNeighbours(Coordinate coordinate)
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

            return neighbours.Where(x => this.Minefield.Contains(coordinate));
        }
    }
}