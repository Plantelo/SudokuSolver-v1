using System.Windows.Media;

namespace SudokuSolver
{
    /// <summary>
    /// Class for determining which color to apply to a given object.
    /// </summary>
    class CustomBrushes
    {
        public static Brush GetBrushForState(ColorStates colorState)
        {
            return colorState switch
            {
                // tile color is affected by selection highlight, potential errors, and parity

                // base states for parity-less tiles
                ColorStates.normal => Brushes.White,
                ColorStates.error => Brushes.Salmon,
                ColorStates.highlight => Brushes.LightGray,
                ColorStates.error_highlight => Brushes.DarkSalmon,

                // duplicate states for even tiles
                ColorStates.even => Brushes.Lime,
                ColorStates.even_error => Brushes.Goldenrod,
                ColorStates.even_highlight => Brushes.Green,
                ColorStates.even_error_highlight => Brushes.DarkGoldenrod,

                // duplicate states for odd tiles
                ColorStates.odd => Brushes.Cyan,
                ColorStates.odd_error => Brushes.MediumPurple,
                ColorStates.odd_highlight => Brushes.DarkCyan,
                ColorStates.odd_error_highlight => Brushes.Purple,

                // base states for UI buttons
                ColorStates.button => Brushes.LightGray,
                ColorStates.button_pressed => Brushes.Gray,
                _ => Brushes.White,
            };
        }
    }

    public enum ColorStates
    {
        normal, error, highlight, error_highlight,
        even, even_error, even_highlight, even_error_highlight,
        odd, odd_error, odd_highlight, odd_error_highlight,
        button, button_pressed
    }
}
