﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public interface IField
    {
        int Height { get; }
        int Width { get; }

        IBlock CurrentBlock { get; }

        bool CanMoveLeft();
        void MoveBlockLeft();

        bool CanMoveRight();
        void MoveBlockRight();

        bool CanAdvance();
        void AdvanceBlock();

        Color? ColorAt(Vector2 position, bool ignoreCurrentBlock = false);

        void SetBlock(IBlock block, Vector2 position);
        void FixBlock();

        void RotateBlockRight();
        void RotateBlockLeft();
    }
}
