using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class RotationCalculator : IRotationCalculator
    {
        public Vector2 RotatePositionInCells(int degrees, Vector2 position)
        {
            // add half points to be *inside* a cell instead of on the border
            Vector2 relPos = position + new Vector2(0.5f, 0.5f);

            double sin = Math.Sin((degrees / 180.0) * Math.PI);
            double cos = Math.Cos((degrees / 180.0) * Math.PI);

            double X = (cos * relPos.X - sin * relPos.Y);
            double Y = (sin * relPos.X + cos * relPos.Y);

            return new Vector2((float) Math.Floor(X), (float) Math.Floor(Y));
        }
    }
}
