using MySweeper.Basic;
using MySweeper.InputOutput.Commands;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MySweeper
{
    public class Game
    {
        public Minefield Minefield { get; }
        public List<ICommand> ExecutedCommands { get; }

        public bool IsLost => this.Minefield.Any(x => x.MineExploded);
        public bool IsWon => this.Minefield.All(x => (x.BombMarked && x.HasMine) ^ x.IsRevealed);
        public bool IsFinished => this.IsLost || this.IsWon;

        public Game()
        {
            this.Minefield = new Minefield();
            this.ExecutedCommands = new List<ICommand>();
        }

        public void RevealAll()
        {
            foreach (var field in this.Minefield)
            {
                field.IsRevealed = true;
            }
        }
        
        #region Playing

        public void Execute(ICommand command)
        {
            this.ExecutedCommands.Add(command);
            command.Execute();
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