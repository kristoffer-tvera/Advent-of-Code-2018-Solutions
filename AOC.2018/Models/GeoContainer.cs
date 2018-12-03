using System;

namespace AOC._2018.Models
{
    public class GeoContainer
    {
        public GeoContainer(int width, int height)
        {
            Map = new int[width, height];
            Width = width;
            Height = height;
        }

        public int[,] Map { get; }
        public int Width { get; }
        public int Height { get; }
    }
}