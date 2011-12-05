using System;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace TddTetris {
    namespace Tests {
        [TestFixture]
        class BlockTests {
            Block b;
            BlockFactoryStub blockFactory;

            [SetUp]
            public void SetUp()
            {
                blockFactory = new BlockFactoryStub();
                blockFactory.makeRealObjects = true;
                b = (Block) blockFactory.MakeBlock();
            }

            [Test]
            public void RotateLeft_incrementsRotationSteps() {
                //arrange
                b.RotationSteps = 0;

                //act
                b.RotateLeft();

                //assert
                Assert.AreEqual(1, b.RotationSteps);
            }

            [Test]
            public void RotateLeft_clampsRotationSteps()
            {
                //arrange
                b.RotationSteps = 3;

                //act
                b.RotateLeft();

                //assert
                Assert.AreEqual(0, b.RotationSteps);
            }

            [Test]
            public void RotateRight_decrementsRotationSteps()
            {
                //arrange
                b.RotationSteps = 3;

                //act
                b.RotateRight();

                // assert
                Assert.AreEqual(2, b.RotationSteps);
            }

            [Test]
            public void RotateRight_clampsRotationSteps()
            {
                //arrange
                b.RotationSteps = 0;

                //act
                b.RotateRight();

                // assert
                Assert.AreEqual(3, b.RotationSteps);
            }

            [Test]
            public void RotateRight_swapsHeightAndWidth()
            {
                //arrange
                b.CurrentHeight = 1;
                b.CurrentWidth = 4;

                //act
                b.RotateRight();

                // assert
                /* If my .Net-fu were a little more awesome, I would have 
                   created a mock with an expectation of SwapHeightAndWidth() 
                   being called */
                Assert.AreEqual(4, b.CurrentHeight);
                Assert.AreEqual(1, b.CurrentWidth);
            }

            [Test]
            public void RotateLeft_swapsHeightAndWidth()
            {
                //arrange
                b.CurrentHeight = 1;
                b.CurrentWidth = 4;

                //act
                b.RotateLeft();

                // assert
                /* If my .Net / Moq-fu were a little more awesome, I would have 
                   created a mock with an expectation of SwapHeightAndWidth() 
                   being called */
                Assert.AreEqual(4, b.CurrentHeight);
                Assert.AreEqual(1, b.CurrentWidth);
            }

            [Test]
            public void Constructor_SetsCurrentHeight()
            {
                blockFactory.NextBlockShape = 1;
                b = (Block) blockFactory.MakeBlock();
                Assert.AreEqual(b.CurrentHeight, 2);
            }

            [Test]
            public void Constructor_SetsCurrentWidth()
            {
                blockFactory.NextBlockShape = 1;
                b = (Block) blockFactory.MakeBlock();
                Assert.AreEqual(b.CurrentWidth, 3);
            }

            [Test]
            public void ColorAt_UnrotatedAndTrueInShapeLocation_ReturnsWhite()
            {
                // arrange
                blockFactory.NextBlockShape = 1;
                b = (Block) blockFactory.MakeBlock();
                RotationCalculatorStub rotator = new RotationCalculatorStub();
                rotator.PositionToReturn = new Vector2(1, 1);
                b.Rotator = rotator;

                // act
                Color? colorAtIndex = b.ColorAt(new Vector2(1,1));

                // assert
                Assert.AreEqual(colorAtIndex, b.ShapeColor);
            }

            [Test]
            public void ColorAt_UnrotatedAndFalseInShapeLocation_ReturnsNull()
            {
                // arrange
                blockFactory.NextBlockShape = 1;
                b = (Block) blockFactory.MakeBlock();

                // act
                Color? colorAtIndex = b.ColorAt(new Vector2(0, 0));

                // assert
                Assert.AreEqual(colorAtIndex, null);
            }

            [Test]
            public void ColorAt_Rotated_MapsToOriginalCoordinates()
            {
                // arrange
                // **
                //  **
                blockFactory.NextBlockShape = 1;
                b = (Block) blockFactory.MakeBlock();

                Vector2 testPosition = new Vector2(1, 0);

                // act
                b.RotateLeft();
                Color? colorAfter = b.ColorAt(testPosition);

                // assert
                Assert.AreEqual(null, colorAfter);
            }

            [Test]
            public void ColorAt_OutsideBlock_ReturnsNull()
            {
                // act
                Color? response = b.ColorAt(new Vector2(4, 4));

                // assert
                Assert.AreEqual(null, response);
            }

            [Test]
            public void ColorAt_OutsideBlockWithNegativeValues_ReturnsNull()
            {
                // act
                Color? response = b.ColorAt(new Vector2(-1, 0));

                // assert
                Assert.AreEqual(null, response);
            }
        }
    }
 }
