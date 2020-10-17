using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Snake
{
    internal class Head : Segment
    {
        private readonly char _upperDirection = '^';
        private readonly char _downDirection = 'v';
        private readonly char _leftDirection = '<';
        private readonly char _rightDirection = '>';

        public Head(Point position, Direction direction) 
            : base(position, direction)
        {
        }

        public bool HeadIsCollidingWithSnake()
        {
            if (!(_nextSegment is null))
            {
                return _nextSegment.IsColliding(Position);
            }
            else
            {
                return false;
            }
        }

        public override void Draw(char[,] board)
        {
            char drawChar = 'x';
            switch (Direction)
            {
                case Direction.up:
                    drawChar = _upperDirection;
                    break;
                case Direction.down:
                    drawChar = _downDirection;
                    break;
                case Direction.left:
                    drawChar = _leftDirection;
                    break;
                case Direction.right:
                    drawChar = _rightDirection;
                    break;
            }
            board[Position.Y, Position.X] = drawChar;
            if (!(_nextSegment is null))
            {
                _nextSegment.Draw(board);
            }
        }
    }
}
