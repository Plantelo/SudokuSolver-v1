using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public class Board
    {
        private readonly Tile[,] tiles = new Tile[9, 9];
        private readonly TileSet[] rows = new TileSet[9];
        private readonly TileSet[] cols = new TileSet[9];
        private readonly TileSet[] blocks = new TileSet[9];
        private readonly TileSet[] diagonals = new TileSet[2];

        private static Board instance = new Board();

        private Board()
        {
            for (int i = 0; i < 9; i++)
            {
                rows[i] = new TileSet();
                cols[i] = new TileSet();
                blocks[i] = new TileSet();
            }
            diagonals[0] = new TileSet();
            diagonals[1] = new TileSet();

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    tiles[i, j] = new Tile();

                    // setup collections: solution engine component
                    tiles[i, j].AddTo(rows[i]);
                    tiles[i, j].AddTo(cols[j]);
                    tiles[i, j].AddTo(blocks[(int)Math.Floor(i / 3d) + j - (j % 3)]);
                    if (i == j)
                        tiles[i, j].AddTo(diagonals[0]);
                    else if (i + j == 8)
                        tiles[i, j].AddTo(diagonals[1]);
                }
            }
        }

        public static Board GetInstance() { return instance; }

        public Tile GetTile(int[] pos)
        {
            return pos[0] < 0 ? null : tiles[pos[0], pos[1]];
        }

        public void SetTileValue(int x, int y, int val)
        {
            tiles[x, y].SetValue(val);
        }
    }
}
