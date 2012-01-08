using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Block : IBlock
    {
        private int numRotationStepsInFullCircle = 4;

        public int RotationSteps;
        public int CurrentWidth { get; private set; }
        public int CurrentHeight { get; private set; }
        public Color ShapeColor { get; private set; }

        private bool[][] Shape;
        private IRotationCalculator Rotator = new RotationCalculator();

        public Block(bool[][] Shape, Color color)
        {
            this.ShapeColor = color;
            this.Shape = Shape;
            this.CurrentWidth = Shape[0].Length;
            this.CurrentHeight = Shape.Length;
        }

        public void RotateLeft()
        {
            RotationSteps = (RotationSteps + 1) % numRotationStepsInFullCircle;
            SwapHeightAndWidth();
        }

        public void RotateRight()
        {
            RotationSteps = (RotationSteps + numRotationStepsInFullCircle - 1) % numRotationStepsInFullCircle;
            SwapHeightAndWidth();
        }

        public Color? ColorAt(Vector2 position)
        {
            if (position.X >= CurrentWidth || position.X < 0 || position.Y >= CurrentHeight || position.Y < 0)
                return null;

            int degreesRotated = RotationSteps * 360 / numRotationStepsInFullCircle;

            Vector2 originalPosition = Rotator.RotateCell(360 - degreesRotated, position - OriginPosition());

            if (Shape[(int)originalPosition.Y][(int)originalPosition.X])
                return ShapeColor;
            return null;
        }

        private Vector2 OriginPosition()
        {
            int X = 0, Y = 0;

            if (RotationSteps == 1 || RotationSteps == 2)
                X = CurrentWidth;
            if (RotationSteps > 1)
                Y = CurrentHeight;
            return new Vector2(X, Y);
        }

        private void SwapHeightAndWidth() 
        {
            int tmp = CurrentHeight;
            CurrentHeight = CurrentWidth;
            CurrentWidth = tmp;
        }
    }
}
