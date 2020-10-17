using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Snake
{
    internal class Segment
    {
        public Segment(Point position, Direction direction)
        {
            _position = position;
            Direction = direction;
        }


        private bool _turnedLastUpdate;
        private Point _position;
        private readonly char _segmentCharacter = '*';
        private readonly char _fullSegment = '@';
        private bool _isFull;
        
        protected Segment _nextSegment;
        public Direction Direction { get; private set; }
        public Point Position => _position;

        public Segment Clone()
        {
            return new Segment(new Point(Position.X, Position.Y), Direction);
        }

        public void Eat()
        {
            _isFull = true;
        }

        public void ChangeDirection(Direction newDirection)
        {
            Direction = newDirection;
            _turnedLastUpdate = true;
        }

        public void Update()
        {
            if (ShouldCreateNextSegment())
            {
                CreateNextSegment();
                MoveSegment();
            }
            else
            {
                MoveSegment();
                if (!(_nextSegment is null))
                {
                    UpdateNextSegment();
                }
            }
        }

        public bool IsColliding(Point checkPoint)
        {
            if(checkPoint == Position)
            {
                return true;
            }
            if(_nextSegment is null)
            {
                return false;
            }
            return _nextSegment.IsColliding(checkPoint);
        }

        public virtual void Draw(char[,] board)
        {
            var character = _isFull ? _fullSegment : _segmentCharacter;
            board[Position.Y, Position.X] = character;
            if(!(_nextSegment is null))
            {
                _nextSegment.Draw(board);
            }
        }

        private bool ShouldCreateNextSegment()
        {
            return _isFull && _nextSegment is null;
        }

        private void CreateNextSegment()
        {
            _nextSegment = Clone();
            _isFull = false;
        }

        private void UpdateNextSegment()
        {
            _nextSegment.Update();
            MoveFullness();
            if (_turnedLastUpdate)
            {
                _nextSegment.ChangeDirection(Direction);
            }
        }

        private void MoveFullness()
        {
            if (_isFull)
            {
                _nextSegment.Eat();
                _isFull = false;
            }
        }

        private void MoveSegment()
        {
            switch (Direction)
            {
                case Direction.up:
                    _position.Y -= 1;
                    break;
                case Direction.down:
                    _position.Y += 1;
                    break;
                case Direction.right:
                    _position.X += 1;
                    break;
                case Direction.left:
                    _position.X -= 1;
                    break;
            }
        }
    }
}
