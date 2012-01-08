using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TddTetris
{
    public class Field : IField
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        public Color?[,] Grid;

        public IBlock CurrentBlock { get; private set; }
        public Vector2 Position { get; private set; }

        public IBlockLocationTester Tester;

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Grid = new Color?[width, height];

            Tester = new BlockLocationTester(this);
        }

        public Color? ColorAt(Vector2 position, bool ignoreCurrentBlock = false)
        {
            float x = position.X;
            float y = position.Y;

            if (x < 0 || x >= Width || y < 0 || y >= Height)
            {
                throw new IndexOutOfRangeException();
            }

            Color? c = Grid[(int) x, (int) y];
            if (c != null)
            {
                return c;
            }

            if (!ignoreCurrentBlock && CurrentBlock != null)
            {
                return CurrentBlock.ColorAt(position - this.Position);
            }
            return null;
        }

        public void SetBlock(IBlock block, Vector2 position)
        {
            this.CurrentBlock = block;
            this.Position = position;
        }

        public void AdvanceBlock()
        {
            Position = new Vector2(Position.X, Position.Y + 1);
        }

        public bool CanMoveLeft()
        {
            Vector2 newPosition = new Vector2(Position.X - 1, Position.Y);
            return Tester.CanPlaceCurrentBlockAt(newPosition);
        }

        public void MoveBlockLeft()
        {
            Position = new Vector2(Position.X - 1, Position.Y);
        }

        public bool CanMoveRight()
        {
            Vector2 newPosition = new Vector2(Position.X + 1, Position.Y);
            return Tester.CanPlaceCurrentBlockAt(newPosition);
        }

        public void MoveBlockRight()
        {
            Position = new Vector2(Position.X + 1, Position.Y);
        }

        public bool CanAdvance()
        {
            if (CurrentBlock != null) {
                Vector2 newPosition = new Vector2(Position.X, Position.Y + 1);
                return Tester.CanPlaceCurrentBlockAt(newPosition);
            }
            return false;
        }

        public void FixBlock()
        {
            if (CurrentBlock == null)
                return;
            
            int height = CurrentBlock.CurrentHeight;
            int width = CurrentBlock.CurrentWidth;

            for (int Y = 0; Y < height; Y++) {
                for (int X = 0; X < width; X++) {
                    Color? c = CurrentBlock.ColorAt(new Vector2(X,Y));
                    if (c != null) {
                        Grid[(int) Position.X + X, (int) Position.Y + Y] = c;
                    }
                }
            }
        }

        public void RotateBlockRight()
        {
            // Todo: test this with mocks
            CurrentBlock.RotateRight();
        }

        public void RotateBlockLeft()
        {
            // Todo: test this with mocks
            CurrentBlock.RotateLeft();
        }            
    }
}
