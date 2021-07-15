using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Input;

namespace SudokuSolver
{
    class Events
    {
        public static void TileClicked(object sender, MouseButtonEventArgs args)
        {
            ControlStates action = Input.ActiveControl();

            if ((int)action < 10 && (int)action > -1)
                UI.ChangeLabel((Rectangle) sender, action == ControlStates.erase ? "" : action.ToString()[1..]);

            if (action == ControlStates.erase)
                ((Rectangle)sender).Fill =
                    CustomBrushes.GetBrushForState(Board.GetInstance().GetTile(UI.FindRectanglePos((Rectangle)sender)).ResetParity());

            if (action < 0)
                Input.SetActiveTile((Rectangle) sender);

            if (action == ControlStates.even)
            {
                ((Rectangle) sender).Fill =
                    CustomBrushes.GetBrushForState(Board.GetInstance().GetTile(UI.FindRectanglePos((Rectangle)sender)).ChangeEven());
            }

            if (action == ControlStates.odd)
            {
                ((Rectangle) sender).Fill =
                    CustomBrushes.GetBrushForState(Board.GetInstance().GetTile(UI.FindRectanglePos((Rectangle) sender)).ChangeOdd());
            }
        }

        public static void ControlClicked(object sender, RoutedEventArgs args)
        {
            Input.SwitchActiveControl(
                (ControlStates)Enum.Parse(
                    typeof(ControlStates),
                    ((FrameworkElement)sender).Name )
                );
        }
    }
}
