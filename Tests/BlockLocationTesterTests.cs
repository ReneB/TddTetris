using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Moq;

namespace TddTetris {
    namespace Tests {
        [TestFixture]
        class BlockLocationTesterTests
        {
            BlockLocationTester tester;
            Field f;
            IBlock b;

            [SetUp]
            public void Setup()
            {
                f = new Field(10, 10);
                tester = new BlockLocationTester(f);
                bool[][] shape = { 
                    new bool[] { true, true, false }, 
                    new bool [] { false, true, true}
                };
                b = new Block(shape, Color.White);
                f.SetBlock(b, new Vector2(0, 0));
            }

            [Test]
            public void CanPlaceCurrentBlockAt_LeftOfField_ReturnsFalse()
            {
                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(-1, 0));

                Assert.IsFalse(result);
            }

            [Test]
            public void CanPlaceCurrentBlockAt_RightOfField_ReturnsFalse()
            {
                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(100, 0));

                Assert.IsFalse(result);
            }

            [Test]
            public void CanPlaceCurrentBlockAt_AboveField_ReturnsFalse()
            {
                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(0, -100));

                Assert.IsFalse(result);
            }

            [Test]
            public void CanPlaceCurrentBlockAt_BelowField_ReturnsFalse()
            {
                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(0, 100));

                Assert.IsFalse(result);
            }

            [Test]
            public void CanPlaceCurrentBlockAt_WithinFieldWithNoObstructions_ReturnsTrue()
            {
                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(0, 0));

                Assert.IsTrue(result);
            }

            [Test]
            public void CanPlaceCurrentBlockAt_WithinFieldTooCloseToRightWall_ReturnsFalse()
            {
                int ImpossibleLocationX = f.Width - b.CurrentWidth + 1;
                // which, incidentally, is 7. Too much logic, 
                // but just '7' would be less readable.

                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(ImpossibleLocationX, 0));

                Assert.IsFalse(result);
            }

            [Test]
            public void CanPlaceCurrentBlockAt_WithinFieldTooCloseToBottom_ReturnsFalse()
            {
                int ImpossibleLocationY = f.Height - b.CurrentHeight + 1;
                // which, incidentally, is 7. Too much logic, 
                // but just '7' would be less readable.

                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(0, ImpossibleLocationY));

                Assert.IsFalse(result);
            }

            [Test]
            public void CanPlaceCurrentBlockAt_WithinFieldOverlappingExistingBlock_ReturnsFalse()
            {
                // arrange
                f.Grid[1,1] = Color.White;

                // act
                bool result = tester.CanPlaceCurrentBlockAt(new Vector2(0, 0));

                // assert
                Assert.IsFalse(result);
            }
        }
    }
}
