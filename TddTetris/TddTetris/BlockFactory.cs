using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class BlockFactory : IBlockFactory
    {
        public virtual IBlock MakeBlock()
        {
            System.Random RandNum = new System.Random();
            int randomShapeNumber = RandNum.Next(BlockShapes.Length);
            return new Block(BlockShapes[randomShapeNumber], colors[randomShapeNumber]);
        }

        public Color[] colors = {
            Color.White,
            Color.Blue,
            Color.Yellow,
            Color.Red,
            Color.Green,
            Color.Purple,
            Color.Orange
        };

        public bool[][][] BlockShapes = new bool[7][][] 
        {
            new bool [][] {
                new bool[] {true, true},
                new bool[] {true, true}
            },
            new bool [][] {
                new bool[] {false, true, true},
                new bool[] {true, true, false}
            },
            new bool [][] {
                new bool[] {true, true, false},
                new bool[] {false, true, true},
            },
            new bool [][] {
                new bool[] {true, true, true},
                new bool[] {true, false, false}
            },
            new bool [][] {
                new bool[] {true, true, true},
                new bool[] {false, false, true}
            },
            new bool [][] {
                new bool[] {true, true, true},
                new bool[] {false, true, false}
            },
            new bool [][] {
                new bool[] {true, true, true, true}
            }
        };
    }
}
