using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IBlockLocationTester
    {
        bool CanPlaceCurrentBlockAt(Vector2 position);
    }
}
