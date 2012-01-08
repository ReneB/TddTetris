using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Moq;

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

            // this test is no longer strictly necessary, but I'll keep it around
            // for reference on how to use Moq
            [Test]
            public void CanMoveLeft_ShouldDelegateToLocationTester()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                f.Tester = mockTester.Object;
                f.SetBlock(bs, new Vector2(0, 0));

                // act
                f.CanMoveLeft();

                // assert
                mockTester.Verify(t => t.CanPlaceCurrentBlockAt(new Vector2(-1,0)));
            }

            // this test is no longer strictly necessary, but I'll keep it around
            // for reference on how to use Moq
            [Test]
            public void CanMoveRight_ShouldDelegateToLocationTester()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                f.Tester = mockTester.Object;
                f.SetBlock(bs, new Vector2(0, 0));

                // act
                f.CanMoveRight();

                // assert
                mockTester.Verify(t => t.CanPlaceCurrentBlockAt(new Vector2(1, 0)));
            }

            [Test]
            public void CanMoveRight_LocationTesterReturnsFalse_ReturnsFalse()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                mockTester.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(false);
                f.Tester = mockTester.Object;

                // act
                bool canMoveRight = f.CanMoveRight();

                // assert
                Assert.IsFalse(canMoveRight);
            }

            [Test]
            public void CanMoveRight_LocationTesterReturnsTrue_ReturnsTrue()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                mockTester.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(true);
                f.Tester = mockTester.Object;

                // act
                bool canMoveRight = f.CanMoveRight();

                // assert
                Assert.IsTrue(canMoveRight);
            }

            [Test]
            public void CanMoveLeft_LocationTesterReturnsFalse_ReturnsFalse()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                mockTester.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(false);
                f.Tester = mockTester.Object;

                // act
                bool canMoveLeft = f.CanMoveLeft();

                // assert
                Assert.IsFalse(canMoveLeft);
            }

            [Test]
            public void CanMoveLeft_LocationTesterReturnsFalse_ReturnsTrue()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                mockTester.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(true);
                f.Tester = mockTester.Object;

                // act
                bool canMoveLeft = f.CanMoveLeft();

                // assert
                Assert.IsTrue(canMoveLeft);
            }

            // this test is no longer strictly necessary, but I'll keep it around
            // for reference on how to use Moq
            [Test]
            public void CanAdvance_ShouldDelegateToLocationTester()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                f.Tester = mockTester.Object;
                f.SetBlock(bs, new Vector2(0, 0));

                // act
                f.CanAdvance();

                // assert
                mockTester.Verify(t => t.CanPlaceCurrentBlockAt(new Vector2(0, 1)));
            }

            [Test]
            public void CanAdvance_LocationTesterReturnsFalse_ReturnsFalse()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                mockTester.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(false);
                f.Tester = mockTester.Object;
                f.SetBlock(bs, new Vector2(0, 0));

                // act
                bool canAdvance = f.CanAdvance();

                // assert
                Assert.IsFalse(canAdvance);
            }

            [Test]
            public void CanAdvance_LocationTesterReturnsTrue_ReturnsTrue()
            {
                // arrange
                Mock<IBlockLocationTester> mockTester = new Mock<IBlockLocationTester>();
                mockTester.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(true);
                f.Tester = mockTester.Object;
                f.SetBlock(bs, new Vector2(0, 0));

                // act
                bool canAdvance = f.CanAdvance();

                // assert
                Assert.IsTrue(canAdvance);
            }

            [Test]
            public void RotateBlockRight_CallsRotateRightOnCurrentBlock()
            {
                // arrange
                Mock<IBlock> blockMock = new Mock<IBlock>();
                f.SetBlock(blockMock.Object, new Vector2(4, 4));

                // act
                f.RotateBlockRight();

                blockMock.Verify(b => b.RotateRight(), Times.Once());
            }

            [Test]
            public void RotateBlockRight_RotationNotPossible_CallsRotateLeftOnCurrentBlock()
            {
                // arrange
                Mock<IBlock> blockMock = new Mock<IBlock>();
                f.SetBlock(blockMock.Object, new Vector2(4, 4));
                Mock<IBlockLocationTester> testerMock = new Mock<IBlockLocationTester>();
                f.Tester = testerMock.Object;
                testerMock.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(false);

                // act
                f.RotateBlockRight();

                blockMock.Verify(b => b.RotateRight(), Times.Once());
                blockMock.Verify(b => b.RotateLeft(), Times.Once());
            }

            [Test]
            public void RotateBlockLeft_CallsRotateLeftOnCurrentBlock()
            {
                // arrange
                Mock<IBlock> blockMock = new Mock<IBlock>();
                f.SetBlock(blockMock.Object, new Vector2(4, 4));

                // act
                f.RotateBlockLeft();

                blockMock.Verify(b => b.RotateLeft(), Times.Once());
            }

            [Test]
            public void RotateBlockLeft_RotationNotPossible_CallsRotateRightOnCurrentBlock()
            {
                // arrange
                Mock<IBlock> blockMock = new Mock<IBlock>();
                f.SetBlock(blockMock.Object, new Vector2(4, 4));
                Mock<IBlockLocationTester> testerMock = new Mock<IBlockLocationTester>();
                f.Tester = testerMock.Object;
                testerMock.Setup(t => t.CanPlaceCurrentBlockAt(It.IsAny<Vector2>())).Returns(false);

                // act
                f.RotateBlockLeft();

                blockMock.Verify(b => b.RotateLeft(), Times.Once());
                blockMock.Verify(b => b.RotateRight(), Times.Once());
            }
        }
    }
}
