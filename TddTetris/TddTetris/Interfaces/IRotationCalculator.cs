using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IRotationCalculator
    {
        Vector2 RotatePositionInCells(int degrees, Vector2 position);
    }
}
