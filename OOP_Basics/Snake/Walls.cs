using System;
using System.Collections.Generic;

namespace Snake
{
    public class Walls
    {
        List<Figure> wallList;

        public Walls(int mapWidth, int mapHeight)
        {
            const char borderSymbol = '+';
            wallList = new List<Figure>()
            {
                new HorizontalLine(0, mapWidth, 0, borderSymbol),
                new HorizontalLine(0, mapWidth, mapHeight, borderSymbol),
                new VerticalLine(0, mapHeight, 0, borderSymbol),
                new VerticalLine(0, mapHeight, mapWidth, borderSymbol)
            };
        }

        public void Draw()
        {
            foreach (Figure wall in wallList)
                wall.Draw();
        }

        internal bool isHit(Figure figure)
        {
            foreach (Figure wall in wallList)
                if (wall.isHit(figure))
                    return true;
            return false;
        }
    }
}
