using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IBlock
    {
        void RotateLeft();
        void RotateRight();

        int CurrentHeight { get; }
        int CurrentWidth { get; }

        Color ShapeColor { get; }

        Color? ColorAt(Vector2 position);
    }
}
