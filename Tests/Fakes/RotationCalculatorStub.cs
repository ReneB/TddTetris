using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris {
    namespace Tests
    {
        public class RotationCalculatorStub : IRotationCalculator
        {
            public Vector2 PositionToReturn;

            public Vector2 RotatePositionInCells(int degrees, Vector2 position)
            {
                if (PositionToReturn != null)
                    return PositionToReturn;
                return new Vector2();
            }
        }
    }
}
