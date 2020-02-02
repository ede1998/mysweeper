using MySweeper.Basic;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MySweeper
{
    public class Game
    {
        public Minefield Minefield { get; }

        public bool GameLost { get => this.Minefield.Any(x => x.MineExploded); }

        public Game()
        {
            this.Minefield = new Minefield();
        }

        public void RevealAll()
        {
            foreach (var field in this.Minefield)
            {
                field.IsRevealed = true;
            }
        }

        #region Playing

        public void RevealField(Coordinate c)
        {
            var field = this.Minefield.GetValue(c);
            field.IsRevealed = true;
        }

        public void RevealAdjacentFields(Coordinate c)
        {
            var nonExplosiveNeighbours = this.Minefield.GetNeighbours(c).Where(x => !x.BombMarked);
            foreach (var neighbour in nonExplosiveNeighbours)
            {
                neighbour.IsRevealed = true;
            }
        }

        public void MarkAsMine(Coordinate c)
        {
            var field = this.Minefield.GetValue(c);
            field.BombMarked = true;
        }

        public void UnmarkMine(Coordinate c)
        {
            var field = this.Minefield.GetValue(c);
            field.BombMarked = false;
        }

        #endregion Playing

        #region OnStartup

        public void Initialize(int mineCount, int sizeX, int sizeY)
        {
            this.InitializeMinefield(mineCount, sizeX, sizeY);
            this.UpdateFields();
        }

        private void InitializeMinefield(int mineCount, int sizeX, int sizeY)
        {
            this.Minefield.SetSize(sizeX, sizeY);

            var mines = GenerateMineLocations(mineCount, sizeX, sizeY);
            foreach (var mine in mines)
            {
                this.Minefield.Add(new Field(true, mine));
            }

            this.Minefield.Fill();
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
                var neighbours = this.Minefield.GetNeighbours(field.Coordinate);

                field.AdjacentMines = neighbours.Count(x => x.HasMine);
            }
        }
    }

    #endregion OnStartup

}