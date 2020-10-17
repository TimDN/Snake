using Snake.Game;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        static void Main()
        {
            var game = new Board();
            while (!game.Quit)
            {
                game.Play();
                if (game.Lost)
                {
                    Console.Clear();
                    Console.WriteLine(" Enter to play again ");
                    Console.ReadLine();
                    game = new Board();
                }
            }
        }
    }
}
