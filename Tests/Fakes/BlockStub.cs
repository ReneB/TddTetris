using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using TddTetris;

namespace TddTetris
{
    namespace Tests
    {
        public class BlockStub : IBlock
        {
            public int CurrentHeight { get; set; }
            public int CurrentWidth { get; set; }

            public Color?[,] expectedColors;
            public bool[][] Shape { get; set;  }
            public Color ShapeColor { get; set; }

            public BlockStub(bool[][] Shape, Color theColor) {
                CurrentHeight = 4;
                CurrentWidth = 4;
                this.Shape = Shape;
                expectedColors = new Color?[4,4];
                for (int X = 0; X < 4; X++)
                    for (int Y = 0; Y < 4; Y++)
                        expectedColors[X, Y] = null;            
            }

            public Color? ColorAt(Vector2 position)
            {
                return expectedColors[(int) position.Y, (int) position.X];
            }

            public void RotateRight() { }
            public void RotateLeft() { }
        }
    }
}
