using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public class TileSet
    {
        private readonly List<Tile> tiles = new List<Tile>();

        public void Add(Tile tile)
        {
            tiles.Add(tile);
        }

        public void Remove(Tile tile)
        {
            tiles.Remove(tile);
        }

        public bool Includes(Tile tile)
        {
            return tiles.Contains(tile);
        }
    }
}
