using System.Linq;
using System;
using System.Collections.Generic;

public class Game
{
    public Dictionary<Coordinate, Field> Minefield { get; }

    public int Width { get; private set; }
    public int Height { get; private set; }

    public bool GameLost { get => this.Minefield.Any(x => x.Value.MineExploded); }

    public Game()
    {
        this.Minefield = new Dictionary<Coordinate, Field>();
    }

    public void Initialize(int mineCount, int sizeX, int sizeY)
    {
        this.Minefield.EnsureCapacity(sizeX*sizeY);
        this.Width = sizeX;
        this.Height = sizeY;

        var mines = GenerateMineLocations(mineCount, sizeX, sizeY);
        foreach (var mine in mines)
        {
            this.Minefield.Add(mine, new Field(true, mine));
        }

        for (var i = 0; i < sizeX; i++)
        {
            for (var j = 0; j < sizeY; j++)
            {
                var field = new Field(false, new Coordinate(i,j));
                this.Minefield.TryAdd(field.Coordinate, field);
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

            var coordinate = new Coordinate(x,y);

            mines.Add(coordinate);
        }

        return mines.ToList();        
    }
}