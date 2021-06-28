using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            Console.Clear();

            Walls walls = new Walls(80, 20);
            walls.Draw();

            Point p = new Point(4, 5, '*');
            Snake snake = new Snake(p, 4, Direction.Right);
            snake.Draw();

            FoodCreator foodCreator = new FoodCreator(80, 20, '$');
            Point food = foodCreator.CreateFood();
            food.Draw();

            while (true)
            {
                if (walls.isHit(snake) || snake.isHitTail())
                {
                    break;
                }

                if (snake.Eat(food))
                {
                    food.Draw();
                    food = foodCreator.CreateFood();
                    food.Draw();
                }
                else
                    snake.Move();

                Thread.Sleep(100);

                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    snake.HandleKey(keyInfo);
                }

            }

        }
    }
}
