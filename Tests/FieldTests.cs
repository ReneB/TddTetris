using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace TddTetris
{
    namespace Tests
    {
        [TestFixture]
        class FieldTests
        {
            BlockFactoryStub bfs;
            BlockStub bs;
            Field f;

            [SetUp]
            public void SetUp()
            {
                bfs = new BlockFactoryStub();
                f = new Field(10,10);

                bfs.NextBlockShape = 1;
                bfs.NextBlockColor = Color.White;
                bfs.makeRealObjects = false;

                bs = (BlockStub) bfs.MakeBlock();
            }

            [Test]
            public void FixBlock_CopiesBlockColorsToGrid()
            {
                // arrange
                bs.expectedColors[0,0] = Color.Blue;
                f.SetBlock(bs, new Vector2(5, 5));

                // act
                f.FixBlock();

                // make sure the color stays, even if a new block enters the field
                f.SetBlock((BlockStub)bfs.MakeBlock(), new Vector2(0, 0));

                // assert
                Assert.AreEqual(Color.Blue, f.Grid[5, 5]);
            }

            [Test]
            public void ColorAt_ColorInCurrentBlock_ChecksCurrentBlock()
            { 
                // arrange
                bs.expectedColors[1, 1] = Color.Blue;
                f.SetBlock(bs, new Vector2(5,5));

                // act
                Color? c = f.ColorAt(new Vector2(6, 6));

                // assert
                Assert.AreEqual(Color.Blue, c);
            }

            [Test]
            public void ColorAt_ColorNotInCurrentBlock_ReturnsEntryInGrid()
            {
                // arrange
                bs.expectedColors[1, 1] = Color.Blue;
                f.SetBlock(bs, new Vector2(5, 5));

                f.FixBlock();
                f.SetBlock(bfs.MakeBlock(), new Vector2(0, 0));

                // act
                Color? c = f.ColorAt(new Vector2(6, 6));

                // assert
                Assert.AreEqual(Color.Blue, c);
            }
        }
    }
}
