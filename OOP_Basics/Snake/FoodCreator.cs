using System;
namespace Snake
{
    class FoodCreator
    {
        int mapWidth;
        int mapHeight;
        char symbol;

        Random random = new Random();

        public FoodCreator(int mapWidth, int mapHeight, char symbol)
        {
            this.mapWidth = mapWidth;
            this.mapHeight = mapHeight;
            this.symbol = symbol;
        }

        public Point CreateFood()
        {
            int x = random.Next(2, mapWidth - 2);
            int y = random.Next(2, mapHeight - 2);

            return new Point(x, y, symbol);
        }
    }
}
