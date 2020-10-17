using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Snake
{
    public class Snake
    {
        public Snake(int startPosX, int startPosY)
        {
            Head = new Head(new Point(startPosX, startPosY), Direction.right);
        }

        internal Head Head { get; }

        public void Update()
        {
            Head.Update();
        }

        public void Eat()
        {
            Head.Eat();
        }

        public void Turn(Direction direction)
        {
            if(IsSameDirection(direction) || IsOppositeDirection(direction))
            {
                return;
            }
            Head.ChangeDirection(direction);
        }

        public void Draw(char[,] board)
        {
            Head.Draw(board);
        }

        public bool IsPositionFree(Point position)
        {
            return Head.IsColliding(position);
        }

        public bool IsSnakeCollidingWithItSelf()
        {
            return Head.HeadIsCollidingWithSnake();
        }

        private bool IsSameDirection(Direction direction)
        {
            return direction == Head.Direction;
        }

        private bool IsOppositeDirection(Direction direction)
        {
            if(direction == Direction.up)
            {
                if (Head.Direction == Direction.down)
                    return true;
            }
            else if (direction == Direction.down)
            {
                if (Head.Direction == Direction.up)
                    return true;
            }
            else if (direction == Direction.left)
            {
                if (Head.Direction == Direction.right)
                    return true;

            }
            else if (direction == Direction.right)
            {
                if (Head.Direction == Direction.left)
                    return true;
            }
            return false;
        }
    }
}
