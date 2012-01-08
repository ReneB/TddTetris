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
                Vector2 RotatedPos = (new RotationCalculator()).RotateCell(NumDegrees, Pos);

                // assert
                Assert.AreEqual(Pos, RotatedPos);                
            }

            [Test]
            public void RotatePosition_360Degrees_ReturnsOriginal()
            {
                // arrange
                Vector2 Pos = new Vector2(1, 1);
                int NumDegrees = 360;

                // act
                Vector2 RotatedPos = (new RotationCalculator()).RotateCell(NumDegrees, Pos);

                // assert
                Assert.AreEqual(Pos, RotatedPos);
            }

            [Test]
            public void RotatePosition_90Degrees_RotatesAroundOrigin()
            {
                // arrange
                Vector2 Pos = new Vector2(0, 0);
                int NumDegrees = 90;

                /* rotating the cell 90 degrees turns its top 
                  left corner into its new origin, which should 
                  then be at (-1,0) */
                Vector2 ExpectedRotatedPos = new Vector2(-1, 0);

                // act
                Vector2 RotatedPos = (new RotationCalculator()).RotateCell(NumDegrees, Pos);

                // assert
                Assert.AreEqual(ExpectedRotatedPos, RotatedPos);
            }

            [Test]
            public void RotatePosition_Minus90Degrees_RotatesAroundOrigin()
            {
                // arrange
                Vector2 Pos = new Vector2(0, 0);
                int NumDegrees = -90;

                /* rotating the cell -90 degrees turns its bottom 
                  right corner into its new origin, which should 
                  then be at (0,-1) */
                Vector2 ExpectedRotatedPos = new Vector2(0, -1);

                // act
                Vector2 RotatedPos = (new RotationCalculator()).RotateCell(NumDegrees, Pos);

                // assert
                Assert.AreEqual(ExpectedRotatedPos, RotatedPos);
            }
        }
    }
}
