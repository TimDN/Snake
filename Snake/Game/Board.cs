using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace Snake.Game
{
    public class Board
    {
        private readonly Snake _snake;
        private readonly int _height;
        private readonly int _width;
        private readonly Random _random;
        private Apple _apple;

        public Board(int height = 15, int width = 20)
        {
            _height = height;
            _width = width;
            _snake = new Snake(5, 5);
            _board = new char[height, width];
            _random = new Random();
            InitBoard();
        }

        public bool Lost { get; private set; }
        public bool Quit { get; private set; }

        private readonly char[,] _board;
        private int LeftEdgeX => 0;
        private int RightEdgeX => _width - 1;
        private int TopEdgeY => 0;
        private int BottomEdgeY => _height - 1;


        public void Play()
        {
            while (!Lost && !Quit)
            {
                EatApple();
                Update();
                CheckForCollisions();
                if (Lost)
                {
                    continue;
                }
                DrawBoard();
                Thread.Sleep(150);
            }
            if (Lost)
            {
                while (!Console.KeyAvailable)
                {
                    DrawBoard();
                    Thread.Sleep(300);
                    DrawEmptyBoard();
                    Thread.Sleep(300);
                }
            }
        }

        private void Update()
        {
            ConsoleKeyInfo input;
            if (Console.KeyAvailable)
            {
                input = Console.ReadKey(true);
                var direction = ConsoleKeyToDirection(input.Key);
                _snake.Turn(direction);
            }
            _snake.Update();
        }

        private void DrawBoard()
        {
            EmptyBoard();
            _snake.Draw(_board);
            _apple.Draw(_board);
            var sb = new StringBuilder();
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    sb.Append(_board[i, j]);
                }
                sb.Append("\n");
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(sb.ToString());
        }

        private void DrawEmptyBoard()
        {
            EmptyBoard();
            var sb = new StringBuilder();
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                for (int j = 0; j < _board.GetLength(1); j++)
                {
                    sb.Append(_board[i, j]);
                }
                sb.Append("\n");
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(sb.ToString());
        }

        private void EatApple()
        {
            if(_apple.Position == _snake.Head.Position)
            {
                _snake.Eat();
                CreateApple();
            }
        }

        private void CheckForCollisions()
        {
            if (SnakeHasCollidedWithWall())
            {
                Lost = true;
            }
            if (_snake.IsSnakeCollidingWithItSelf())
            {
                Lost = true;
            }
        }

        private void CreateApple()
        {
            var done = false;
            while (!done)
            {
                var x = _random.Next(LeftEdgeX + 1, RightEdgeX);
                var y = _random.Next(TopEdgeY + 1, BottomEdgeY);
                if(!_snake.IsPositionFree(new Point(x, y)))
                {
                    _apple = new Apple(x, y);
                    done = true;
                }
            }
        }

        private bool SnakeHasCollidedWithWall()
        {
            var snakePos = _snake.Head.Position;
            return snakePos.X == LeftEdgeX || snakePos.X == RightEdgeX ||
                snakePos.Y == TopEdgeY || snakePos.Y == BottomEdgeY;
        }

        private Direction ConsoleKeyToDirection(ConsoleKey consoleKey)
        {
            switch (consoleKey)
            {
                case ConsoleKey.UpArrow:
                    return Direction.up;
                case ConsoleKey.DownArrow:
                    return Direction.down;
                case ConsoleKey.LeftArrow:
                    return Direction.left;
                case ConsoleKey.RightArrow:
                    return Direction.right;
                case ConsoleKey.Escape:
                    throw new ApplicationException();
                default:
                    return Direction.error;
            }
        }

        private void InitBoard()
        {
            AddBorder();
            EmptyBoard();
            CreateApple();
        }

        private void EmptyBoard()
        {
            for (int i = 1; i < _board.GetLength(0) - 1; i++)
            {
                for (int j = 1; j < _board.GetLength(1) - 1; j++)
                {
                    _board[i, j] = ' ';
                }
            }
        }

        private void AddBorder()
        {
            for (int j = 0; j < _board.GetLength(1); j++)
            {
                _board[TopEdgeY, j] = '#';
                _board[BottomEdgeY, j] = '#';
            }
            for (int i = 0; i < _board.GetLength(0); i++)
            {
                _board[i, LeftEdgeX] = '#';
                _board[i, RightEdgeX] = '#';
            }
        }
    }
}
