using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

    public class MapGenerator
    {
        private Random random;
        private int[,] map;

        public MapGenerator(int width, int height, int seed)
        {
            random = new Random(seed);
            map = new int[width, height];
        }

        public int[,] GenerateMap()
        {
            for (int x = 0; x < map.GetLength(0); x++)
            {
                for (int y = 0; y < map.GetLength(1); y++)
                {
                    map[x, y] = random.Next(0, 2);
                }
            }

            return map;
        }
    }

    
    
