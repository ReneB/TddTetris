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

            [Test]
            public void CanMoveLeft_WhenBlockTouchesLeftWall_ReturnsFalse()
            {
                // arrange
                f.SetBlock(bs, new Vector2(0,8));

                // act
                bool canMoveLeft = f.CanMoveLeft();

                // assert
                Assert.IsFalse(canMoveLeft);
            }

            [Test]
            public void CanMoveLeft_WhenBlockDoesNotTouchLeftWall_ReturnsTrue()
            {
                // arrange
                f.SetBlock(bs, new Vector2(4, 8));

                // act
                bool canMoveLeft = f.CanMoveLeft();

                // assert
                Assert.IsTrue(canMoveLeft);
            }

            [Test]
            public void CanMoveRight_WhenBlockDoesNotTouchRightWall_ReturnsTrue()
            {
                // arrange
                f.SetBlock(bs, new Vector2(0, 8));

                // act
                bool canMoveRight = f.CanMoveRight();

                // assert
                Assert.IsTrue(canMoveRight);
            }

            [Test]
            public void CanMoveRight_WhenBlockTouchesRightWall_ReturnsFalse()
            {
                // arrange
                int LocationX = f.Width - bs.CurrentWidth;
                // which, incidentally, is 6. Too much logic, 
                // but just '6' would be less readable.
                f.SetBlock(bs, new Vector2(LocationX, 8));

                // act
                bool canMoveRight = f.CanMoveRight();

                // assert
                Assert.IsFalse(canMoveRight);
            }

            [Test]
            public void CanAdvance_NotAtBottom_ReturnsTrue()
            {
                // arrange
                f.SetBlock(bs, new Vector2(5, 5));

                // act
                bool canAdvance = f.CanAdvance();

                // assert
                Assert.IsTrue(canAdvance);
            }

            [Test]
            public void CanAdvance_AtBottom_ReturnsFalse()
            {
                // arrange
                Vector2 bottomPosition = new Vector2(5, f.Height - bs.CurrentHeight);
                // This is 6, but that would be a magic number. This code involves 
                // some logic (boo hiss), but has better readability
                f.SetBlock(bs, bottomPosition);

                // act
                bool canAdvance = f.CanAdvance();

                // assert
                Assert.IsFalse(canAdvance);
            }
        }
    }
}
