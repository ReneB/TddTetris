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

        public IBlock Block { get; private set; }
        public Vector2 Position { get; private set; }

        public Field(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Grid = new Color?[width, height];
        }

        public Color? ColorAt(Vector2 position)
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

            if (Block != null)
            {
                return Block.ColorAt(position - this.Position);
            }
            return null;
        }

        public void SetBlock(IBlock block, Vector2 position)
        {
            this.Block = block;
            this.Position = position;
        }

        public void AdvanceBlock()
        {
            Position = new Vector2(Position.X, Position.Y + 1);
        }

        public bool CanMoveLeft()
        {
            return Position.X > 0;
        }

        public void MoveBlockLeft()
        {
            Position = new Vector2(Position.X - 1, Position.Y);
        }

        public bool CanMoveRight()
        {
            return Position.X < Width - 1;
        }

        public void MoveBlockRight()
        {
            Position = new Vector2(Position.X + 1, Position.Y);
        }

        public bool CanAdvance()
        {
            return Block != null && Position.Y + Block.CurrentHeight < Height;
        }

        public void FixBlock()
        {
            if (Block == null)
                return;
            
            int height = Block.CurrentHeight;
            int width = Block.CurrentWidth;

            for (int Y = 0; Y < height; Y++) {
                for (int X = 0; X < width; X++) {
                    Color? c = Block.ColorAt(new Vector2(X,Y));
                    if (c != null) {
                        Grid[(int) Position.X + X, (int) Position.Y + Y] = c;
                    }
                }
            }
        }

        public void RotateBlockRight()
        {
            // Todo: test this with mocks
            Block.RotateRight();
        }

        public void RotateBlockLeft()
        {
            // Todo: test this with mocks
            Block.RotateLeft();
        }            
    }
}
