using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace SudokuSolver
{
    public class Input
    {
        /**
         * -2: tile
         * -1: none
         * 0: erase
         * 1-9: numbers
         * 10: even squares
         * 11: odd squares
         * 12: thermometers *WIP*
         */
        private static ControlStates activeControl = ControlStates.none;

        public static ControlStates ActiveControl() { return activeControl; }

        // used for selecting tile first, value second
        private static int[] lastActiveTile = { -1, -1 };

        // element to use for logical tree searches
        private static Grid mainGrid;

        // used for selecting value first, tile second
        private static Button lastHighlightedButton;

        ///<summary>
        /// Used for externally setting main grid element during UI initialization.
        ///</summary>
        public static void SetMainGrid(Grid grid) { mainGrid = grid; }

        /// <summary>
        /// Determine which tile (if any) should be highlighted after a tile is pressed on.
        /// </summary>
        /// <param name="tile">Tile that was pressed.</param>
        public static void SetActiveTile(Rectangle tile)
        {
            int[] nextActiveTile = UI.FindRectanglePos(tile);

            activeControl =
                // if tile is selected and selected tile position matches new selection, remove selection
                activeControl == ControlStates.tile && nextActiveTile[0] == lastActiveTile[0] && nextActiveTile[1] == lastActiveTile[1]
                ? ControlStates.none : ControlStates.tile;

            // apply findings to color engine
            ChangeTileHighlight(nextActiveTile);
        }

        /// <summary>
        /// Get position of highlighted tile (if any).
        /// </summary>
        /// <returns>Position ot the highlighted tile, if one exists, otherwise {-1, -1}.</returns>
        public static int[] GetActiveTile()
        {
            return activeControl == ControlStates.tile ? lastActiveTile : new int[] { -1, -1 };
        }

        /// <summary>
        /// Change which control is considered active, or apply control effects to highlighted tile.
        /// </summary>
        /// <param name="control">Control that was pressed.</param>
        public static void SwitchActiveControl(ControlStates control)
        {
            if (activeControl == control) activeControl = ControlStates.none;

            // if tile highlight is active, apply control effects to given tile
            else if (activeControl == ControlStates.tile)
            {
                if ((int)control > -1 && (int)control < 10)
                {
                    UI.ChangeLabel(
                            UI.FindRectangle(lastActiveTile), control == 0 ? "" : control.ToString()[1..]
                    );
                    // when erased, also remove parity
                    if (control == 0)
                        Board.GetInstance().GetTile(lastActiveTile).ResetParity();
                }

                if (control == ControlStates.even)
                    Board.GetInstance().GetTile(lastActiveTile).ChangeEven();

                if (control == ControlStates.odd)
                    Board.GetInstance().GetTile(lastActiveTile).ChangeOdd();

                // unhighlight tile
                activeControl = ControlStates.none;
            }

            // if no tile is highlighted, and control wasn't already pressed, change active control to the pressed control
            else activeControl = control;

            // reset control highlight to appropriate control
            if ((int)activeControl > -1)
                ChangeControlHighlight((Button)LogicalTreeHelper.FindLogicalNode(mainGrid, activeControl.ToString()));
            else
                ChangeControlHighlight(null);

            ChangeTileHighlight(new int[] { -1, -1 });
        }

        /// <summary>
        /// Unhighlight last selected control, and highlight given control.
        /// <b>Sets new control as last highlighted!</b>
        /// </summary>
        /// <param name="toHighlight">New highlight target.</param>
        private static void ChangeControlHighlight(Button toHighlight)
        {
            if (lastHighlightedButton != null)
                lastHighlightedButton.Background = CustomBrushes.GetBrushForState(ColorStates.button);

            if (toHighlight != null)
                toHighlight.Background = CustomBrushes.GetBrushForState(ColorStates.button_pressed);

            lastHighlightedButton = toHighlight;
        }

        /// <summary>
        /// Unhighlight last selected tile, and highlight tile at a certain position.
        /// <b>Sets new tile as last highlighted!</b>
        /// </summary>
        /// <param name="pos">Position of new highlight target.</param>
        private static void ChangeTileHighlight(int[] pos)
        {
            if (lastActiveTile[0] != -1)
                UI.FindRectangle(lastActiveTile).Fill =
                    CustomBrushes.GetBrushForState(Board.GetInstance().GetTile(lastActiveTile).ResetHighlighted());

            if (pos[0] != -1)
                UI.FindRectangle(pos).Fill =
                    CustomBrushes.GetBrushForState(Board.GetInstance().GetTile(pos).SetHighlighted());

            lastActiveTile = pos;
        }
    }

    public enum ControlStates
    {
        tile = -2, none = -1, erase = 0,
        _1 = 1, _2 = 2, _3 = 3, _4 = 4, _5 = 5, _6 = 6, _7 = 7, _8 = 8, _9 = 9,
        even = 10, odd = 11
    }
}
