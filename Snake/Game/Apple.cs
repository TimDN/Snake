using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Snake.Game
{
    public class Apple
    {
        public Apple(int x, int y)
        {
            Position = new Point(x, y);
        }
        private readonly char _appleCharacter = '¤';
        public Point Position { get; }
        public void Draw(char[,] board)
        {
            board[Position.Y, Position.X] = _appleCharacter;
        }
    }
}
