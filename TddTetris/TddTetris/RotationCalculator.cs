using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class RotationCalculator : IRotationCalculator
    {
        public Vector2 RotateCell(int degrees, Vector2 position)
        {
            /* add half points to be *inside* a cell instead of on the border,
               then rotate around (0,0) and round down to get the coordinates
               of the bottom left corner of the cell */
            Vector2 relPos = position + new Vector2(0.5f, 0.5f);

            Vector2 rotatedPosition = RotatePoint(degrees, relPos);

            Vector2 result = new Vector2();
            result.X = (float)Math.Floor(rotatedPosition.X);
            result.Y = (float)Math.Floor(rotatedPosition.Y);
            return result;
        }

        private Vector2 RotatePoint(int degrees, Vector2 position)
        {
            double sin = Math.Sin((degrees / 180.0) * Math.PI);
            double cos = Math.Cos((degrees / 180.0) * Math.PI);

            float X = (float)(cos * position.X - sin * position.Y);
            float Y = (float)(sin * position.X + cos * position.Y);

            return new Vector2(X, Y);
        }
    }
}
