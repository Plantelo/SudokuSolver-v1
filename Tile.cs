using System;
using System.Collections.Generic;
using System.Text;

namespace SudokuSolver
{
    public class Tile
    {
        private int value = 0;
        private bool isEven = false;
        private bool isOdd = false;
        private bool isErroneous = false;
        private bool isHighlighted = false;

        private readonly List<TileSet> includedIn = new List<TileSet>();

        public Tile()
        {
        }

        public void AddTo(TileSet tileSet)
        {
            tileSet.Add(this);
            includedIn.Add(tileSet);
        }

        public void RemoveFrom(TileSet tileSet)
        {
            tileSet.Remove(this);
            includedIn.Remove(tileSet);
        }

        public void SetValue(int value)
        {
            this.value = value;
        }

        public int GetValue()
        {
            return value;
        }

        private ColorStates SetColorState()
        {
            int state = 0;

            if (isEven) state += 4;
            else if (isOdd) state += 8;

            if (isErroneous) state += 1;

            if (isHighlighted) state += 2;

            return (ColorStates)state;
        }

        public ColorStates SetHighlighted()
        {
            isHighlighted = true;
            return SetColorState();
        }

        public ColorStates ResetHighlighted()
        {
            isHighlighted = false;
            return SetColorState();
        }

        public ColorStates SetErroneous()
        {
            isErroneous = true;
            return SetColorState();
        }

        public ColorStates ResetErroneous()
        {
            isErroneous = false;
            return SetColorState();
        }

        public ColorStates ChangeEven()
        {
            if (!isEven)
            {
                isEven = true;
                isOdd = false;
                return SetColorState();
            }
            else
            {
                isEven = false;
                return SetColorState();
            }
        }

        public ColorStates ChangeOdd()
        {
            if (!isOdd)
            {
                isOdd = true;
                isEven = false;
                return SetColorState();
            }
            else
            {
                isOdd = false;
                return SetColorState();
            }
        }

        public ColorStates ResetParity()
        {
            isOdd = false;
            isEven = false;
            return SetColorState();
        }
    }
}
