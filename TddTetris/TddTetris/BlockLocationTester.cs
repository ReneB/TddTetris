using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class BlockLocationTester : IBlockLocationTester
    {
        private IField field;

        public BlockLocationTester(IField field)
        {
            this.field = field;
        }

        public bool CanPlaceCurrentBlockAt(Vector2 position)
        {
            IBlock block = field.CurrentBlock;
            if (position.X < 0 || position.X + field.CurrentBlock.CurrentWidth > field.Width )
                return false;
            else if (position.Y < 0 || position.Y + field.CurrentBlock.CurrentHeight > field.Height )
                return false;
            return true;
        }
    }
}
