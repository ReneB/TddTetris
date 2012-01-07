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
        public class BlockFactoryStub : BlockFactory
        {
            public int NextBlockShape;
            public Color NextBlockColor = Color.Blue;
            public bool makeRealObjects = false;

            public override IBlock MakeBlock()
            {
                if (makeRealObjects)
                {
                    return new Block(BlockShapes[NextBlockShape], NextBlockColor);
                }
                else
                {
                    return new BlockStub(BlockShapes[NextBlockShape], NextBlockColor);
                }
            }
        }
    }
}