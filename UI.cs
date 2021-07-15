using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Linq;

namespace SudokuSolver
{
    class UI
    {
        private static readonly Rectangle[,] tiles = new Rectangle[9,9];
        private static readonly Dictionary<Rectangle, TextBlock> labels = new Dictionary<Rectangle, TextBlock>();
        private static Dictionary<Rectangle, int[]> positions = new Dictionary<Rectangle, int[]>();

        /// <summary>
        /// Initialize board components and place them into their respective
        /// collections.
        /// </summary>
        public static void Initialize(MainWindow mainWindow)
        {
            Grid mainGrid = (Grid) mainWindow.FindName("mainGrid");

            Input.SetMainGrid(mainGrid);

            // setup buttons and button events
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Button control = new Button();
                    mainGrid.Children.Add(control);

                    control.Content = i + 1 + (j * 3);
                    control.Name = $"_{i + 1 + (j * 3)}";

                    control.Width = 18;
                    control.Height = 20;
                    control.Focusable = false;

                    control.HorizontalAlignment = HorizontalAlignment.Center;
                    control.VerticalAlignment = VerticalAlignment.Center;
                    control.Margin = new Thickness(i * 50 - 350, (j - 1) * 50, 0, 0);

                    control.Click += Events.ControlClicked;
                }
            }

            ((Button)mainGrid.FindName("erase")).Click += Events.ControlClicked;
            ((Button)mainGrid.FindName("even")).Click += Events.ControlClicked;
            ((Button)mainGrid.FindName("odd")).Click += Events.ControlClicked;


            // init tiles, prepare foreach loop
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Rectangle tile = new Rectangle();
                    TextBlock label = new TextBlock();

                    mainGrid.Children.Add(tile);
                    mainGrid.Children.Add(label);

                    tiles[i,j] = tile;
                    labels[tile] = label;
                    positions[tile] = new int[] { i, j };
                }
            }

            foreach (Rectangle tile in tiles)
            {
                // find position
                int[] pos = FindRectanglePos(tile);
                int x = pos[0];
                int y = pos[1];

                // setup visuals
                tile.Stroke = Brushes.Black;
                tile.Fill = Brushes.White;
                tile.Width = 20;
                tile.Height = 20;

                // position correctly
                tile.HorizontalAlignment = HorizontalAlignment.Center;
                tile.VerticalAlignment = VerticalAlignment.Center;
                tile.Margin = new Thickness((x - 4) * 40, (y - 4) * 40, 0, 0);

                tile.MouseLeftButtonDown += Events.TileClicked;

                TextBlock text = labels[tile];

                // setup text block
                text.HorizontalAlignment = HorizontalAlignment.Center;
                text.VerticalAlignment = VerticalAlignment.Center;
                text.Margin = new Thickness((x - 4) * 40, (y - 4) * 40, 0, 0);
                text.Width = 20;
                text.Height = 20;

                text.TextAlignment = TextAlignment.Center;
                text.IsHitTestVisible = false;
            }
        }

        public static int[] FindRectanglePos(Rectangle r)
        {
            try
            {
                return positions[r];
            }
            catch (KeyNotFoundException)
            {
                return new int[] { -1, -1 };
            }
        }

        public static Rectangle FindRectangle(int[] pos)
        {
            if (pos[0] < 0) return null;
            return tiles[pos[0], pos[1]];
        }

        public static void ChangeLabel(Rectangle tile, string content)
        {
            labels[tile].Text = content;

            int[] pos = FindRectanglePos(tile);
            Tile cor = Board.GetInstance().GetTile(pos);
            cor.SetValue(content == "" ? 0 : int.Parse(content));
        }
    }
}
