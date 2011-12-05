using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace TddTetris {
    namespace Tests
    {
        [TestFixture]
        class RotationMathTest
        {
            [Test]
            public void RotatePosition_ZeroDegrees_ReturnsOriginal()
            {
                // arrange
                Vector2 Pos = new Vector2(1, 1);
                int NumDegrees = 0;

                // act
                Vector2 RotatedPos = (new RotationCalculator()).RotatePositionInCells(NumDegrees, Pos);

                // assert
                Assert.AreEqual(Pos, RotatedPos);                
            }

            [Test]
            public void RotatePosition_NinetyDegrees_RotatesAroundMovingOrigin()
            {
                // arrange
                Vector2 Pos = new Vector2(0, 0);
                int NumDegrees = 90;
                Vector2 ExpectedRotatedPos = new Vector2(-1, 0);

                // act
                Vector2 RotatedPos = (new RotationCalculator()).RotatePositionInCells(NumDegrees, Pos);

                // assert
                Assert.AreEqual(ExpectedRotatedPos, RotatedPos);
            }
        }
    }
}
